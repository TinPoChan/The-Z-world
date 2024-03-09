using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public int healAmount = 20; // Amount of health to restore

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Make sure your player GameObject has the tag "Player"
        {
            CharacterStats characterStats = collision.GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                characterStats.Heal(healAmount);
                Destroy(gameObject); // Destroy the heart object after picking it up
            }
        }
    }
}
