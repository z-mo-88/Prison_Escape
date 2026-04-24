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

        // check sequence 
        for (int i = 0; i < playerInput.Count; i++)
        {
            if (playerInput[i] != correctSequence[i])
            {
                StartCoroutine(ResetWithDelay());
                return;
            }
        }

        // correct full sequence
        if (playerInput.Count == correctSequence.Count)
        {
            SolvePuzzle();
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
        yield return new WaitForSeconds(0.6f);

        // PLAY RESET SOUND 
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