using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public Transform holdPoint;        // KeyHoldPoint (in hand)
    public GameObject player;          // The prisoner
    public KeyInventory inventory;     // Player inventory

    private bool playerNear = false;
    private bool picked = false;

    void Update()
    {
        if (playerNear && !picked && Input.GetKeyDown(KeyCode.E))
        {
            PickKey();
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