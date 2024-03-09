using UnityEngine;
using UnityEngine.UI; // For UI elements like Buttons
using TMPro; // If using TextMeshPro

public class LevelUpSystem : MonoBehaviour
{
    public GameObject levelUpPanel; // Assign in the inspector
    public CharacterStats characterStats; // Reference to the CharacterStats script

    void Start()
    {
        // Ensure the level-up panel is not visible at the start
        levelUpPanel.SetActive(false);
    }

    public void LevelUp()
    {
        //pause the game
        Time.timeScale = 0;
        // Activate the level-up panel
        levelUpPanel.SetActive(true);

        // pause the game or other actions as needed
        

        // Here you can also update any UI text to indicate the new level
        // And temporarily halt other game actions if needed
    }

    // Example method for a reward option
    public void IncreaseDamage()
    {
        characterStats.IncreaseBaseAttackDamage(0.1f);
        levelUpPanel.SetActive(false); // Hide the panel after selection
        unpause();
        // Apply other necessary updates
    }
    public void IncreaseSpeed()
    {
        characterStats.moveSpeed += 5; // Adjust value as needed
        levelUpPanel.SetActive(false); // Hide the panel after selection
        unpause();
    }
    public void IncreaseHealth()
    {
        characterStats.IncreaseMaxHealth(25); // Adjust value as needed
        levelUpPanel.SetActive(false); // Hide the panel after selection
        unpause();
    }

    void unpause()
    {
        Time.timeScale = 1;
    }

    // Include similar methods for IncreaseMoveSpeed and IncreaseHealth
}
