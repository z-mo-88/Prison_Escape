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

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip resetSound;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void RegisterInput(int index)
    {
        playerInput.Add(index);

        // ONLY CHECK AFTER FULL INPUT
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
        if (cameraController == null || hintGroup == null) return;

        if (cameraController.IsInteracting())
        {
            if (!hintGroup.activeSelf)
                hintGroup.SetActive(true);
        }
        else
        {
            if (hintGroup.activeSelf)
                hintGroup.SetActive(false);
        }
    }

    void SolvePuzzle()
    {
        Debug.Log("Puzzle Solved!");

        playerInput.Clear();

        PuzzleManager.Instance.SolvePuzzle();
    }

    IEnumerator ResetWithDelay()
    {
        yield return new WaitForSeconds(0.8f);
        audioSource.PlayOneShot(resetSound);

      
        if (audioSource != null && resetSound != null)
        {
            audioSource.PlayOneShot(resetSound);
        }

        playerInput.Clear();

        foreach (Switch s in switches)
        {
            s.ResetSwitch();
        }

        PuzzleManager.Instance.ResetPuzzle();
    }
}