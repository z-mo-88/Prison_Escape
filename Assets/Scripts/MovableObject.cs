using UnityEngine;

public class MovableObject : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement")]
    public float moveSpeed = 1f;
    public float maxDistance = 5f;

    [Header("Audio")]
    public AudioSource moveAudio;

    private Vector3 startPos;
    private bool canMove = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        startPos = transform.position;

        rb.isKinematic = true;
    }

    void Update()
    {
        if (!canMove) return;

        float x = 0f;
        float z = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
            x = -1f;

        if (Input.GetKey(KeyCode.RightArrow))
            x = 1f;

        if (Input.GetKey(KeyCode.UpArrow))
            z = 1f;

        if (Input.GetKey(KeyCode.DownArrow))
            z = -1f;

        Vector3 move =
            new Vector3(x, 0f, z) *
            moveSpeed *
            Time.deltaTime;

        rb.MovePosition(rb.position + move);

        bool isMoving =
            Mathf.Abs(x) > 0.1f ||
            Mathf.Abs(z) > 0.1f;

        if (moveAudio != null)
        {
            if (isMoving && !moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
            else if (!isMoving && moveAudio.isPlaying)
            {
                moveAudio.Stop();
            }
        }

        // Limit distance
        float dist =
            Vector3.Distance(startPos,
                             transform.position);

        if (dist > maxDistance)
        {
            Vector3 dir =
                (transform.position - startPos)
                .normalized;

            transform.position =
                startPos + dir * maxDistance;
        }
    }

    public void EnableMove()
    {
        canMove = true;

        rb.isKinematic = false;
    }

    public void DisableMove()
    {
        canMove = false;

        rb.isKinematic = true;

        transform.position = startPos;

        // Stop sound
        if (moveAudio != null)
        {
            moveAudio.Stop();
        }
    }
}