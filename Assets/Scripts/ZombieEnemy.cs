using UnityEngine;

public class ZombieEnemy : MonoBehaviour
{

    public Transform playerTransform; // Assign this via the Inspector or find it dynamically in Start()
    public float speed = 5f; // Movement speed of the bat

    public int damageAmount = 10;
    public int maxHealth = 100; // Add a max health for the ghost
    private int currentHealth; // Track the current health of the ghost

    public int expValue = 10;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public GameObject heartPrefab; // Assign this in the Inspector
    public float dropChance = 0.25f; // 25% chance to drop a heart

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth; // Initialize the zombie's health

        // Optionally find the player's transform dynamically if not assigned
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate direction towards the player
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // Move the zombie towards the player
        rb.velocity = direction * speed;

        // Check if the zombie is moving left or right, and flip the sprite accordingly
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true; // Flip the sprite to face left
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false; // Keep the sprite facing right
        }

        // Implement a minimum distance to prevent flickering when too close to the player
        float distanceToPlayer = Vector2.Distance(playerTransform.position, transform.position);
        if (distanceToPlayer < 1f) // Assume 1f as the minimum distance; adjust as needed
        {
            rb.velocity = Vector2.zero; // Stop moving if too close
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            var playerStats = collider.GetComponent<CharacterStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damageAmount);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die(); // Handle the zombie's death
        }
    }

    void Die()
    {
        FindObjectOfType<CharacterStats>().AddExp(expValue);
        // Implement death logic here (e.g., play death animation)
        TryDropHeart();
        Destroy(gameObject); // Destroy the zombie object
    }

    void TryDropHeart()
    {
        if (Random.value < dropChance) // Random.value returns a number between 0 and 1
        {
            Instantiate(heartPrefab, transform.position, Quaternion.identity);
        }
    }
}
