using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInteraction : MonoBehaviour 
{ public CameraController cameraController; 
    void Update() { 
        // Prevent clicking while already interacting
        if (cameraController.IsInteracting())
            return;
        if (Mouse.current.leftButton.wasPressedThisFrame) 
        { 
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()); 
            RaycastHit hit; if (Physics.Raycast(ray, out hit, 100f)) 
            { 
                if (hit.collider.CompareTag("Interactable"))
                { 
                    cameraController.EnterInteraction(hit.transform); 
                } 
            }
        }
    }
}