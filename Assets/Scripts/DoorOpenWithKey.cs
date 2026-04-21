using UnityEngine;

public class DoorOpenWithKey : MonoBehaviour
{
    public GameObject player;
    public KeyInventory inventory;

    public float slideDistance = 3f;
    public float slideSpeed = 2f;

    private bool playerNear = false;
    private bool opening = false;
    private Vector3 closedPosition;
    private Vector3 openPosition;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.left * slideDistance;
    }

    void Update()
    {
        if (playerNear && inventory != null && inventory.hasKey)
        {
            opening = true;
        }

        if (opening)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                openPosition,
                slideSpeed * Time.deltaTime
            );
        }
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