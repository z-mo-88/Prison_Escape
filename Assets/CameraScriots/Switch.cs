using UnityEngine;

public class Switch : MonoBehaviour
{
    public int switchIndex;
    public GameObject lightObject;
    public SwitchPuzzle puzzle;

    private bool isOn = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryClick();
        }
    }

    void TryClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                Activate();
            }
        }
    }

    void Activate()
    {
        if (isOn) return;

        isOn = true;

        if (lightObject != null)
            lightObject.SetActive(true);

        if (puzzle != null)
            puzzle.RegisterInput(switchIndex);
    }

    public void ResetSwitch()
    {
        isOn = false;

        if (lightObject != null)
            lightObject.SetActive(false);
    }
}