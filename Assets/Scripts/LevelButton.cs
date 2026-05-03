using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int levelIndex; // Set this in the Inspector for each button
    public UnityEngine.UI.Button button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    int unlocked = PlayerPrefs.GetInt("highestLevelUnlocked", 1);
    //    bool isUnlocked = levelIndex <= unlocked;

    //    button.interactable = isUnlocked;
    //    //lockIcon.enabled = !isUnlocked;
    //    //checkmarkIcon.enabled = (levelIndex < unlocked);
    //}

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }
}
