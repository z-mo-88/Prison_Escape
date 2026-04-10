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
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            // ONLY interact with objects that have Interactable
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable == null)
                return;

            // Distance check
            float distance = Vector3.Distance(
                playerPoint.position,
                hit.collider.ClosestPoint(playerPoint.position)
            );

            if (distance <= interactDistance)
            {
                cameraController.EnterInteraction(hit.collider.transform.root);
            }
        }
    }
}