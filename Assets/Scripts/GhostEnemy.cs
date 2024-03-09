using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemy : MonoBehaviour
{
    public Transform playerTransform;
    public float speed = 5f;
    public int damageAmount = 10;
    public int maxHealth = 100; // Add a max health for the ghost
    private int currentHealth; // Track the current health of the ghost
    public int expValue = 10;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public GameObject heartPrefab; // Assign this in the Inspector
    public float dropChance = 0.25f; // 25% chance to drop a heart
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth; // Initialize the ghost's health

        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * speed;

        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        float distanceToPlayer = Vector2.Distance(playerTransform.position, transform.position);
        if (distanceToPlayer < 1f)
        {
            rb.velocity = Vector2.zero;
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

    // Method to allow the ghost to take damage
    public void TakeDamage(int damage)
    {
        //Debug.Log("B4" + currentHealth +"Ghost took damage: " + damage);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //Debug.Log("Ghost has died!");
            Die(); // Handle the ghost's death
        }
    }

    void Die()
    {
        FindObjectOfType<CharacterStats>().AddExp(expValue);
        // Add death handling logic here (e.g., play death animation, remove ghost)
        TryDropHeart();
        Destroy(gameObject); // For now, just destroy the ghost
    }

    void TryDropHeart()
    {
        if (Random.value < dropChance) // Random.value returns a number between 0 and 1
        {
            Instantiate(heartPrefab, transform.position, Quaternion.identity);
        }
    }
}
