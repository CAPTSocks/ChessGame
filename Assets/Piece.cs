using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{

    public bool hasMoved = false;
    public Vector2Int cordinates = Vector2Int.zero;
    public Grid gridRef = null;
    public Sprite blackSprite;
    public Sprite whiteSprite;
    public PieceColor pieceColor;

    //Sliding movement variables
    public float moveTime = .25f;
    public bool moving = false;
    public Vector2 startPos = Vector2.zero;
    public Vector2 endPos = Vector2.zero;
    public float elapsedTime = 0f;

    public SpriteRenderer spriteRenderer;

    protected SpriteRenderer highlightSprite;

    public enum PieceColor
    {
        White,
        Black
    }

    public void Start()
    {
        Debug.Log("Piece Start Called");
        
        PickColor();
    }

    public void IntializePiece(Vector2Int spawnCordinate, Vector2 spawnPos, PieceColor color, Grid grid)
    {
        cordinates = spawnCordinate;
        transform.position = spawnPos;
        pieceColor = color;
        gridRef = grid;
        spriteRenderer = GetComponent<SpriteRenderer>();
        highlightSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        highlightSprite.enabled = false;
        PickColor();
    }

    public abstract List<Vector2> LegalMoves();

    public abstract void Move(Tile moveTile);

    public abstract void checkTake();

    public abstract void Take();

    public void PickColor()
    {
        if (pieceColor == PieceColor.White)
        {
            spriteRenderer.sprite = whiteSprite;
        }
        else
        {
            spriteRenderer.sprite = blackSprite;
        }
    }

    public void toggleHighlight()
    {
        highlightSprite.enabled = !highlightSprite.enabled;
    }


}
