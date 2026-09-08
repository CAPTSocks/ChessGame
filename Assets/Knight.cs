using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Knight : Piece
{
    private float moveTime = .25f;
    private Vector2 startPos = Vector2.zero;
    private Vector2 firstMove = Vector2.zero;
    private Vector2 secondMove = Vector2.zero;
    private Vector2 endMove = Vector2.zero;
    private float elapsedTime = 1f;
    private bool movedX = false;

    private static readonly Vector2Int[] Directions =
{
    new Vector2Int(-2, 1), // LowUpLeft
    new Vector2Int(-1, 2), // HighUpLeft
    new Vector2Int( 2, 1), // LowUpRight
    new Vector2Int( 1, 2), // HighUpRight
    new Vector2Int(-2, -1), // LowDownLeft
    new Vector2Int(-1, -2), // HighlowLeft
    new Vector2Int( 2, -1), // LowDownRight
    new Vector2Int( 1, -2), // HighDownRight
};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveTime = moveTime / 2;
    }

    public override void IntializePiece(Vector2Int spawnCordiante, Vector2 spawnPos, PieceColor color, Grid grid)
    {
        cordinates = spawnCordiante;
        transform.position = spawnPos;
        pieceColor = color;
        gridRef = grid;
        spriteRenderer = GetComponent<SpriteRenderer>();
        highlightSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        highlightSprite.enabled = false;
        PickColor();
    }

    public override List<Vector2> LegalMoves()
    {
        List<Vector2> legalMovesVectors = new List<Vector2>();
        foreach (var direc in Directions)
        {
            Vector2Int cord = cordinates;

            Vector2Int move = cord += direc;

            if (cord.x < 0 || cord.y < 0 || cord.y >= 8 || cord.x >= 8)
            {
                continue;
            }
            if (gridRef.CheckTileIsOccupied(move))
            {
                legalMovesVectors.Add(move);
            }
            else if (gridRef.CheckTilePieceColor(move, pieceColor))
            {
                legalMovesVectors.Add(move);
                continue;
            }
        }

        return legalMovesVectors;
    }



    public override void Move(Tile moveTile)
    {
        hasMoved = true;
        cordinates = moveTile.gridPos;
        startPos = transform.position;
        firstMove = new Vector2(startPos.x, moveTile.transform.position.y);
        secondMove = moveTile.transform.position;
        endMove = firstMove;
        elapsedTime = 0;
        movedX = false;
        //transform.position = moveTile.transform.position;
    }

    private void HandleSecondMove()
    {
        startPos = transform.position;
        endMove = secondMove;
        elapsedTime = 0; 
    }

    public override void checkTake()
    {
        throw new System.NotImplementedException();
    }

    public override void Take()
    {
        throw new System.NotImplementedException();
    }


    // Update is called once per frame
    void Update()
    {
        if (elapsedTime < moveTime)
        {
            // Increment elapsed time each frame
            elapsedTime += Time.deltaTime;

            // Calculate percentage of completion (0.0 to 1.0)
            float percentageComplete = elapsedTime / moveTime;
            Debug.Log(percentageComplete);
            // Linearly interpolate between the two vectors
            transform.position = Vector2.Lerp(startPos, endMove, percentageComplete);
            
            if (percentageComplete >= 1 && !movedX)
            {
                movedX = true;
                HandleSecondMove();
            }
            
        }
    }
}
