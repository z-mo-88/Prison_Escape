using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPuzzleManager : MonoBehaviour
{
    [Header("Buttons")]
    public PuzzleButtonLevel4 greenButton;
    public PuzzleButtonLevel4 redButton;
    public PuzzleButtonLevel4 blueButton;

    [Header("Door")]
    public SlidingDoorLevel4 door;

    [Header("Power")]
    public PowerHandle powerHandle;
    public bool powerOn = false;

    [Header("Win Settings")]
    public float winDelay = 2f;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip wrongSound;

    public FlashEffect flashEffect;

    private string[] correctSequence = { "Green", "Red", "Blue" };

    private string[] playerSequence = new string[3];
    private int inputIndex = 0;

    private bool puzzleSolved = false;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void TurnPowerOn()
    {
        powerOn = true;
        ResetPuzzle();

        Debug.Log("Power ON - Buttons ready");
    }

    public void TurnPowerOff()
    {
        powerOn = false;
        ResetPuzzle();

        Debug.Log("Power OFF - Buttons disabled");
    }

    public void PressButton(string color)
    {
        if (!powerOn || puzzleSolved)
            return;

        playerSequence[inputIndex] = color;
        inputIndex++;

        if (inputIndex >= correctSequence.Length)
        {
            CheckSequence();
        }
    }

    void CheckSequence()
    {
        bool correct = true;

        for (int i = 0; i < correctSequence.Length; i++)
        {
            if (playerSequence[i] != correctSequence[i])
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            OpenDoor();
        }
        else
        {
            StartCoroutine(ResetWithDelay());
        }
    }

    void OpenDoor()
    {
        puzzleSolved = true;

        if (door != null)
            door.OpenDoor();

        Debug.Log("Puzzle Solved!");

        GameTimer timer = FindFirstObjectByType<GameTimer>();

        if (timer != null)
        {
            timer.timerRunning = false;

            PlayerPrefs.SetInt("CompletedPuzzles", 4);
            PlayerPrefs.SetInt("FinalScore", timer.GetScore());
            PlayerPrefs.SetString("TimeTaken", timer.GetFormattedTimeTaken());
        }

        inputIndex = 0;

        StartCoroutine(LoadWinScreen());
    }

    IEnumerator LoadWinScreen()
    {
        yield return new WaitForSeconds(winDelay);

        SceneManager.LoadScene("WinScreen");
    }

    IEnumerator ResetWithDelay()
    {
        yield return new WaitForSeconds(0.6f);

        if (audioSource != null && wrongSound != null)
        {
            audioSource.PlayOneShot(wrongSound);
        }

        if (flashEffect != null)
        {
            flashEffect.WrongFeedback();
        }

        inputIndex = 0;
        playerSequence = new string[3];

        if (greenButton != null)
            greenButton.TurnOff();

        if (redButton != null)
            redButton.TurnOff();

        if (blueButton != null)
            blueButton.TurnOff();

        if (powerHandle != null)
            powerHandle.ResetHandle();

        Debug.Log("Wrong → Reset ALL");
    }

    public void ResetPuzzle()
    {
        inputIndex = 0;
        playerSequence = new string[3];

        if (greenButton != null)
            greenButton.TurnOff();

        if (redButton != null)
            redButton.TurnOff();

        if (blueButton != null)
            blueButton.TurnOff();
    }
}