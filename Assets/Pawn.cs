using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Pawn : Piece
{
    //public Sprite sprite;
   // public PieceColor pColor;
    private float moveTime = .25f;
    private bool moving = false;
    private Vector2 startPos = Vector2.zero;
    private Vector2 endPos = Vector2.zero;
    private float elapsedTime = 0f;

    private static readonly Vector2Int[] AttackDirections =
    {
        new Vector2Int(-1, 1), // LeftUp
        new Vector2Int( 1, 1), // RightUp
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
        var attackTiles = CheckAttackTiles();
        legalMovesVectors.AddRange(attackTiles);
        int moveNum = 1;
        if (pieceColor == PieceColor.White)
            moveNum = -1;
        if (hasMoved == false)
        {
            Vector2Int firstMove = new Vector2Int(cordinates.x, cordinates.y + moveNum);
            if (gridRef.CheckTileIsOccupied(firstMove))
            {
                legalMovesVectors.Add(firstMove);
            }
            else
            {
                return legalMovesVectors;
            }

            Vector2 secondMove = new Vector2(firstMove.x, firstMove.y + moveNum);
            if (gridRef.CheckTileIsOccupied(firstMove))
                legalMovesVectors.Add(secondMove);

        }
        else
        {
            
            Vector2Int firstMove = new Vector2Int(cordinates.x, cordinates.y + moveNum);
            if (gridRef.CheckTileIsOccupied(firstMove))
                legalMovesVectors.Add(firstMove);
        }


        return legalMovesVectors;
    }

    private List<Vector2> CheckAttackTiles()
    {
        List<Vector2> attackTiles = new List<Vector2>();
        int moveNum = 1;
        if (pieceColor == PieceColor.White)
            moveNum = -1;

        foreach (var direc in AttackDirections)
        {
            var adjustedDirec = new Vector2Int(direc.x, direc.y * moveNum);
            Vector2Int cord = cordinates;
            Vector2Int move = cord += adjustedDirec;

            if (move.x < 0 || move.y < 0 || move.y >= 8 || move.x >= 8)
            {
                continue;
            }
            if (!gridRef.CheckTileIsOccupied(move))
            {
                if (gridRef.CheckTilePieceColor(move, pieceColor))
                {
                    attackTiles.Add(move);
                }
            }
        }

        return attackTiles;
    }

    public override void Move(Tile moveTile)
    {
        hasMoved = true;
        cordinates = moveTile.gridPos;
        startPos = transform.position;
        endPos = moveTile.transform.position;
        elapsedTime = 0;
        moving = true; 
        //transform.position = moveTile.transform.position;
        
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
        if (elapsedTime < moveTime && moving == true)
        {
            // Increment elapsed time each frame
            elapsedTime += Time.deltaTime;

            // Calculate percentage of completion (0.0 to 1.0)
            float percentageComplete = elapsedTime / moveTime;

            // Linearly interpolate between the two vectors
            transform.position = Vector2.Lerp(startPos, endPos, percentageComplete);
        }
        else
            moving = false; 
    }
}
