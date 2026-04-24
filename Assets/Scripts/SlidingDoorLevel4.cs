using UnityEngine;

public class SlidingDoorLevel4 : MonoBehaviour
{
    public Vector3 slideDirection = Vector3.left;
    public float slideDistance = 3f;
    public float speed = 2f;

    private Vector3 startPos;
    private Vector3 targetPos;

    private bool isOpening = false;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + slideDirection.normalized * slideDistance;
    }

    void Update()
    {
        if (isOpening)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                speed * Time.deltaTime
            );
        }
    }

    public void OpenDoor()
    {
        isOpening = true;
        Debug.Log("Door is sliding...");
    }
}