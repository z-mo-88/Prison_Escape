using System.Collections;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Vector3 openOffset = new Vector3(-3.5f, 0f, 0f);
    public float speed = 1f;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip openSound;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpening = false;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + openOffset;

        // Auto assign AudioSource if missing
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void OpenDoor()
    {
        if (!isOpening)
        {
            isOpening = true;
            StartCoroutine(OpenWithDelay());
        }
    }

    IEnumerator OpenWithDelay()
    {
        // Delay before opening 
        yield return new WaitForSeconds(2f);

        Debug.Log("DOOR OPENED - PLAY SOUND");

        // Play sound
        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or Clip missing!");
        }

        // Start sliding door
        StartCoroutine(SlideDoor());
    }

    IEnumerator SlideDoor()
    {
        while (Vector3.Distance(transform.position, openPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                openPosition,
                speed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = openPosition;
    }
}