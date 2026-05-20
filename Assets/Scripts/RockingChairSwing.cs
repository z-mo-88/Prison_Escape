using UnityEngine;

public class RockingChairSwing : MonoBehaviour
{
    [Header("Swing Settings")]
    public float backAngle = -90f;
    public float frontAngle = -85f;
    public float speed = 1.5f;

    private float originalY;
    private float originalZ;

    void Start()
    {
        originalY = transform.eulerAngles.y;
        originalZ = transform.eulerAngles.z;
    }

    void Update()
    {
        float xAngle = Mathf.Lerp(backAngle, frontAngle, (Mathf.Sin(Time.time * speed) + 1f) / 2f);

        transform.rotation = Quaternion.Euler(xAngle, originalY, originalZ);
    }
}