using UnityEngine;

public class Interactable : MonoBehaviour
{
    public CameraController cameraController;

    [Header("Interaction")]
    public Transform player;
    public float interactDistance = 3f;

    [HideInInspector]
    public bool isZoomed = false;

    void Start()
    {
        if (cameraController == null && Camera.main != null)
        {
            cameraController = Camera.main.GetComponent<CameraController>();
        }

        // AUTO FIND PLAYER
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");

            if (p != null)
            {
                player = p.transform;
            }
        }
    }

    void OnMouseDown()
    {
        if (cameraController == null || player == null)
            return;

        float distance = Vector3.Distance(player.position, transform.position);

        // TOO FAR
        if (distance > interactDistance)
            return;

        // ENTER ZOOM
        if (!isZoomed)
        {
            cameraController.EnterInteraction(transform);
            isZoomed = true;
        }
    }

    void Update()
    {
        // EXIT ZOOM
        if (isZoomed && Input.GetKeyDown(KeyCode.Escape))
        {
            cameraController.ExitInteraction();
            isZoomed = false;
        }
    }
}