using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    public Transform playerTransform;
    public float speed = 5f; // Movement speed of the bat
    public int damageAmount = 5; // Damage the bat deals to the player
    public int maxHealth = 20; // Max health for the bat
    private int currentHealth; // Current health of the bat
    public int expValue = 10;

    public GameObject heartPrefab; // Assign this in the Inspector
    public float dropChance = 0.25f; // 25% chance to drop a heart

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth; // Initialize the bat's health

        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * speed;
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
            Die(); // Handle the bat's death
        }
    }

    void Die()
    {
        FindObjectOfType<CharacterStats>().AddExp(expValue);
        // Add death handling logic here (e.g., play death animation)
        TryDropHeart();
        Destroy(gameObject); // Destroy the bat object
    }

    void TryDropHeart()
    {
        if (Random.value < dropChance) // Random.value returns a number between 0 and 1
        {
            Instantiate(heartPrefab, transform.position, Quaternion.identity);
        }
    }
}
