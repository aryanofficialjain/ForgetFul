using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [Header("UI GameObjects")]

    private bool canMove = false;  // 👈 Prevents movement until Play
    [SerializeField] private GameObject joystickUI;
    [SerializeField] private GameObject arrowUI;


    [Header("Player Components References")]
    [SerializeField] private Rigidbody2D rb;

    private bool invertControls = false;

    [Header("Player Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float underwaterSpeed = 2f;
    [SerializeField] private float baseJumpPower = 10f;
    [SerializeField] private float boostedJumpPower = 15f;

    [Header("Grounding")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask jumpingLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;

    [Header("Water Settings")]
    [SerializeField] private float waterGravity = 0.3f;
    [SerializeField] private float waterUpwardForce = 3f;

    [Header("UI Button Control")]
    public bool usingUIControls; // Toggle for joystick/UI
    private int uiDirection = 0; // -1 = left, 1 = right, 0 = stop

    private float currentJumpPower;
    private float horizontal;
    private float vertical;
    private bool inWater = false;



    private void Start()
    {
        currentJumpPower = baseJumpPower;

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        invertControls = (sceneIndex == 2); // Invert controls in Level 3
    }

    private void FixedUpdate()
    {

        if (!canMove) return; 
        // If using UI buttons for movement
        if (usingUIControls)
        {
            horizontal = uiDirection;
        }


        if (horizontal > 0)
        {
            transform.localScale = new Vector3((float)0.7499999, transform.localScale.y, transform.localScale.z); // Face right
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3((float)-0.7499999, transform.localScale.y, transform.localScale.z); // Face left
        }

        if (inWater)
        {
            Vector2 velocity = new Vector2(horizontal * underwaterSpeed, 0f);

            if (vertical > 0)
            {
                velocity.y = waterUpwardForce;
            }
            else
            {
                velocity.y = rb.linearVelocity.y - waterGravity;
            }

            rb.linearVelocity = velocity;
        }
        else
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
    }
    #region PLAYER_CONTROLS

    public void Move(InputAction.CallbackContext context)
    {
        if (usingUIControls) return; // Skip if UI buttons are controlling movement

        Vector2 input = context.ReadValue<Vector2>();

        if (invertControls)
        {
            horizontal = -input.x;

            if (inWater)
            {
                vertical = -input.y;
            }
        }
        else
        {
            horizontal = input.x;

            if (inWater)
            {
                vertical = input.y;
            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if ( !canMove || inWater) return;

        if (context.performed && IsGrounded())
        {
            DoJump();
        }
    }

    public void OnJumpButtonPressed() // 👈 For UI Button
    {
        if (!canMove || inWater) return;

        if (IsGrounded())
        {
            DoJump();
        }
    }

    private void DoJump()
    {
        Collider2D hitJumping = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, jumpingLayer);
        Collider2D hitGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (hitJumping != null)
        {
            currentJumpPower = boostedJumpPower;
        }
        else if (hitGround != null)
        {
            currentJumpPower = baseJumpPower;
        }

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, currentJumpPower);
    }

    #endregion

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer | jumpingLayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = true;
            rb.gravityScale = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = false;
            rb.gravityScale = 1f;
        }
    }

    public void UseJoystick(bool isOn)
    {
        if (isOn)
        {
            usingUIControls = false; // Using joystick (hardware input)

            // Enable joystick UI, disable arrow UI
            if (joystickUI != null) joystickUI.SetActive(true);
            if (arrowUI != null) arrowUI.SetActive(false);
        }
    }

    public void UseArrowButtons(bool isOn)
    {
        if (isOn)
        {
            usingUIControls = true; // Using on-screen buttons

            // Enable arrow UI, disable joystick UI
            if (arrowUI != null) arrowUI.SetActive(true);
            if (joystickUI != null) joystickUI.SetActive(false);
        }
    }

    //  UI Button Movement Functions 
    public void MoveLeft() => uiDirection = invertControls ? 1 : -1;
    public void MoveRight() => uiDirection = invertControls ? -1 : 1;
    public void StopMoving() => uiDirection = 0;
    


    public void EnableMovement()
{
    canMove = true;
}
}