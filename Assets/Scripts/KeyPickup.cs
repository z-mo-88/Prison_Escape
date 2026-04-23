using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public Transform holdPoint;
    public GameObject player;
    public KeyInventory inventory;

    public CameraController cameraController;

    private bool playerNear = false;
    private bool picked = false;

    void Start()
    {
        // Auto assign camera
        if (cameraController == null && Camera.main != null)
            cameraController = Camera.main.GetComponent<CameraController>();
    }

    void Update()
    {
        //  ADD zoom condition
        if (playerNear && !picked && Input.GetKeyDown(KeyCode.E))
        {
            if (cameraController != null && cameraController.IsInteracting())
            {
                PickKey();
            }
        }
    }

    void PickKey()
    {
        picked = true;

        // Mark that player has the key
        inventory.hasKey = true;

        // Move key to hand
        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // Disable collider
        GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerNear = false;
        }
    }
}