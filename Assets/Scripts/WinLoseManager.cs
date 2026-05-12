using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseManager : MonoBehaviour
{
    // Retry current level
    public void RetryLevel()
    {
        string currentLevel = PlayerPrefs.GetString("CurrentLevel", "Level1");
        SceneManager.LoadScene(currentLevel);
    }

    // Go back to main menu
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu 1");
    }

    // Go to next level
    public void GoToNextLevel()
    {
        int currentLevelNumber = PlayerPrefs.GetInt("CurrentLevelNumber", 1);
        int nextLevelNumber = currentLevelNumber + 1;

        // If player finished Level 5
        if (currentLevelNumber == 5)
        {
            // Open the final escape screen
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            // Load next level normally
            SceneManager.LoadScene("Level" + nextLevelNumber);
        }
    }
}