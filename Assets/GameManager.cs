using System;
using UnityEditor.VisionOS;
using UnityEngine;
using UnityEngine.UI;

public static class GameManager
{
    public enum Turn
    {
        WhitesTurn,
        BlacksTurn
    }

    // Property must have at least one accessor (get/set)
    public static Turn playerTurn { get; set; } = Turn.WhitesTurn;

    public static void ChangeTurn()
    {
        if (playerTurn == Turn.WhitesTurn)
        {
            playerTurn = Turn.BlacksTurn;
        }
        else
        {
            playerTurn = Turn.WhitesTurn;
        }

        Debug.Log($"Turn changed to: {playerTurn}");
    }
}
