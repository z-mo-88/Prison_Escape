using UnityEngine;

public class ButtonPuzzleManager : MonoBehaviour
{
    public PuzzleButtonLevel4 greenButton;
    public PuzzleButtonLevel4 redButton;
    public PuzzleButtonLevel4 blueButton;

    public SlidingDoorLevel4 door;

    public bool powerOn = false;

    private int currentStep = 0;
    private bool puzzleSolved = false;

    public void TurnPowerOn()
    {
        powerOn = true;
        ResetPuzzle();

        Debug.Log("Power ON - Buttons are ready");
    }

    public void PressButton(string color)
    {
        if (!powerOn || puzzleSolved) return;

        // First button must be Green
        if (currentStep == 0)
        {
            if (color == "Green")
            {
                greenButton.TurnOn();
                currentStep = 1;
            }

            return; // Red or Blue first = nothing happens
        }

        // Second button must be Red
        if (currentStep == 1)
        {
            if (color == "Red")
            {
                redButton.TurnOn();
                currentStep = 2;
            }
            else
            {
                ResetPuzzle(); // Green then wrong button = all off
            }

            return;
        }

        // Third button must be Blue
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
                ResetPuzzle();
            }
        }
    }

    private void OpenDoor()
    {
        puzzleSolved = true;

        if (door != null)
            door.OpenDoor();

        Debug.Log("Correct order - Door opened");
    }

    public void ResetPuzzle()
    {
        currentStep = 0;

        if (greenButton != null) greenButton.TurnOff();
        if (redButton != null) redButton.TurnOff();
        if (blueButton != null) blueButton.TurnOff();

        Debug.Log("Wrong order - All buttons turned off");
    }
}