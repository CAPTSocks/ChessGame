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

    public abstract void IntializePiece(Vector2Int spawnCordinate, Vector2 spawnPos, PieceColor color, Grid grid);
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
