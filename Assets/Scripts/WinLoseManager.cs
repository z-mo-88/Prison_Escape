using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseManager : MonoBehaviour
{
    public void RetryLevel()
    {
        string currentLevel = PlayerPrefs.GetString("CurrentLevel", "Level1");
        SceneManager.LoadScene(currentLevel);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu 1");
    }

    public void GoToNextLevel()
    {
        int currentLevelNumber = PlayerPrefs.GetInt("CurrentLevelNumber", 1);
        int nextLevelNumber = currentLevelNumber + 1;

        if (nextLevelNumber <= 5)
        {
            SceneManager.LoadScene("Level" + nextLevelNumber);
        }
        else
        {
            SceneManager.LoadScene("MainMenu 1");
        }
    }
}