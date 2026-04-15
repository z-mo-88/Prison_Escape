using System.Collections;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Vector3 openOffset = new Vector3(-3.5f, 0f, 0f);
    public float speed = 2f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpening = false;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + openOffset;
    }

    public void OpenDoor()
    {
        if (!isOpening)
        {
            isOpening = true;
            StartCoroutine(SlideDoor());
        }
    }

    IEnumerator SlideDoor()
    {
        while (Vector3.Distance(transform.position, openPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                openPosition,
                speed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = openPosition;
    }
}