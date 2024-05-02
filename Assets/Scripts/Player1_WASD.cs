using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public enum PlayerNumber
    {
        Player1,
        Player2
    }

    public PlayerNumber playerNumber = PlayerNumber.Player1;
    public float speed = 10f;
    public float gravity = -20f;
    public float jumpSpeed = 15f;
    public float dashSpeed = 20f;
    public float dashCooldown = 0.3f;

    private Vector3 velocity;
    private bool isDashing;
    private float nextDashTime;
    private bool isJumping;
    private bool hasJumped;

    public TrailRenderer trail;
    public bool IsPlayerDashing { get { return isDashing; } }
    public bool IsPlayerJumping { get { return isJumping; } }
    public bool IsPlayerGrounded { get; private set; } // Add this property to the PlayerController
    public GameObject staminaBarObject;
    public Slider staminaBar;
    public Staminabar staminabar;
    public PlayerRespawner playerRespawner;
    public LayerMask groundMask;
    public LayerMask whatIsGround;
    public float groundCheckRadius = 0.5f;
    public Transform groundCheck;

    private CharacterController characterController;

    private IEnumerator Dash()
    {
        float originalSpeed = speed;
        speed = dashSpeed;
        isDashing = true;

        if (trail != null)
        {
            trail.emitting = true;
        }

        yield return new WaitForSeconds(0.2f);

        if (trail != null)
        {
            trail.emitting = false;
        }

        speed = originalSpeed;
        isDashing = false;
    }

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        groundCheck = transform.Find("GroundCheck");

        staminabar = GameObject.FindObjectOfType<Staminabar>();
        if (staminabar != null)
        {
            staminaBar = staminabar.staminaBar;
        }
    }

    void Update()
    {
        if (playerNumber == PlayerNumber.Player1)
        {
            HandlePlayer1Movement();
        }
        else
        {
            HandlePlayer2Movement();
        }

        IsPlayerGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
        if (IsPlayerGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        if (IsGrounded())
        {
            isJumping = false;
            hasJumped = false;
        }
    }

    void OnDestroy()
    {
        // Check which player was destroyed and respawn the correct player
        if (playerNumber == PlayerNumber.Player1)
        {
            playerRespawner.Player1Destroyed();
        }
        else if (playerNumber == PlayerNumber.Player2)
        {
            playerRespawner.Player2Destroyed();
        }
    }

    private bool IsGrounded()
    {
        Collider[] colliders = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, whatIsGround);
        return colliders.Length > 0;
    }

    private void HandlePlayer1Movement()
    {
        HandleMovement("Horizontal", "Vertical", KeyCode.Space, KeyCode.LeftShift);
    }

    private void HandlePlayer2Movement()
    {
        HandleMovement("Horizontal2", "Vertical2", KeyCode.N, KeyCode.RightShift);
    }

    private void HandleMovement(string horizontalInput, string verticalInput, KeyCode jumpKey, KeyCode dashKey)
    {
        Vector3 move = new Vector3(Input.GetAxis(horizontalInput), 0, Input.GetAxis(verticalInput));

        if (characterController.isGrounded)
        {
            isJumping = false; // Reset the isJumping flag when the player is grounded
        }

        if (characterController.isGrounded && Input.GetKeyDown(jumpKey))
        {
            if (staminaBar != null && staminaBar.value >= 20)
            {
                velocity.y = jumpSpeed;
                staminaBar.value -= 20; // Decrease stamina by 20 for the jump
                isJumping = true;
            }
        }

        if (Input.GetKeyDown(dashKey) && !isDashing && Time.time >= nextDashTime && (staminaBar == null || staminaBar.value > 10))
        {
            StartCoroutine(Dash());
            nextDashTime = Time.time + dashCooldown;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(move * speed * Time.deltaTime + velocity * Time.deltaTime);
    }

}
