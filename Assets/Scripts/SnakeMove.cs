using UnityEngine;

public class SnakeMove : MonoBehaviour
{
    public float wiggleAmount = 0.15f;
    public float wiggleSpeed = 3f;
    public float forwardAmount = 0.1f;
    public float forwardSpeed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAmount;
        float z = Mathf.Cos(Time.time * forwardSpeed) * forwardAmount;

        transform.position = startPos + new Vector3(x, 0f, z);
    }
}