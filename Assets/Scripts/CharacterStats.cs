using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public Image healthBarFiller; // Assign in the inspector
    public LeaderboardManager leaderboardManager; // Reference to the LeaderboardManager script

    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    public float moveSpeed = 5f;

    public float baseAttackDamage = 1f;

    // EXP and Level fields
    public int currentLevel = 1;
    public int currentExp = 0;
    public int expToNextLevel = 100;
    public Image expBarFiller; // Assign in the inspector

    public TMP_Text levelText; // Assign in the inspector

    public int score = 0;
    public TMP_Text scoreText;

    public LevelUpSystem levelUpSystem; // Reference to the LevelUpSystem script

    void Awake()
    {
        // Initialize all stats at the start
        currentLevel = 1;
        currentExp = 0;
        score = 0;
        currentHealth = maxHealth; // Initialize current health to max health at start
        UpdateHealthBarUI();
        UpdateExpBar();
        leaderboardManager = FindObjectOfType<LeaderboardManager>(); // Find the LeaderboardManager instance in the scene
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't drop below 0
        if (currentHealth <= 0)
        {
            Die(); // Handle death
        }
        UpdateHealthBarUI();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't exceed max
        // Optionally, update health bar UI here or via event
        UpdateHealthBarUI();
    }

    private void Die()
    {
        // Handle character death (e.g., disable controls, play death animation)
        // Save the score to the leaderboard
        Debug.Log("Score: " + score);
        leaderboardManager.setPlayerScore(score);
        // Call a method in GameManager to show game over panel
        GameManager.Instance.ShowGameOverPanel();
        Debug.Log("Character has died.");
    }

    public void UpdateHealthBarUI()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBarFiller.fillAmount = healthPercentage;
    }

    public void AddExp(int exp)
    {
        currentExp += exp;
        score += exp; // Add EXP to score as well
        UpdateScoreDisplay();
        while (currentExp >= expToNextLevel)
        {
            LevelUp();
        }
        UpdateExpBar();
    }

    void LevelUp()
    {

        GameManager.Instance.TriggerLevelUp(); // Trigger the level-up panel


        currentExp -= expToNextLevel;
        currentLevel++;
        expToNextLevel = CalculateExpForNextLevel(currentLevel);
        levelText.text = "Level: " + currentLevel;


        // Optionally, increase stats here
    }

    void UpdateExpBar()
    {
        float fillAmount = (float)currentExp / expToNextLevel;
        expBarFiller.fillAmount = fillAmount;
    }

    int CalculateExpForNextLevel(int level)
    {
        // Implement your logic to calculate EXP needed for the next level
        return level * 100; // Example calculation
    }
    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
    // Add more methods to modify other stats as needed

    // Getter for damage
    //public int GetDamage()
    //{
    //    return baseDamage;
    //}

    public void IncreaseBaseAttackDamage(float amount)
    {
        baseAttackDamage += amount;
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth += amount; // Increase current health as well
        UpdateHealthBarUI();
    }
}
