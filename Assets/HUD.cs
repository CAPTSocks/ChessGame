using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI turnText;

    void Start()
    {
        GameManager.OnTurnChanged += UpdateTurnText;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
