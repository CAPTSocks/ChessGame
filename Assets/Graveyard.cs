using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Graveyard : MonoBehaviour
{
    [SerializeField]
    private GameObject whiteGraveyard;
    [SerializeField]
    private GameObject blackGraveyard;

    private List<GameObject> whiteGraveyardList = new List<GameObject>();
    private List<GameObject> blackGraveyardList = new List<GameObject>();
    private Vector2 whiteGraveyardPos;
    private Vector2 blackGraveyardPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Called Start in Graveyard");
        whiteGraveyardPos = whiteGraveyard.transform.position;
        blackGraveyardPos = blackGraveyard.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendToGraveyard(Piece takenPiece)
    {
        if (takenPiece.pieceColor == Piece.PieceColor.White)
        {
            
            if (whiteGraveyardList.Count > 0)
            {
                whiteGraveyardPos = whiteGraveyardPos += new Vector2(0, -1);
            }

            takenPiece.transform.position = whiteGraveyardPos;
            takenPiece.transform.SetParent(whiteGraveyard.transform);
            whiteGraveyardList.Add(takenPiece.gameObject);
        }
        else
        {
            if (blackGraveyardList.Count > 0)
            {
                blackGraveyardPos = blackGraveyardPos += new Vector2(0, -1);
            }
            takenPiece.transform.position = blackGraveyardPos;
            takenPiece.transform.SetParent(blackGraveyard.transform);
            blackGraveyardList.Add(takenPiece.gameObject);
        }
        
        if (blackGraveyardList.Count % 7 == 0)
        {
            blackGraveyardPos = new Vector2(blackGraveyardPos.x + 1, blackGraveyard.transform.position.y + 1);
        }

        if (whiteGraveyardList.Count % 7 == 0)
        {
            whiteGraveyardPos = new Vector2(whiteGraveyardPos.x - 1, whiteGraveyard.transform.position.y + 1);
        }
    }

    public void ReturnToBoard()
    {
        // Logic to return pieces from graveyard to the board
    }
}
