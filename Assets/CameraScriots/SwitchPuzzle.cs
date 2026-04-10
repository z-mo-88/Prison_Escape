using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzle : MonoBehaviour
{
    public List<int> correctSequence = new List<int> { 2, 5, 1 };
    private List<int> playerInput = new List<int>();

    public Switch[] switches;

    public void RegisterInput(int index)
    {
        playerInput.Add(index);

        for (int i = 0; i < playerInput.Count; i++)
        {
            if (playerInput[i] != correctSequence[i])
            {
                ResetPuzzle();
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
        Debug.Log("Correct!");

        PuzzleManager.Instance.SolvePuzzle();
    }

    void ResetPuzzle()
    {
        Debug.Log("Wrong Reset");

        playerInput.Clear();

        foreach (Switch s in switches)
        {
            s.ResetSwitch();
        }

        PuzzleManager.Instance.ResetPuzzle();
    }
}