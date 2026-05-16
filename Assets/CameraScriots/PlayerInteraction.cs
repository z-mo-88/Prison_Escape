using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public CameraController cameraController;
    public float interactDistance = 3f;
    public Transform playerPoint;

    void Update()
    {
        if (!cameraController.IsInteracting() &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Ray ray =
            Camera.main.ScreenPointToRay(
                Mouse.current.position.ReadValue()
            );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            // FIND INTERACTABLE
            Interactable interactable =
                hit.collider.GetComponentInParent<Interactable>();

            if (interactable == null)
                return;

            // DISTANCE CHECK
            float distance = Vector3.Distance(
                playerPoint.position,
                hit.collider.ClosestPoint(playerPoint.position)
            );

            if (distance <= interactDistance)
            {
                // PLAY INTERACT FIRST
                interactable.Interact();
            }
        }
    }
}