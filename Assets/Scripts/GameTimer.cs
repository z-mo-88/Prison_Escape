using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeRemaining = 60f;
    public bool timerRunning = true;

    [Header("UI")]
    public TMP_Text timerText;

    [Header("Lose Settings")]
    public float loseDelay = 2f;

    void Start()
    {
        UpdateTimerUI();
    }

    void Update()
    {
        if (!timerRunning) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();

            //  BLINK EFFECT WHEN LOW TIME
            if (timeRemaining < 10)
            {
                timerText.color = Color.Lerp(
                    Color.red,
                    Color.white,
                    Mathf.PingPong(Time.time * 5, 1)
                );
            }
            else
            {
                timerText.color = Color.red; // normal color
            }
        }
        else
        {
            timeRemaining = 0;
            timerRunning = false;
            UpdateTimerUI();
            TimeUp();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    void TimeUp()
    {
        Debug.Log("YOU LOST!");

        timerRunning = false;

        StartCoroutine(LoadLoseScreen());
    }

    IEnumerator LoadLoseScreen()
    {
        yield return new WaitForSeconds(loseDelay);

        SceneManager.LoadScene("loseScreen");
    }

    public int GetScore()
    {
        return Mathf.RoundToInt(timeRemaining * 10);
    }
}