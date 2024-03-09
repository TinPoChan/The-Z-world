using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage; // Damage dealt by the bullet

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Make sure your enemy GameObjects have the tag "Enemy"
        {
            Debug.Log("Bullet hit enemy:" + damage);
            // Use SendMessage to call the TakeDamage method on any script attached to the enemy
            collision.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject); // Optionally destroy the bullet after hitting an enemy
        }
    }
}
