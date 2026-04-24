using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPuzzleManager : MonoBehaviour
{
    public PuzzleButton redButton;
    public PuzzleButton greenButton;
    public PuzzleButton blueButton;

    public Transform door;
    public Vector3 openRotation = new Vector3(0f, 90f, 0f);

    public bool powerOn = false;

    [Header("Win Settings")]
    public float winDelay = 2f;

    private int currentStep = 0;
    private bool puzzleSolved = false;

    public void PressButton(string buttonColor)
    {
        if (!powerOn)
        {
            Debug.Log("Power is OFF");
            return;
        }

        if (puzzleSolved) return;

        if (currentStep == 0 && buttonColor == "Red")
        {
            redButton.TurnOn();
            currentStep = 1;
        }
        else if (currentStep == 1 && buttonColor == "Green")
        {
            greenButton.TurnOn();
            currentStep = 2;
        }
        else if (currentStep == 2 && buttonColor == "Blue")
        {
            blueButton.TurnOn();
            currentStep = 3;
            OpenDoor();
        }
        else
        {
            ResetPuzzle();
        }
    }

    void OpenDoor()
    {
        puzzleSolved = true;

        //  Open door
        if (door != null)
            door.rotation = Quaternion.Euler(openRotation);

        Debug.Log("Door Opened");
    }

        // STOP TIMER
      /*  GameTimer timer = FindFirstObjectByType<GameTimer>();
        if (timer != null)
            timer.timerRunning = false;
      
        //  LOAD WIN SCREEN
        StartCoroutine(LoadWinScreen());
    }

    IEnumerator LoadWinScreen()
    {
        yield return new WaitForSeconds(winDelay);
        SceneManager.LoadScene("WinScreen");
    }
      */
    public void ResetPuzzle()
    {
        currentStep = 0;

        if (redButton != null) redButton.TurnOff();
        if (greenButton != null) greenButton.TurnOff();
        if (blueButton != null) blueButton.TurnOff();

        Debug.Log("Puzzle Reset");
    }
}