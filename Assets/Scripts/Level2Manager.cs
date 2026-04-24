using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    public SlidingDoor door;

    private bool keypadSolved = false;

    public void KeypadSolved()
    {
        Debug.Log("Keypad solved in Level 2!");
        keypadSolved = true;
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
        }
    }
}