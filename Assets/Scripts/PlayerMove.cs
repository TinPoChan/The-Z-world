using UnityEngine;

// Ensures that a Rigidbody2D component is attached to the GameObject this script is attached to.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    private CharacterStats characterStats;

    private Rigidbody2D rb;
    private Vector2 movementVector;
    //private float speed = 10f; // Speed of the player
    private float moveSpeed;

    // Optional: Remove if not needed
    public float lastHorizontalVector;
    public float lastVerticalVector;

    // Assuming 'Animate' is a custom script handling animations. Please adjust as per your actual implementation.
    private Animate animate;

    private void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        moveSpeed = characterStats.moveSpeed;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animate>();
    }

    private void Update()
    {
        moveSpeed = characterStats.moveSpeed;
        ProcessInputs();
        Move();
        UpdateAnimation();
    }

    private void ProcessInputs()
    {
        // Convert mouse position to world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate direction vector from player to mouse position
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        float distanceToMouse = direction.magnitude;

        // Threshold below which the character doesn't move or change direction
        float movementThreshold = 1f; // Adjust this value as needed

        if (distanceToMouse > movementThreshold)
        {
            // Dynamic speed based on distance to mouse cursor
            float speedMultiplier = Mathf.Clamp(distanceToMouse / 5f, 0.5f, 2f);

            // Update movement vector with dynamic speed
            movementVector = direction.normalized * moveSpeed * speedMultiplier;
        }
        else
        {
            // Close to cursor, reduce movement to avoid flickering
            movementVector = Vector2.zero;
        }

        // Store last non-zero movement vectors for animation purposes
        if (movementVector.x != 0) lastHorizontalVector = movementVector.x;
        if (movementVector.y != 0) lastVerticalVector = movementVector.y;
    }



    private void Move()
    {
        // Apply movement vector to Rigidbody2D to move the player
        rb.velocity = movementVector;
    }

    private void UpdateAnimation()
    {
        // Update animation component, assuming 'horizontal' represents animation direction
        if (animate != null)
        {
            animate.horizontal = movementVector.x;
            // Add more animation updates here if necessary
        }
    }
}
