using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzle : MonoBehaviour
{
    public List<int> correctSequence = new List<int> { 2, 4, 0 };
    private List<int> playerInput = new List<int>();

    public Switch[] switches;
    public GameObject hintGroup;
    public CameraController cameraController;
    public Interactable interactable;

    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip wrongSequenceSound;
    public AudioClip timerExpiredSound;

    [Header("Timer")]
    public float timeLimit = 20f;

    private bool timerRunning = false;
    private Coroutine timerCoroutine;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (hintGroup != null)
            hintGroup.SetActive(false);
    }

    public void RegisterInput(int index)
    {
        // START TIMER ON FIRST SWITCH
        if (!timerRunning)
        {
            timerRunning = true;
            timerCoroutine = StartCoroutine(PuzzleTimer());
        }

        playerInput.Add(index);

        // CHECK WHEN FULL SEQUENCE ENTERED
        if (playerInput.Count == correctSequence.Count)
        {
            CheckSequence();
        }
    }

    void CheckSequence()
    {
        bool isCorrect = true;

        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (playerInput[i] != correctSequence[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            SolvePuzzle();
        }
        else
        {
            StartCoroutine(ResetWithDelay());
        }
    }

    void Update()
    {
        if (cameraController == null ||
            hintGroup == null ||
            interactable == null)
            return;

        if (cameraController.currentTarget == interactable.transform)
        {
            hintGroup.SetActive(true);
        }
        else
        {
            hintGroup.SetActive(false);
        }
    }

    void SolvePuzzle()
    {
        Debug.Log("Puzzle Solved!");

        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }

        timerRunning = false;
        playerInput.Clear();

        PuzzleManager.Instance.SolvePuzzle();
    }

    IEnumerator PuzzleTimer()
    {
        yield return new WaitForSeconds(timeLimit);

        Debug.Log("Time Ran Out!");

        // PLAY TIMEOUT SOUND 
        if (audioSource != null &&
            timerExpiredSound != null)
        {
            audioSource.PlayOneShot(timerExpiredSound);
        }

        playerInput.Clear();

        foreach (Switch s in switches)
        {
            s.ResetSwitch();
        }

        timerRunning = false;
    }

    IEnumerator ResetWithDelay()
    {
        yield return new WaitForSeconds(0.8f);

        // PLAY WRONG SEQUENCE SOUND
        if (audioSource != null &&
            wrongSequenceSound != null)
        {
            audioSource.PlayOneShot(wrongSequenceSound);
        }

        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }

        timerRunning = false;

        playerInput.Clear();

        foreach (Switch s in switches)
        {
            s.ResetSwitch();
        }

        PuzzleManager.Instance.ResetPuzzle();
    }
}