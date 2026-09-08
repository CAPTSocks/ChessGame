using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Queen : Piece
{
    //public Sprite sprite;
    //public PieceColor pColor;

    private static readonly Vector2Int[] Directions =
{
    new Vector2Int(-1, 0), // Left
    new Vector2Int( 1, 0), // Right
    new Vector2Int( 0, 1), // Up
    new Vector2Int( 0,-1), // Down
    new Vector2Int(-1, -1), // LeftUp
    new Vector2Int( 1, -1), // RightUp
    new Vector2Int( -1, 1), // LeftDown
    new Vector2Int( 1,1), // RightDown
};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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

        //if (pieceColor == PieceColor.White)
        //{
        //    spriteRenderer.sprite = whiteSprite;
        //}
        //else
        //{
        //    spriteRenderer.sprite = blackSprite;
        //}
    }

    public override List<Vector2> LegalMoves()
    {
        List<Vector2> legalMovesVectors = new List<Vector2>();
        foreach (var direc in Directions)
        {
            Vector2Int cord = cordinates;
            while (cord.x <= 7 && cord.y <= 7)
            {
                Vector2Int move = cord += direc;

                if (cord.x < 0 || cord.y < 0 || cord.y >= 8 || cord.x >= 8)
                {
                    break;
                }
                if (gridRef.CheckTileIsOccupied(move))
                {
                    legalMovesVectors.Add(move);
                }
                else 
                {
                    if (gridRef.CheckTilePieceColor(move, pieceColor))
                    {
                        legalMovesVectors.Add(move);
                    }
                    break;
                }
            }
        }

        return legalMovesVectors;
    }

    public override void Move(Tile moveTile)
    {
        hasMoved = true;
        cordinates = moveTile.gridPos;
        transform.position = moveTile.transform.position;
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

    }
}
