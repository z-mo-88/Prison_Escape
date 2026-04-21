using UnityEngine;

public class Level2Door : MonoBehaviour
{
    [SerializeField] private float slideDistance = 1.5f;
    [SerializeField] private float slideSpeed = 2f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpening = false;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.left * slideDistance;
    }

    void Update()
    {
        if (isOpening)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                openPosition,
                slideSpeed * Time.deltaTime
            );
        }
    }

    public void OpenDoor()
    {
        isOpening = true;
    }
}