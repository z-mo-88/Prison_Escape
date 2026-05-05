using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPuzzleManager : MonoBehaviour
{
    public PuzzleButtonLevel4 greenButton;
    public PuzzleButtonLevel4 redButton;
    public PuzzleButtonLevel4 blueButton;

    public SlidingDoorLevel4 door;
    public PowerHandle powerHandle;

    public bool powerOn = false;

    [Header("Win Settings")]
    public float winDelay = 2f;

    private int currentStep = 0;
    private bool puzzleSolved = false;

    public void TurnPowerOn()
    {
        powerOn = true;
        ResetPuzzle();

        Debug.Log("Power ON - Buttons ready");
    }

    public void PressButton(string color)
    {
        if (!powerOn || puzzleSolved) return;

        // STEP 0 → GREEN
        if (currentStep == 0)
        {
            if (color == "Green")
            {
                greenButton.TurnOn();
                currentStep = 1;
            }
            else
            {
                StartCoroutine(ResetWithDelay());
            }
            return;
        }

        // STEP 1 → RED
        if (currentStep == 1)
        {
            if (color == "Red")
            {
                redButton.TurnOn();
                currentStep = 2;
            }
            else
            {
                StartCoroutine(ResetWithDelay());
            }
            return;
        }

        // STEP 2 → BLUE
        if (currentStep == 2)
        {
            if (color == "Blue")
            {
                blueButton.TurnOn();
                currentStep = 3;
                OpenDoor();
            }
            else
            {
                StartCoroutine(ResetWithDelay());
            }
        }
    }

    void OpenDoor()
    {
        puzzleSolved = true;

        if (door != null)
            door.OpenDoor();

        Debug.Log("Puzzle Solved!");

        GameTimer timer = FindFirstObjectByType<GameTimer>();
        if (timer != null)
            timer.timerRunning = false;

        StartCoroutine(LoadWinScreen());
    }

    IEnumerator LoadWinScreen()
    {
        yield return new WaitForSeconds(winDelay);
        SceneManager.LoadScene("WinScreen");
    }

    IEnumerator ResetWithDelay()
    {
        yield return new WaitForSeconds(0.6f);

        currentStep = 0;

        greenButton.TurnOff();
        redButton.TurnOff();
        blueButton.TurnOff();

        if (powerHandle != null)
            powerHandle.ResetHandle();

        Debug.Log("Wrong → Reset ALL");
    }

    public void ResetPuzzle()
    {
        currentStep = 0;

        if (greenButton != null) greenButton.TurnOff();
        if (redButton != null) redButton.TurnOff();
        if (blueButton != null) blueButton.TurnOff();
    }
}