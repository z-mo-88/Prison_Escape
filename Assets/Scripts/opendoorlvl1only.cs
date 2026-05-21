using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro; 

public class opendoorlvl1only : MonoBehaviour
{
    public GameObject player;
    public KeyInventory inventory;

    public float slideDistance = 3f;
    public float slideSpeed = 1f;

    [Header("UI Feedback")]
    public TextMeshProUGUI feedbackText; // Drag your UI text here
    public float messageDuration = 2.5f;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip openSound;        // The heavy sliding door sound
    public AudioClip wrongKeySound;    // The jammed/rattling key sound
    public AudioClip correctKeySound;  // The successful unlock/click sound

    [Header("Win")]
    public float winDelay = 5f;

    private bool playerNear = false;
    private bool opening = false;
    private bool soundPlayed = false;
    private bool winStarted = false;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Coroutine feedbackCoroutine;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.left * slideDistance;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerNear && inventory != null && inventory.hasKey)
        {
            inventory.keyAttempts++;

            KeyPickup heldKey = player.GetComponentInChildren<KeyPickup>();
            if (heldKey != null)
            {
                Destroy(heldKey.gameObject);
            }

            inventory.hasKey = false;

            if (inventory.keyAttempts >= 3)
            {
                
                if (!opening)
                {
                    opening = true;

                    if (!soundPlayed && audioSource != null)
                    {
                        // Play the unlock click sound
                        if (correctKeySound != null)
                        {
                            audioSource.PlayOneShot(correctKeySound);
                        }

                        // Play the door sliding open sound
                        if (openSound != null)
                        {
                            audioSource.PlayOneShot(openSound);
                        }

                        soundPlayed = true;
                    }

                    GameTimer timer = FindFirstObjectByType<GameTimer>();
                    if (timer != null)
                    {
                        timer.timerRunning = false;
                        PlayerPrefs.SetInt("CompletedPuzzles", 1);
                        PlayerPrefs.SetInt("FinalScore", timer.GetScore());
                        PlayerPrefs.SetString("TimeTaken", timer.GetFormattedTimeTaken());

                        PlayerPrefs.SetInt("levelAt", 2);

                        PlayerPrefs.Save();
                    }

                    if (!winStarted)
                    {
                        winStarted = true;
                        StartCoroutine(LoadWinScreen());
                    }
                }
            }
            else
            {
                // --- WRONG KEY LOGIC ---
                Debug.Log($"Key dropped! Attempt {inventory.keyAttempts}/3 failed.");

                // Play the jammed key fail sound!
                if (audioSource != null && wrongKeySound != null)
                {
                    audioSource.PlayOneShot(wrongKeySound);
                }

                // Show the "That's not the one!" message on screen
                if (feedbackText != null)
                {
                    if (feedbackCoroutine != null) StopCoroutine(feedbackCoroutine);
                    feedbackCoroutine = StartCoroutine(ShowFeedback("That's not the one!"));
                }
            }
        }

        if (opening)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                openPosition,
                slideSpeed * Time.deltaTime
            );
        }
    }

    // Coroutine to display the failure message temporarily
    IEnumerator ShowFeedback(string message)
    {
        feedbackText.text = message;
        feedbackText.gameObject.SetActive(true);

        yield return new WaitForSeconds(messageDuration);

        feedbackText.gameObject.SetActive(false);
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