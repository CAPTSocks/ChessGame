using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Grid : MonoBehaviour
{
    public float offset = 1;
    public List<SpriteRenderer> squareSprites = new List<SpriteRenderer>();
    public GameObject tile;
    public Piece Pawn;
    public List<Piece> whitePieces = new List<Piece>();
    public List<Piece> blackPieces = new List<Piece>();

    private Piece selectedPiece = null;
    private Tile selectedTile = null;
    private Tile[,] gridArray;
    private List<Tile> highLightedTiles = new List<Tile>();
    private float height = 8;
    private float width = 8;
    public float xStart = -3.5f;
    private float yStart = 3.5f;
    private Camera mainCamera;
    private bool tileSelected = false;

    [SerializeField]
    private GameObject graveyardRef;
    // private SpriteRenderer blackTileSprite, whiteTileSprite;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        CalculateGridStart();
        InitializeGrid();
        SpawnPieces();
    }

    void CalculateGridStart()
    {
        float calculatedStart = (width - 1) / 2;
        calculatedStart = calculatedStart * offset;

        float calculatedYStart = (height - 1) / 2;
        calculatedYStart = calculatedYStart * offset;
        xStart = -calculatedStart;
        yStart = calculatedYStart;

    }

    void InitializeGrid()
    {
        gridArray = new Tile[(int)height, (int)width];
        bool squarepicker = true;

        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < this.width; w++)
            {
                var newSquare = Instantiate(tile);
                gridArray[h, w] = newSquare.GetComponent<Tile>();
                newSquare.transform.position = GridToPixel(h, w);
                newSquare.GetComponent<Tile>().SetupTile(new Vector2Int(h, w), squareSprites[Convert.ToInt32(squarepicker)], this);
                newSquare.name = "Tile " + h.ToString() + "," + w.ToString();
                squarepicker = !squarepicker;

            }
            squarepicker = !squarepicker;
        }
    }

    private Vector2 GridToPixel(int column, int row)
    {
        float newX = xStart + offset * column;
        float newY = yStart + -offset * row;

        return new Vector2(newX, newY);
    }

    private void SpawnPieces()
    {
        int pieceToSpawn = 0;
        //Spawn big pieces
        for (int i = 7; i > -1; i -= 7)
        {
            pieceToSpawn = 0;

            for (int j = 0; j < whitePieces.Count; j++)
            {

                //spawn back row pieces
                InitilizePiece(pieceToSpawn, j, i);
                pieceToSpawn++;
            }
        }

        //Spawn pawns
        for (int i = 6; i > -1; i -= 5)
        {
            for (int j = 0; j < whitePieces.Count; j++)
            {
                //spawn pawns
                InitilizePawn(j, i);
            }
        }

    }

    private void InitilizePawn(int x, int y)
    {
        Piece.PieceColor color;
        string name;
        if (y == 6)
        {
            color = Piece.PieceColor.White;
            name = "white";
        }
        else
        {

            color = Piece.PieceColor.Black;
            name = "black";
        }
        Piece newPawn = Instantiate(Pawn);
        newPawn.IntializePiece(new Vector2Int(x, y), gridArray[x, y].transform.position, color, this);
        var currentTile = gridArray[x, y];
        currentTile.AddPiece(newPawn);
        newPawn.name = newPawn.name + name;
    }

    private void InitilizePiece(int pieceToSpawn, int x, int y)
    {
        Piece.PieceColor color;
        string name;
        if (y == 7)
        {
            color = Piece.PieceColor.White;
            name = "white";
        }
        else
        {

            color = Piece.PieceColor.Black;
            name = "black";
        }
        Piece newPiece = Instantiate(whitePieces[pieceToSpawn]);
        newPiece.IntializePiece(new Vector2Int(x, y), gridArray[x, y].transform.position, color, this);
        var currentTile = gridArray[x, y];
        currentTile.AddPiece(newPiece);
        newPiece.name = newPiece.name + name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Cast a ray from the mouse's screen position into the 2D scene
            Vector2 mousePos = Mouse.current.position.ReadValue();
            RaycastHit2D hit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(mousePos));

            // Check if the ray hit this specific collider
            if (hit.collider != null)
            {
                var tile = hit.collider.GetComponent<Tile>();
                if (tile != null)
                {
                    TileClicked(tile);
                }
            }
        }
    }

    private void TileClicked(Tile clickedTile)
    {
        Debug.Log(highLightedTiles);
        if (clickedTile.tileOccupied && tileSelected == false)
        {
            if (CheckIfPlayersTurn(clickedTile.occupiedPiece.pieceColor) == false)
            {
                return;
            }


            var legalMoves = clickedTile.occupiedPiece.LegalMoves();
            foreach (var move in legalMoves)
            {
                if (CheckMoveIsInBounds(move))
                {
                    int x = (int)move.x;
                    int y = (int)move.y;
                    gridArray[x, y].HighlightTile();
                    highLightedTiles.Add(gridArray[x, y]);
                }
                else return;
            }

            tileSelected = true;
            selectedPiece = clickedTile.occupiedPiece;
            selectedTile = clickedTile;
            selectedPiece.toggleHighlight();
        }
        else
        {
            if (highLightedTiles.Contains(clickedTile))
            {
                selectedPiece.Move(clickedTile);


                if (!CheckTileIsOccupied(clickedTile.gridPos))
                {
                    Take(clickedTile);
                }

                clickedTile.AddPiece(selectedPiece);
                selectedTile.RemovePiece();
                GameManager.ChangeTurn();

            }
            tileSelected = false;
            for (int x = 0; x < highLightedTiles.Count; x++)
            {
                highLightedTiles[x].HighlightTile();

            }
            highLightedTiles.Clear();
            selectedPiece.toggleHighlight();
            selectedPiece = null;
        }
    }

    private void Take(Tile clickedTile)
    {
        var takePiece = clickedTile.occupiedPiece;
        clickedTile.RemovePiece();
        var graveyardCode = graveyardRef.GetComponent<Graveyard>();
        if (graveyardCode != null)
        {
            graveyardCode.SendToGraveyard(takePiece);
        }
    }

    private bool CheckMoveIsInBounds(Vector2 move)
    {
        if (move.x > width - 1 || move.x < 0 || move.y > height - 1 || move.y < 0)
        {
            return false;
        }


        return true;
    }

    private bool CheckMoveIsntBlocked(Vector2 move)
    {
        int x = (int)move.x;
        int y = (int)move.y;

        if (gridArray[x, y].tileOccupied)
        {
            return false;
        }
        return true;
    }

    public bool CheckIfPlayersTurn(Piece.PieceColor color)
    {
        if (color == Piece.PieceColor.White && GameManager.playerTurn == GameManager.Turn.BlacksTurn)
        {
            return false;
        }
        else if (color == Piece.PieceColor.Black && GameManager.playerTurn == GameManager.Turn.WhitesTurn)
        {
            return false;
        }
        return true;
    }

    public bool CheckTileIsOccupied(Vector2Int move)
    {
        if (gridArray[move.x, move.y].tileOccupied )
        {
            return false; 
        }
        return true; 
    }

    public bool CheckTilePieceColor(Vector2Int move, Piece.PieceColor color)
    {
        if (gridArray[move.x, move.y].tileOccupied && gridArray[move.x, move.y].occupiedPiece.pieceColor == color)
        {
            return false;
        }
        return true;
    }

}
