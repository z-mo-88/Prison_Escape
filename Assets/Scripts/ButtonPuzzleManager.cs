using System.Collections;
using UnityEngine;

public class ButtonPuzzleManager : MonoBehaviour
{
    public PuzzleButtonLevel4 greenButton, redButton, blueButton;
    public SlidingDoorLevel4 door;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip resetSound;

    public bool powerOn = false; // Starts as FALSE
    private int currentStep = 0;
    private bool puzzleSolved = false;

    public void TurnPowerOn()
    {
        powerOn = true;
        currentStep = 0;
    }

    public void PressButton(string color)
    {
        // CRITICAL CHECK: If power is off, the button click is ignored entirely.
        if (!powerOn || puzzleSolved)
        {
            Debug.Log("Buttons are disabled. Power is OFF.");
            return;
        }

        bool correct = false;
        if (currentStep == 0 && color == "Green") { greenButton.TurnOn(); correct = true; }
        else if (currentStep == 1 && color == "Red") { redButton.TurnOn(); correct = true; }
        else if (currentStep == 2 && color == "Blue") { blueButton.TurnOn(); correct = true; }

        if (correct)
        {
            currentStep++;
            if (currentStep == 3) OpenDoor();
        }
        else
        {
            // Wrong button: Reset buttons and play sound, but KEEP powerOn = true.
            StartCoroutine(ResetButtonsOnly());
        }
    }

    public void ResetPuzzle()
    {
        StartCoroutine(ResetButtonsOnly());
    }

    IEnumerator ResetButtonsOnly()
    {
        yield return new WaitForSeconds(0.5f);

        if (audioSource != null && resetSound != null)
            audioSource.PlayOneShot(resetSound);

        currentStep = 0;
        if (greenButton) greenButton.TurnOff();
        if (redButton) redButton.TurnOff();
        if (blueButton) blueButton.TurnOff();
    }

    void OpenDoor()
    {
        puzzleSolved = true;
        if (door) door.OpenDoor();
    }
}