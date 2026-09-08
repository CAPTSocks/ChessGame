using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer sprite;
    private SpriteRenderer defaultSprite;
    public bool tileOccupied = false;
    public Piece occupiedPiece = null;
    private Grid gridRef = null;
    private bool highlighted = false;
    public Vector2Int gridPos = Vector2Int.zero;
    
    [SerializeField]
    private SpriteRenderer highlightSprite = null;
    [SerializeField]
    private Color highlightColor = Color.orangeRed;
    [SerializeField]
    private Color highlightColorOccupied = Color.darkRed;
    private Color defaultColor = Color.white;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        defaultSprite = sprite;

    }

    public void SetupTile(Vector2Int newGridPos, SpriteRenderer spriteRef, Grid grid)
    {
        gridPos = newGridPos;
        sprite.color = spriteRef.color;
        defaultColor = spriteRef.color;
        gridRef = grid;
    }

    public void AddPiece(Piece piece)
    {
        occupiedPiece = piece;
        tileOccupied = true; 
    }

    public void RemovePiece()
    {
        occupiedPiece = null;
        tileOccupied = false;
    }

    public void HighlightTile()
    {
        if (highlighted)
        {
            highlightSprite.gameObject.SetActive(false);
            highlighted = false;
        }
        else 
        {
            if (occupiedPiece == null)
            {
                highlightSprite.color = highlightColor;
            }
            else
            {
                highlightSprite.color = highlightColorOccupied;
            }
            highlightSprite.gameObject.SetActive(true);
            highlighted = true;
        }
    }
}
