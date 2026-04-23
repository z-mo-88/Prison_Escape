using UnityEngine;

public class ButtonPuzzleManager : MonoBehaviour
{
    public PuzzleButton redButton;
    public PuzzleButton greenButton;
    public PuzzleButton blueButton;

    public Transform door;
    public Vector3 openRotation = new Vector3(0f, 90f, 0f);

    private int currentStep = 0;
    private bool puzzleSolved = false;

    public void PressButton(string buttonColor)
    {
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
        door.rotation = Quaternion.Euler(openRotation);
        Debug.Log("Door opened!");
    }

    void ResetPuzzle()
    {
        currentStep = 0;

        redButton.TurnOff();
        greenButton.TurnOff();
        blueButton.TurnOff();

        Debug.Log("Wrong order! Reset.");
    }
}