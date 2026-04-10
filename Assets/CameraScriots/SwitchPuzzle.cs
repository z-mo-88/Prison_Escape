using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzle : MonoBehaviour
{
    public GameObject hintGroup;

    public float zoomDistance = 2.5f;

    private Transform cam;
    private bool isShowing = false;

    // PUZZLE LOGIC
    public List<int> correctSequence = new List<int> { 2, 5, 1 };
    private List<int> playerInput = new List<int>();

    public Switch[] switches;

    void Start()
    {
        cam = Camera.main.transform;

        if (hintGroup != null)
            hintGroup.SetActive(false);
    }

    void Update()
    {
        if (cam == null) return;

        float distance = Vector3.Distance(cam.position, transform.position);

        if (distance < zoomDistance && !isShowing)
        {
            ShowHints();
        }

        if (distance >= zoomDistance && isShowing)
        {
            HideHints();
        }
    }

    void ShowHints()
    {
        isShowing = true;

        if (hintGroup != null)
            hintGroup.SetActive(true);

        Debug.Log("HINT SHOW");
    }

    void HideHints()
    {
        isShowing = false;

        if (hintGroup != null)
            hintGroup.SetActive(false);

        Debug.Log("HINT HIDE");
    }

    public void RegisterInput(int index)
    {
        playerInput.Add(index);

        for (int i = 0; i < playerInput.Count; i++)
        {
            if (playerInput[i] != correctSequence[i])
            {
                StartCoroutine(ResetWithDelay());
                return;
            }
        }

        if (playerInput.Count == correctSequence.Count)
        {
            SolvePuzzle();
        }
    }

    void SolvePuzzle()
    {
        playerInput.Clear();
        PuzzleManager.Instance.SolvePuzzle();
    }

    IEnumerator ResetWithDelay()
    {
        yield return new WaitForSeconds(0.5f);

        playerInput.Clear();

        foreach (Switch s in switches)
        {
            s.ResetSwitch();
        }

        PuzzleManager.Instance.ResetPuzzle();
    }
}