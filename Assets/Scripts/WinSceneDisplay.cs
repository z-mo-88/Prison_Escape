using UnityEngine;
using TMPro;

public class WinSceneDisplay : MonoBehaviour
{
    public TMP_Text puzzleText;
    public TMP_Text scoreText;
    public TMP_Text timeText;

    void Start()
    {
        int puzzles =
            PlayerPrefs.GetInt("CompletedPuzzles", 0);

        int score =
            PlayerPrefs.GetInt("FinalScore", 0);

        string time =
            PlayerPrefs.GetString("TimeTaken", "00:00");

        puzzleText.text =
             puzzles + " / 5";

        scoreText.text =
            "" + score;

        timeText.text =
             time;
    }
}