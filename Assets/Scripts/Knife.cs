using UnityEngine;

public class Knife : MonoBehaviour
{
    public int damage; // The damage value for this knife

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Make sure your enemy GameObjects have the tag "Enemy"
        {
            Debug.Log("Knife hit enemy:" + damage);
            // Use SendMessage to call the TakeDamage method on any script attached to the enemy
            collision.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject); // Destroy the knife after hitting an enemy
        }
    }
}
