using UnityEngine;
using UnityEngine.InputSystem;

public class Switch : MonoBehaviour
{
    public int switchIndex;
    public GameObject lightObject;
    public SwitchPuzzle puzzle;

    private bool isActivated = false;

    void Update()
    {
        if (!PuzzleManager.Instance.IsActive()) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryClick();
        }
    }

    void TryClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.collider.gameObject == this.gameObject && !isActivated)
            {
                Activate();
            }
        }
    }

    void Activate()
    {
        isActivated = true;

        if (lightObject != null)
            lightObject.SetActive(true);

        puzzle.RegisterInput(switchIndex);
    }

    public void ResetSwitch()
    {
        isActivated = false;

        if (lightObject != null)
            lightObject.SetActive(false);
    }
}