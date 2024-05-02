using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapCol : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public float raycastDistance = 5.0f; // Set the distance of raycast
    Vector3 spawnLocation = new Vector3(0, 0, 0); // Define your spawn location here

    void Update()
    {
        // Create a raycast origin at the top of the game object
        Vector3 raycastOrigin = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

        RaycastHit hit;
        // Cast a ray upwards from the game object's position
        if (Physics.Raycast(raycastOrigin, Vector3.up, out hit, raycastDistance))
        {
            // Check if the raycast hit a player
            if (hit.collider.gameObject.CompareTag("Player1") || hit.collider.gameObject.CompareTag("Player2"))
            {
                Debug.Log(hit.collider.gameObject.tag);

                GameObject player1 = GameObject.FindWithTag("Player1"); // Get player 1
                GameObject player2 = GameObject.FindWithTag("Player2"); // Get player 2

                // Check if player 1 and player 2 exist
                if (player1 != null && player2 != null)
                {
                    // Check if player 1 is strictly above player 2
                    if (player1.transform.position.y > player2.transform.position.y)
                    {
                        // Destroy player 2 and respawn at spawn location
                        Destroy(player2);
                        
                    }
                    // Check if player 2 is strictly above player 1
                    else if (player2.transform.position.y > player1.transform.position.y)
                    {
                        // Destroy player 1 and respawn at spawn location
                        Destroy(player1);
                        
                    }
                    // If neither player is strictly above the other, do nothing
                }
            }
        }
    }
}