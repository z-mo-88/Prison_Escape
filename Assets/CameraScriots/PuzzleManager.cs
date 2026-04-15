using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    private bool isActive = false;
    private bool isSolved = false;

    [Header("Door")]
    public SlidingDoor door; 

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Start puzzle (called when camera zooms)
    public void StartPuzzle()
    {
        isActive = true;
        isSolved = false;

        Debug.Log("Puzzle Started");
    }

    // Called when puzzle is completed
    public void SolvePuzzle()
    {
        if (!isActive) return;

        isSolved = true;
        isActive = false;

        Debug.Log("Puzzle Solved!");

        // OPEN DOOR
        if (door != null)
        {
            door.OpenDoor();
        }
        else
        {
            Debug.LogWarning("Door is not assigned in PuzzleManager!");
        }
    }

    // Called when player makes mistake
    public void ResetPuzzle()
    {
        if (!isActive) return;

        Debug.Log("Puzzle Reset");

        // Puzzle scripts will reset themselves
    }

    // Check if puzzle is active
    public bool IsActive()
    {
        return isActive;
    }

    // Check if solved
    public bool IsSolved()
    {
        return isSolved;
    }
}