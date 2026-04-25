using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
{
    public SlidingDoor door;

    [Header("Timer")]
    public GameTimer timer; 

    [Header("Win Settings")]
    public float winDelay = 2f;

    private bool keypadSolved = false;

    public void KeypadSolved()
    {
        Debug.Log("Keypad solved in Level 2!");
        keypadSolved = true;

        if (timer != null)
            timer.timerRunning = false;

        TryOpenDoor();
    }

    private void TryOpenDoor()
    {
        if (keypadSolved)
        {
            Debug.Log("Level 2 completed!");

            if (door != null)
                door.OpenDoor();
            else
                Debug.LogWarning("Door is not assigned in Level2Manager.");

            Invoke(nameof(LoadWinScreen), winDelay);
        }
    }

    void LoadWinScreen()
    {
        SceneManager.LoadScene("WinScreen");
    }
}