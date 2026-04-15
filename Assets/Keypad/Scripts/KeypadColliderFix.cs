using UnityEngine;

public class KeypadColliderFix : MonoBehaviour
{
    [Header("References")]
    public Collider mainCollider; 
    public CameraController cameraController;

    void Start()
    {
        if (cameraController == null && Camera.main != null)
            cameraController = Camera.main.GetComponent<CameraController>();
    }

    void Update()
    {
        if (cameraController == null || mainCollider == null)
            return;

        // When zooming disable big collider
        if (cameraController.IsInteracting())
        {
            if (mainCollider.enabled)
                mainCollider.enabled = false;
        }
        // When not zooming enable again
        else
        {
            if (!mainCollider.enabled)
                mainCollider.enabled = true;
        }
    }
}