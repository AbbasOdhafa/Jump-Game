using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Staminabar : MonoBehaviour
{
    public Slider staminaBar;
    private float stamina = 100;
    private float maxStamina = 100;
    public float regenerationCooldown = 2f; // Time in seconds before stamina starts regenerating
    private float staminaDepletedTime; // Time when stamina was last depleted
    private bool isStaminaDepleted = false; // Flag to track if stamina is depleted
    private PlayerController playerController;
    private bool staminaDecreasedForCurrentJump = false; // Add this line
    public PlayerController player; // Reference to the player movement script
    public RectTransform staminaBarUI; // The UI element of the stamina bar
    public Vector3 offset = new Vector3(0, 2, 0); // The offset to position the stamina bar above the player's head

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController not found in scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController == null)
        {
            playerController = GameObject.FindObjectOfType<PlayerController>();
            if (playerController == null)
            {
                return;
            }
        }

        if (player == null)
        {
            player = GameObject.FindObjectOfType<PlayerController>();
            if (player == null)
            {
                return;
            }
        }

        float playerSpeed = playerController.speed;
        bool isPlayerDashing = playerController.IsPlayerDashing;

        staminaBarUI.position = Camera.main.WorldToScreenPoint(player.transform.position + offset);

        // Decrease stamina when the player is dashing or jumping
        if (player.IsPlayerDashing)
        {
            stamina -= Time.deltaTime * 110; // Adjust the multiplier to control the rate of stamina decrease
            if (stamina <= 0)
            {
                stamina = 0;
                isStaminaDepleted = true; // Set the flag to true when all stamina is used up
                staminaDepletedTime = Time.time; // Record the time when stamina was depleted
            }
        }
        else if (player.IsPlayerJumping)
        {
            Debug.Log("Player is jumping");

            if (!staminaDecreasedForCurrentJump)
            {
                stamina -= 20; // Decrease stamina by 20 for the jump
                if (stamina < 0)
                {
                    stamina = 0;
                }
                staminaDecreasedForCurrentJump = true;
            }
        }
        else
        {
            if (staminaDecreasedForCurrentJump)
            {
                staminaDecreasedForCurrentJump = false; // Reset the flag when the player lands
            }

            if (!isStaminaDepleted)
            {
                stamina += Time.deltaTime * 25; // Adjust the multiplier to control the rate of stamina increase
                if (stamina > maxStamina)
                {
                    stamina = maxStamina;
                }
            }
        }
        UpdateStamina();
    }

    public void UpdateStamina()
    {
        if (!isStaminaDepleted)
        {
            stamina += Time.deltaTime * 25; // Adjust the multiplier to control the rate of stamina increase
            if (stamina > maxStamina)
            {
                stamina = maxStamina;
            }
        }

        staminaBar.value = stamina;
    }

    public float GetStamina()
    {
        return stamina;
    }

    public void DecreaseStamina(float amount)
    {
        stamina -= amount;
        if (stamina < 0)
        {
            stamina = 0;
        }
    }
}