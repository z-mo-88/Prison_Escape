using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    public CameraController cameraController;
    private PlayerMovement playerMovement;

    [Header("Interaction")]
    public Transform player;
    public float interactDistance = 3f;

    [HideInInspector]
    public bool isZoomed = false;

    private bool isBusy = false;
    private bool canInteract = true;

    void Start()
    {
        if (cameraController == null && Camera.main != null)
        {
            cameraController = Camera.main.GetComponent<CameraController>();
        }

        playerMovement = FindFirstObjectByType<PlayerMovement>();

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

        // BLOCK EXTRA CLICKS
        if (!canInteract || isBusy || isZoomed || cameraController.IsInteracting())
            return;

        float distance = Vector3.Distance(player.position, transform.position);

        // TOO FAR
        if (distance > interactDistance)
            return;

        isBusy = true;
        canInteract = false;

        // PLAY INTERACT ANIMATION
        if (playerMovement != null)
        {
            playerMovement.PlayInteractAnimation();
        }

        StartCoroutine(ZoomDelay());
    }

    IEnumerator ZoomDelay()
    {
        // WAIT FOR INTERACT ANIMATION
        yield return new WaitForSeconds(1f);

        // START ZOOM
        cameraController.EnterInteraction(transform);

        isZoomed = true;

        isBusy = false;

        // SMALL DELAY BEFORE ALLOWING NEXT INTERACTION
        yield return new WaitForSeconds(0.2f);

        canInteract = true;
    }

    void Update()
    {
        // EXIT ZOOM
        if (isZoomed && Input.GetKeyDown(KeyCode.Escape))
        {
            cameraController.ExitInteraction();

            isZoomed = false;

            StartCoroutine(ExitCooldown());
        }
    }

    IEnumerator ExitCooldown()
    {
        canInteract = false;

        yield return new WaitForSeconds(0.5f);

        canInteract = true;
    }
}