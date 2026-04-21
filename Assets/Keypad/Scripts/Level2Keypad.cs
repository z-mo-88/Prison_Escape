using TMPro;
using UnityEngine;

public class Level2Keypad : MonoBehaviour
{
    [Header("Correct Code")]
    [SerializeField] private string correctCode = "851";

    [Header("Messages")]
    [SerializeField] private string startMessage = "Enter PIN";
    [SerializeField] private string successMessage = "Access Granted";

    [Header("Display")]
    [SerializeField] private TMP_Text displayText;

    [Header("Door")]
    [SerializeField] private Level2Door door;

    private string currentInput = "";

    private void Start()
    {
        if (displayText != null)
            displayText.text = startMessage;
    }

    public void AddInput(string input)
    {
        if (input == "enter")
        {
            CheckCode();
            return;
        }

        if (currentInput.Length >= 3)
            return;

        currentInput += input;

        if (displayText != null)
            displayText.text = currentInput;
    }

    void CheckCode()
    {
        if (currentInput == correctCode)
        {
            Debug.Log("Correct Code!");

            if (displayText != null)
                displayText.text = successMessage;

            if (door != null)
                door.OpenDoor();
        }
        else
        {
            Debug.Log("Wrong Code");

            if (displayText != null)
                displayText.text = "Wrong PIN";
        }

        currentInput = "";
    }
}