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

    private float startTime;

    [Header("Warning Sound")]
    public AudioSource warningAudio;
    public float warningTime = 10f;
    void Start()
    {
        startTime = timeRemaining;

        UpdateTimerUI();
    }

    void Update()
    {
        if (!timerRunning) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();

            // Blink when low time
            if (timeRemaining < warningTime)
            {
                timerText.color = Color.Lerp(
                    Color.red,
                    Color.white,
                    Mathf.PingPong(Time.time * 5, 1)
                );

                // Play warning sound
                if (warningAudio != null && !warningAudio.isPlaying)
                {
                    warningAudio.Play();
                }
            }
            else
            {
                timerText.color = Color.red;

                // Stop sound if time above warning
                if (warningAudio != null && warningAudio.isPlaying)
                {
                    warningAudio.Stop();
                }
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

        timerText.text =
            minutes.ToString("00") + ":" +
            seconds.ToString("00");
    }

    void TimeUp()
    {
        Debug.Log("YOU LOST!");

        timerRunning = false;

        if (warningAudio != null)
        {
            warningAudio.Stop();
        }

        StartCoroutine(LoadLoseScreen());
    }

    IEnumerator LoadLoseScreen()
    {
        yield return new WaitForSeconds(loseDelay);

        SceneManager.LoadScene("loseScreen");
    }

    // SCORE
    public int GetScore()
    {
        return Mathf.RoundToInt(timeRemaining * 10);
    }

    // TIME TAKEN
    public string GetFormattedTimeTaken()
    {
        float timeTaken = startTime - timeRemaining;

        int minutes = Mathf.FloorToInt(timeTaken / 60);
        int seconds = Mathf.FloorToInt(timeTaken % 60);

        return minutes.ToString("00") + ":" +
               seconds.ToString("00");
    }
}