using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform player;
    public Vector3 offset;

    [Header("Zoom Settings")]
    public float zoomDistance = 5f;
    public float zoomHeight = 1f;
    public float smoothSpeed = 5f;

    private bool isInteracting = false;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private Quaternion fixedRotation;

    void Start()
    {
        // Save the camera rotation you set in Scene view
        fixedRotation = transform.rotation;

        // Automatically calculate offset (so no manual values needed)
        offset = transform.position - player.position;
    }

    void Update()
    {
        // Press ESC to exit interaction
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ExitInteraction();
        }
    }

    void LateUpdate()
    {
        if (!isInteracting)
        {
            // Follow player (keep isometric view)
            Vector3 desiredPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

            // Keep fixed rotation (important!)
            transform.rotation = fixedRotation;
        }
        else
        {
            // Smooth zoom movement
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
        }
    }


    public void EnterInteraction(Transform target)
    {
        isInteracting = true;

        Transform focus = target.Find("FocusPoint");

        Vector3 targetCenter;
        Vector3 direction;

        if (focus != null)
        {
            // For terminals / screens
            targetCenter = focus.position;

            // VERY IMPORTANT: use screen forward direction
            direction = focus.forward;
        }
        else
        {
            // For switches and normal objects
            targetCenter = target.GetComponent<Collider>().bounds.center;

            direction = (targetCenter - player.position).normalized;
        }

        float distance = 4f;
        float height = 1.5f;

        targetPosition = targetCenter - direction * distance + Vector3.up * height;

        targetRotation = Quaternion.LookRotation(targetCenter - targetPosition);
    }

    public void ExitInteraction()
    {
        isInteracting = false;
    }

    public bool IsInteracting()
    {
        return isInteracting;
    }
}