using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level5Manager : MonoBehaviour
{
    public SlidingDoor door;

    [Header("Win Settings")]
    public float winDelay = 5f;

    private bool switchSolved = false;
    private bool keypadSolved = false;

    public void SwitchPuzzleSolved()
    {
        switchSolved = true;
        TryOpenDoor();
    }

    public void KeypadSolved()
    {
        keypadSolved = true;
        TryOpenDoor();
    }

    public bool IsSwitchPuzzleSolved()
    {
        return switchSolved;
    }

    private void TryOpenDoor()
    {
        if (switchSolved && keypadSolved)
        {
            Debug.Log("Level 5 completed!");

            if (door != null)
                door.OpenDoor();
            else
                Debug.LogWarning("Door is not assigned in Level5Manager.");

            GameTimer timer = FindFirstObjectByType<GameTimer>();
            if (timer != null)
                timer.timerRunning = false;

            StartCoroutine(LoadWinScreen());
        }
    }

    IEnumerator LoadWinScreen()
    {
        yield return new WaitForSeconds(winDelay);

        SceneManager.LoadScene("WinScreen");
    }
}