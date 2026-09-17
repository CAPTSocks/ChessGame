using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Rook : Piece
{
    private static readonly Vector2Int[] Directions =
{
    new Vector2Int(-1, 0), // Left
    new Vector2Int( 1, 0), // Right
    new Vector2Int( 0, 1), // Up
    new Vector2Int( 0,-1), // Down
};

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
