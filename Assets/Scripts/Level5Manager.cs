using UnityEngine;

public class Level5Manager : MonoBehaviour
{
    public SlidingDoor door;

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
        }
    }
}