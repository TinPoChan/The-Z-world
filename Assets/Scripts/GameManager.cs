using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject gameOverPanel; // Assign this in the Inspector

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); bug for some reason
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Ensure there are no duplicate managers
        }
    }

    // Add other game management logic here
    public void TriggerLevelUp()
    {
        Debug.Log("Triggering level up");
        // Assuming LevelUpSystem is attached to the same GameObject or you have a reference to it
        LevelUpSystem levelUpSystem = FindObjectOfType<LevelUpSystem>(); // Find the LevelUpSystem instance in the scene
        if (levelUpSystem != null)
        {
            levelUpSystem.LevelUp(); // Call the LevelUp method to show the level-up options
        }
    }

    // Call this method when the game is over
    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        // Optionally, pause the game or take other actions
        Time.timeScale = 0; // Pause the game
    }

    // Method to restart the game
    public void RestartGame()
    {
        // Ensure the game's time scale is reset, useful if the game was paused
        Time.timeScale = 1;

        // Reloads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Method to exit the game
    public void ExitGame()
    {
        // If running in the Unity Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // If running as a standalone build
        Application.Quit();
        #endif
    }
}
