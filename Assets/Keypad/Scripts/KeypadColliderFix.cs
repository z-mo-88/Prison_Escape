using UnityEngine;

public class KeypadColliderFix : MonoBehaviour
{
    [Header("References")]
    public Collider mainCollider;
    public CameraController cameraController;

    public Light keypadLight;

    void Start()
    {
        if (cameraController == null && Camera.main != null)
            cameraController = Camera.main.GetComponent<CameraController>();

        if (keypadLight != null)
            keypadLight.enabled = false;
    }

    void Update()
    {
        if (cameraController == null || mainCollider == null)
            return;

        // Check if we are currently interacting/zoomed
        bool isInteracting = cameraController.IsInteracting();

        // Handle Collider
        mainCollider.enabled = !isInteracting;

        //  Handle Light Toggle
        if (keypadLight != null)
        {
            // The light is enabled ONLY when isInteracting is true
            keypadLight.enabled = isInteracting;
        }
    }
}