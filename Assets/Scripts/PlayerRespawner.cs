using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    public GameObject player1Prefab; // Assign in Inspector
    public GameObject player2Prefab; // Assign in Inspector
    public Transform player1SpawnPoint; // Assign in Inspector
    public Transform player2SpawnPoint; // Assign in Inspector


    private GameObject player1Instance;
    private GameObject player2Instance;

    void Start()
    {

    }

    public void RespawnPlayer1()
    {
        if (player1Instance == null)
        {
            player1Instance = Instantiate(player1Prefab, player1SpawnPoint.position, Quaternion.identity);
            player1Instance.GetComponent<CharacterController>().enabled = true; // Ensure the CharacterController is enabled
            player1Instance.GetComponent<PlayerController>().enabled = true; // Ensure the PlayerController is enabled
        }
        player1Instance.SetActive(true); // Activate the player
    }

    public void RespawnPlayer2()
    {
        if (player2Instance == null)
        {
            player2Instance = Instantiate(player2Prefab, player2SpawnPoint.position, Quaternion.identity);
            player2Instance.GetComponent<CharacterController>().enabled = true; // Ensure the CharacterController is enabled
            player2Instance.GetComponent<PlayerController>().enabled = true; // Ensure the PlayerController is enabled
        }
        player2Instance.SetActive(true); // Activate the player
    }

    public void Player1Destroyed()
    {
        player1Instance = null;
        RespawnPlayer1();
    }

    public void Player2Destroyed()
    {
        player2Instance = null;
        RespawnPlayer2();
    }
}