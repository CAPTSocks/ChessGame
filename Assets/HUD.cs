using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI turnText;
    public GameObject pawnPromotionPanel;
    private Piece selectedPromotionPiece = null;

    public Piece SelectedPromotionPiece { get { return selectedPromotionPiece; } private set { selectedPromotionPiece = value; } }

    [SerializeField]
    private List<Piece> promotionPieces = new List<Piece>(); 

    void Start()
    {
        GameManager.OnTurnChanged += UpdateTurnText;
        HidePawnPromotionPanel();
    }

    private void UpdateTurnText()
    {
        if (GameManager.playerTurn == GameManager.Turn.WhitesTurn)
        {
            turnText.text = "White's Turn";
            turnText.color = Color.white;
        }
        else
        {
            turnText.text = "Black's Turn";
            turnText.color = Color.black;
        }
    }

    public void ShowPawnPromotionPanel()
    {
        selectedPromotionPiece = null;
        pawnPromotionPanel.SetActive(true);
    }

    private void HidePawnPromotionPanel()
    {
        pawnPromotionPanel.SetActive(false);
    }

    public void SelectPromotionPiece(int selection)
    {
        Debug.Log(selection);
        switch (selection)
        {
            //Queen
            case 0:
                selectedPromotionPiece = promotionPieces[selection];
                break;
            //Rook
            case 1:
                selectedPromotionPiece = promotionPieces[selection];
                break;
            //Bishop
            case 2:
                selectedPromotionPiece = promotionPieces[selection];
                break;
            //Knight
            case 3:
                selectedPromotionPiece = promotionPieces[selection];
                break;
            default:
                Debug.LogError("Invalid selection for pawn promotion.");
                break;
        }
        HidePawnPromotionPanel();
    }


    private void OnDestroy()
    {
        GameManager.OnTurnChanged -= UpdateTurnText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
