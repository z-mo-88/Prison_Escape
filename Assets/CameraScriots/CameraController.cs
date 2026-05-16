using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform player;

    [Header("Zoom Settings")]
    public float zoomDistance = 3f;
    public float zoomHeight = 1.5f;
    public float smoothSpeed = 6f;

    [Header("Rotation Settings")]
    public float rotationSpeed = 60f;
    public float maxHorizontalAngle = 30f;
    public float maxVerticalAngle = 20f;

    private Vector3 offset;
    private Quaternion fixedRotation;

    private bool isInteracting = false;
    private bool useFocusPoint = false;

    public Transform currentTarget;

    private Transform target;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private float currentYaw = 0f;
    private float currentPitch = 0f;

    private Rigidbody playerRb;

    void Start()
    {
        fixedRotation = transform.rotation;
        offset = transform.position - player.position;
        playerRb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ExitInteraction();
        }
    }

    void LateUpdate()
    {
        if (!isInteracting)
        {
            Vector3 desiredPos = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * smoothSpeed);
            transform.rotation = fixedRotation;
        }
        else
        {
            HandleRotation();

            if (useFocusPoint)
            {
                Quaternion rotationOffset = Quaternion.Euler(currentPitch, currentYaw, 0);
                Vector3 offsetDir = rotationOffset * (targetRotation * Vector3.back);
                Vector3 desiredPos = targetPosition + offsetDir * zoomDistance;

                transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * smoothSpeed);

                Quaternion lookRot = Quaternion.LookRotation(targetPosition - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * smoothSpeed);
            }
            else
            {
                Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
                Vector3 offsetDir = rotation * Vector3.back;
                Vector3 desiredPos = targetPosition + offsetDir * zoomDistance + Vector3.up * zoomHeight;

                transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * smoothSpeed);

                Quaternion lookRot = Quaternion.LookRotation(targetPosition - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * smoothSpeed);
            }
        }
    }

    void HandleRotation()
    {
        float inputX = 0f;
        float inputY = 0f;

        if (Keyboard.current.aKey.isPressed) inputX = 1f;
        if (Keyboard.current.dKey.isPressed) inputX = -1f;
        if (Keyboard.current.wKey.isPressed) inputY = 1f;
        if (Keyboard.current.sKey.isPressed) inputY = -1f;

        currentYaw += inputX * rotationSpeed * Time.deltaTime;
        currentPitch -= inputY * rotationSpeed * Time.deltaTime;

        currentYaw = Mathf.Clamp(currentYaw, -maxHorizontalAngle, maxHorizontalAngle);
        currentPitch = Mathf.Clamp(currentPitch, -maxVerticalAngle, maxVerticalAngle);
    }

    public void EnterInteraction(Transform interactTarget)
    {
        if (isInteracting) return;

        currentTarget = interactTarget;
        isInteracting = true;
        target = interactTarget;

        currentYaw = 0f;
        currentPitch = 0f;

        if (PuzzleManager.Instance != null)
            PuzzleManager.Instance.StartPuzzle();

        // Freeze player
        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            playerRb.constraints = RigidbodyConstraints.FreezeAll;
        }

        //Focus point
        Transform focus = target.Find("FocusPoint");
        if (focus != null)
        {
            useFocusPoint = true;
            targetPosition = focus.position;
            targetRotation = focus.rotation;
        }
        else
        {
            useFocusPoint = false;
            Collider col = target.GetComponent<Collider>();
            targetPosition = (col != null) ? col.bounds.center : target.position;
        }

        //  Enable object movement
        MovableObject obj = target.GetComponentInChildren<MovableObject>();
        if (obj != null)
        {
            obj.EnableMove();
        }
    }

    public void ExitInteraction()
    {
        currentTarget = null;

        MovableObject obj = target?.GetComponentInChildren<MovableObject>();

        isInteracting = false;

        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if (obj != null)
        {
            obj.DisableMove();
        }

        target = null;
        useFocusPoint = false;
    }
    
    public bool IsInteracting() => isInteracting;
   
}