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

    // Start puzzle
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

        if (timer != null)
        {
            int score = timer.GetScore();

            Debug.Log("Score: " + score);

            PlayerPrefs.SetInt("FinalScore", score);
            PlayerPrefs.SetString("TimeTaken", timer.GetFormattedTimeTaken());
        }

        Debug.Log("Puzzle Solved!");

        // SAVE DATA
        PlayerPrefs.SetInt("CompletedPuzzles", 4);

        // UNLOCK LEVEL 5
        PlayerPrefs.SetInt("levelAt", 5);
        PlayerPrefs.Save();

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
        if (flashEffect != null)
            flashEffect.WrongFeedback();

        if (!isActive) return;

        Debug.Log("Puzzle Reset");
    }

    public bool IsActive()
    {
        return isActive;
    }

    public bool IsSolved()
    {
        return isSolved;
    }
}