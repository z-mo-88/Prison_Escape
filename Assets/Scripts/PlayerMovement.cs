using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private Animator animator;
    private Vector3 movement;

    [Header("Audio")]
    public AudioSource footstepAudio;

    [Header("Reference")]
    public CameraController cameraController; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (rb == null)
            Debug.LogError("Rigidbody is missing on the player!");

        if (animator == null)
            Debug.LogError("Animator is missing on the player!");

        if (cameraController == null && Camera.main != null)
            cameraController = Camera.main.GetComponent<CameraController>();
    }

    void Update()
    {
        if (cameraController != null && cameraController.IsInteracting())
        {
            movement = Vector3.zero;

            if (animator != null)
                animator.SetBool("isWalking", false);

            if (footstepAudio != null && footstepAudio.isPlaying)
                footstepAudio.Stop();

            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        movement = new Vector3(moveX, 0f, moveZ).normalized;

        bool isMoving = movement.magnitude > 0.1f;

        if (animator != null)
            animator.SetBool("isWalking", isMoving);

        if (footstepAudio != null)
        {
            if (isMoving && !footstepAudio.isPlaying)
                footstepAudio.Play();
            else if (!isMoving && footstepAudio.isPlaying)
                footstepAudio.Stop();
        }
    }

    void FixedUpdate()
    {
        if (cameraController != null && cameraController.IsInteracting())
            return;

        if (rb == null) return;

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    public void PlayInteractAnimation()
    {
        if (animator != null)
        {
            animator.ResetTrigger("Interact");
            animator.SetTrigger("Interact");

            animator.SetBool("isWalking", false);
        }
    }

    public bool IsMoving()
{
    return movement.magnitude > 0.1f;
}
}