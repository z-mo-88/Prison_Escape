using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour
{
    [Header("Level Buttons")]
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;
    public Button level5Button;

    void Start()
    {
      

        int levelAt = PlayerPrefs.GetInt("levelAt", 1);
        Debug.Log("Level Selection levelAt = " + levelAt);

        level1Button.interactable = true;
        level2Button.interactable = levelAt >= 2;
        level3Button.interactable = levelAt >= 3;
        level4Button.interactable = levelAt >= 4;
        level5Button.interactable = levelAt >= 5;
    }

    public void OpenLevel1()
    {
        SceneManager.LoadScene("StoryScene1");
    }

    public void OpenLevel2()
    {
        if (PlayerPrefs.GetInt("levelAt", 1) >= 2)
            SceneManager.LoadScene("Level2");
    }

    public void OpenLevel3()
    {
        if (PlayerPrefs.GetInt("levelAt", 1) >= 3)
            SceneManager.LoadScene("Level3");
    }

    public void OpenLevel4()
    {
        if (PlayerPrefs.GetInt("levelAt", 1) >= 4)
            SceneManager.LoadScene("Level4");
    }

    public void OpenLevel5()
    {
        if (PlayerPrefs.GetInt("levelAt", 1) >= 5)
            SceneManager.LoadScene("Level5");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Optional: use this only for testing
    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("levelAt");
        PlayerPrefs.Save();
    }
}