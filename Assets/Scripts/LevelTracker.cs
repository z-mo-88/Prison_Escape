using UnityEngine;

public class LevelTracker : MonoBehaviour
{
    void Start()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        PlayerPrefs.SetString("CurrentLevel", sceneName);

        int levelNumber = int.Parse(sceneName.Replace("Level", ""));
        PlayerPrefs.SetInt("CurrentLevelNumber", levelNumber);
    }
}