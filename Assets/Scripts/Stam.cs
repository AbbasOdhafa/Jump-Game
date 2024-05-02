using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stam : MonoBehaviour
{
    public Staminabar player1Stamina;
    public Staminabar player2Stamina;

    void Start()
    {
        // Assuming that the player objects are tagged with "Player1" and "Player2" respectively
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");

        // Get the Staminabar component attached to the player objects
        player1Stamina = player1.GetComponent<Staminabar>();
        player2Stamina = player2.GetComponent<Staminabar>();
    }

    void Update()
    {
        // Update and manage stamina for both players
        player1Stamina.UpdateStamina();
        player2Stamina.UpdateStamina();
    }
}
