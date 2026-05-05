using UnityEngine;

public class MovableObject : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement")]
    public float moveSpeed = 1f;
    public float maxDistance = 5f;

    private Vector3 startPos;
    private bool canMove = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;

        rb.isKinematic = true;
    }

    void Update()
    {
        if (!canMove) return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0f, z) * moveSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + move);

        // Limit distance
        float dist = Vector3.Distance(startPos, transform.position);

        if (dist > maxDistance)
        {
            Vector3 dir = (transform.position - startPos).normalized;
            transform.position = startPos + dir * maxDistance;
        }
    }

    public void EnableMove()
    {
        canMove = true;
        rb.isKinematic = false;
    }

    public void DisableMove()
    {
        canMove = false;
        rb.isKinematic = true;
        transform.position = startPos; 
    }
}