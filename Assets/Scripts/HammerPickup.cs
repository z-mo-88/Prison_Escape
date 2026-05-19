using UnityEngine;

public class HammerPickup : MonoBehaviour
{
    public Transform player;
    public Transform handPoint;
    public float pickupDistance = 3f;

    private bool isPickedUp = false;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;

    public static bool hasHammer = false;
    public static bool nearBreakableScreen = false;

    public CameraController cameraController;

    [Header("Audio")]
    public AudioSource pickupAudio;

    void Start()
    {
        hasHammer = false;
        nearBreakableScreen = false;
        isPickedUp = false;

        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalParent = transform.parent;

        if (cameraController == null && Camera.main != null)
        {
            cameraController = Camera.main.GetComponent<CameraController>();
        }
    }

    void Update()
    {
        if (cameraController == null || !cameraController.IsInteracting())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (!isPickedUp)
            {
                if (distance <= pickupDistance && !nearBreakableScreen)
                {
                    PickUpHammer();
                }
                else
                {
                    Debug.Log("You must be near the hammer to pick it up.");
                }
            }
            else
            {
                if (!nearBreakableScreen)
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