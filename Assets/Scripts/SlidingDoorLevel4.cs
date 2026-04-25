using UnityEngine;

public class SlidingDoorLevel4 : MonoBehaviour
{
    public Vector3 slideDirection = Vector3.left;
    public float slideDistance = 3f;
    public float speed = 1f;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip openSound;

    private Vector3 startPos;
    private Vector3 targetPos;

    private bool isOpening = false;
    private bool soundPlayed = false;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + slideDirection.normalized * slideDistance;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isOpening)
        {
            if (!soundPlayed)
            {
                if (audioSource != null && openSound != null)
                    audioSource.PlayOneShot(openSound);

                soundPlayed = true;
            }

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                speed * Time.deltaTime
            );
        }
    }

    public void OpenDoor()
    {
        if (isOpening) return;

        isOpening = true;
        soundPlayed = false;

        Debug.Log("Door is sliding...");
    }
}