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

    private Transform target;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private float currentYaw = 0f;
    private float currentPitch = 0f;

    void Start()
    {
        fixedRotation = transform.rotation;
        offset = transform.position - player.position;
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
            // Follow player
            Vector3 desiredPos = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * smoothSpeed);
            transform.rotation = fixedRotation;
        }
        else
        {
            if (useFocusPoint)
            {
                HandleRotation();

                Quaternion rotationOffset = Quaternion.Euler(currentPitch, currentYaw, 0);

                // Rotate around FocusPoint forward direction
                Vector3 offsetDir = rotationOffset * (targetRotation * Vector3.back);

                Vector3 desiredPos = targetPosition + offsetDir * zoomDistance;

                transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * smoothSpeed);

                // Always look at focus point
                Quaternion lookRot = Quaternion.LookRotation(targetPosition - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * smoothSpeed);
            }
            else
            {
                HandleRotation();

                Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);

                Vector3 center = targetPosition;

                Vector3 offsetDir = rotation * Vector3.back;

                Vector3 desiredPos = center + offsetDir * zoomDistance + Vector3.up * zoomHeight;

                transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * smoothSpeed);

                Quaternion lookRot = Quaternion.LookRotation(center - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * smoothSpeed);
            }
        }
    }

    void HandleRotation()
    {
        float inputX = 0f;
        float inputY = 0f;

        
        if (Keyboard.current.leftArrowKey.isPressed)
            inputX = -1f;

        if (Keyboard.current.rightArrowKey.isPressed)
            inputX = 1f;

        if (Keyboard.current.upArrowKey.isPressed)
            inputY = 1f;

        if (Keyboard.current.downArrowKey.isPressed)
            inputY = -1f;

        currentYaw += inputX * rotationSpeed * Time.deltaTime;
        currentPitch -= inputY * rotationSpeed * Time.deltaTime;

        // Limit rotation
        currentYaw = Mathf.Clamp(currentYaw, -maxHorizontalAngle, maxHorizontalAngle);
        currentPitch = Mathf.Clamp(currentPitch, -maxVerticalAngle, maxVerticalAngle);
    }

    public void EnterInteraction(Transform interactTarget)
    {
        isInteracting = true;
        target = interactTarget;

        currentYaw = 0f;
        currentPitch = 0f;

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

            if (col != null)
                targetPosition = col.bounds.center;
            else
                targetPosition = target.position;
        }
    }

    public void ExitInteraction()
    {
        isInteracting = false;
        target = null;
        useFocusPoint = false;
    }

    public bool IsInteracting()
    {
        return isInteracting;
    }
}