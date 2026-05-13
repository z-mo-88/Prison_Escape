using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    public FlashEffect flashEffect;

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

        GameTimer timer = FindFirstObjectByType<GameTimer>();

        int score = timer.GetScore();

        Debug.Log("Score: " + score);

        Debug.Log("Puzzle Solved!");

        // SAVE DATA
        PlayerPrefs.SetInt("CompletedPuzzles", 3);

        PlayerPrefs.SetInt("FinalScore", score);

        PlayerPrefs.SetString(
            "TimeTaken",
            timer.GetFormattedTimeTaken()
        );

        // OPEN DOOR
        if (door != null)
        {
            door.OpenDoor();
        }
        else
        {
            Debug.LogWarning("Door is not assigned in PuzzleManager!");
        }

        StartCoroutine(LoadWinScreen());
    }

    IEnumerator LoadWinScreen()
    {
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("WinScreen");
    }

    // Called when player makes mistake
    public void ResetPuzzle()
    {
        flashEffect.WrongFeedback();

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