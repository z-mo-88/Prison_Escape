using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    private CameraController cameraController;
    private PlayerMovement playerMovement;

    [Header("Interaction")]
    private Transform player;

    public float interactDistance = 3f;

    [HideInInspector]
    public bool isZoomed = false;

    private bool isBusy = false;
    private bool canInteract = true;

    void Awake()
    {
       
        if (Camera.main != null)
        {
            cameraController =
                Camera.main.GetComponent<CameraController>();
        }

      
        GameObject playerObj =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;

         
            playerMovement =
                playerObj.GetComponent<PlayerMovement>();
        }
    }

    public void Interact()
    {
        if (cameraController == null || player == null)
        {
            Debug.LogWarning(
                "Missing CameraController or Player."
            );

            return;
        }

        // BLOCK EXTRA INTERACTIONS
        if (!canInteract ||
            isBusy ||
            isZoomed ||
            cameraController.IsInteracting())
        {
            return;
        }

        isBusy = true;
        canInteract = false;

        // PLAY INTERACT ANIMATION FIRST
        if (playerMovement != null)
        {
            playerMovement.PlayInteractAnimation();
        }

        // WAIT THEN START ZOOM
        StartCoroutine(ZoomDelay());
    }

    IEnumerator ZoomDelay()
    {
        // WAIT FOR INTERACT ANIMATION
        yield return new WaitForSeconds(2f);

        if (cameraController == null)
            yield break;

        // START CAMERA INTERACTION
        cameraController.EnterInteraction(transform.root);

        isZoomed = true;

        isBusy = false;

        // SMALL INPUT DELAY
        yield return new WaitForSeconds(0.2f);

        canInteract = true;
    }

    void Update()
    {
        // EXIT INTERACTION
        if (isZoomed && Input.GetKeyDown(KeyCode.Escape))
        {
            if (cameraController != null)
            {
                cameraController.ExitInteraction();
            }

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