using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        if (sceneName == "PauseScene")
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
}