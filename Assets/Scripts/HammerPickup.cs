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
            if (!isPickedUp)
            {
                TryPickUpHammer();
            }
            else if (!nearBreakableScreen)
            {
                ReturnHammer();
            }
        }
    }

    void TryPickUpHammer()
    {
        if (nearBreakableScreen)
        {
            Debug.Log("You cannot pick up the hammer from the glass area.");
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > pickupDistance)
        {
            Debug.Log("You are too far from the hammer.");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupDistance))
        {
            if (hit.transform == transform || hit.transform.IsChildOf(transform))
            {
                PickUpHammer();
            }
            else
            {
                Debug.Log("Look at the hammer first.");
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