using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public Transform holdPoint;
    public GameObject player;
    public KeyInventory inventory;

    public CameraController cameraController;

    [Header("Audio")]
    public AudioSource pickupAudio;

    private bool playerNear = false;
    private bool picked = false;

    void Start()
    {
        if (cameraController == null && Camera.main != null)
            cameraController = Camera.main.GetComponent<CameraController>();
    }

    void Update()
    {
        if (playerNear && !picked && Input.GetKeyDown(KeyCode.E))
        {
            // Fix: Don't allow picking up a new key if already holding one
            if (inventory != null && inventory.hasKey)
            {
                return;
            }

            if (cameraController != null && cameraController.IsInteracting())
            {
                PickKey();
            }
        }
    }

    void PickKey()
    {
        picked = true;

        inventory.hasKey = true;

        if (pickupAudio != null)
            pickupAudio.Play();

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            playerNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            playerNear = false;
    }
}