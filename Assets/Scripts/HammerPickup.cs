using UnityEngine;

public class HammerPickup : MonoBehaviour
{
    public Transform player;
    public Transform handPoint;
    public float pickupDistance = 10f;

    private bool isPickedUp = false;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;

    public static bool hasHammer = false;

    // This stops the hammer from returning when player is near the breakable screen
    public static bool nearBreakableScreen = false;

    public CameraController cameraController;
    [Header("Audio")]
    public AudioSource pickupAudio;
    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalParent = transform.parent;

        if (cameraController == null &&
    Camera.main != null)
        {
            cameraController =
                Camera.main.GetComponent<CameraController>();
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(
            transform.position,
            player.position);

        //  ONLY allow while zooming/interacting
        if (cameraController != null &&
            cameraController.IsInteracting())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isPickedUp && distance <= pickupDistance)
                {
                    PickUpHammer();
                }
                else if (isPickedUp && !nearBreakableScreen)
                {
                    ReturnHammer();
                }
            }
        }
    }

    void PickUpHammer()
    {
        isPickedUp = true;
        hasHammer = true;

        if (pickupAudio != null)
        {
            pickupAudio.Play();
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        transform.SetParent(handPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    void ReturnHammer()
    {
        isPickedUp = false;
        hasHammer = false;

        transform.SetParent(originalParent);
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = true;
        }
    }
}