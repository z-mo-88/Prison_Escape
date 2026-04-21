using UnityEngine;

public class Level2KeypadButton : MonoBehaviour
{
    [SerializeField] private string value;
    [SerializeField] private Level2Keypad keypad;

    private void OnMouseDown()
    {
        if (keypad != null && !string.IsNullOrEmpty(value))
        {
            keypad.AddInput(value.Trim().ToLower());
            Debug.Log("Clicked button: " + value);
        }
        else
        {
            Debug.LogWarning("Button is missing Value or Keypad on: " + gameObject.name);
        }
    }
}