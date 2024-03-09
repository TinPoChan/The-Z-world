using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Image healthBarFiller; // Assign this in the Inspector

    void Start()
    {
        currentHealth = 50;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBarFiller.fillAmount = healthPercentage;
    }
}
