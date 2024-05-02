using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject player1Prefab; // Assign in Inspector
    public GameObject player2Prefab; // Assign in Inspector
    public Transform player1SpawnPoint; // Assign in Inspector
    public Transform player2SpawnPoint; // Assign in Inspector

    private GameObject player1Instance;
    private GameObject player2Instance;

    void Start()
    {
        RespawnPlayer1();
        RespawnPlayer2();
    }

    public void RespawnPlayer1()
    {
        if (player1Instance == null)
        {
            player1Instance = Instantiate(player1Prefab, player1SpawnPoint.position, Quaternion.identity);
        }
    }

    public void RespawnPlayer2()
    {
        if (player2Instance == null)
        {
            player2Instance = Instantiate(player2Prefab, player2SpawnPoint.position, Quaternion.identity);
        }
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
