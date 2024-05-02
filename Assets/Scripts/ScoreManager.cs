using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField]
    private int scorePlayer1 = 0;
    [SerializeField]
    private int scorePlayer2 = 0;

    //score manager
    public void AddScore(PlayerController.PlayerNumber playerNumber, PlayerController.PlayerNumber movedPlayer)
    {
        Debug.Log("AddScore called");

        if (movedPlayer == PlayerController.PlayerNumber.Player1)
        {
            if (playerNumber == PlayerController.PlayerNumber.Player2)
            {
                scorePlayer2++;
                Debug.Log("Player 2 Score: " + scorePlayer2);
            }
        }
        else if (movedPlayer == PlayerController.PlayerNumber.Player2)
        {
            if (playerNumber == PlayerController.PlayerNumber.Player1)
            {
                scorePlayer1++;
                Debug.Log("Player 1 Score: " + scorePlayer1);
            }
        }
    }

}
