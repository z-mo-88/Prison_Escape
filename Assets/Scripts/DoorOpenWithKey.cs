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
        
        if (playerNear && inventory != null && inventory.hasKey)
        {
            if (!opening)
            {
                opening = true;

              
                if (!soundPlayed && audioSource != null && openSound != null)
                {
                    audioSource.PlayOneShot(openSound);
                    soundPlayed = true;
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

            opening = true;

           
            GameTimer timer = FindFirstObjectByType<GameTimer>();
            if (timer != null)
                timer.timerRunning = false;

            StartCoroutine(LoadWinScreen());
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