using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuButton : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu 1");
    }
}