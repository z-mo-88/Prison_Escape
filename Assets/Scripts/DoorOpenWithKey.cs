using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorOpenWithKey : MonoBehaviour
{
    public GameObject player;
    public KeyInventory inventory;

    public float slideDistance = 3f;
    public float slideSpeed = 1f;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip openSound;

    [Header("Win")]
    public float winDelay = 5f;

    private bool playerNear = false;
    private bool opening = false;
    private bool soundPlayed = false;
    private bool winStarted = false;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.left * slideDistance;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Start opening
        if (playerNear && inventory != null && inventory.hasKey)
        {
            if (!opening)
            {
                opening = true;

                // Play sound once
                if (!soundPlayed &&
                    audioSource != null &&
                    openSound != null)
                {
                    audioSource.PlayOneShot(openSound);
                    soundPlayed = true;
                }

                // Stop timer
                GameTimer timer =
                    FindFirstObjectByType<GameTimer>();

                if (timer != null)
                {
                    timer.timerRunning = false;

                    // SAVE DATA
                    PlayerPrefs.SetInt(
                        "CompletedPuzzles", 1);

                    PlayerPrefs.SetInt(
                        "FinalScore",
                        timer.GetScore());

                    PlayerPrefs.SetString(
                        "TimeTaken",
                        timer.GetFormattedTimeTaken());
                }

                // Start win screen ONCE
                if (!winStarted)
                {
                    winStarted = true;
                    StartCoroutine(LoadWinScreen());
                }
            }
        }

        // Slide door
        if (opening)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                openPosition,
                slideSpeed * Time.deltaTime
            );
        }
    }

    IEnumerator LoadWinScreen()
    {
        yield return new WaitForSeconds(winDelay);

        SceneManager.LoadScene("WinScreen");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerNear = false;
        }
    }
}