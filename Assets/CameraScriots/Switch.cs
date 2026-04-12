using UnityEngine;

public class Switch : MonoBehaviour
{
    public int switchIndex;
    public GameObject lightObject;
    public SwitchPuzzle puzzle;
    public CameraController cameraController;

    private bool isOn = false;

    private static Switch activeSwitch = null;

    void Start()
    {
        if (cameraController == null)
            cameraController = Camera.main.GetComponent<CameraController>();
    }

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
            Switch clickedSwitch = hit.collider.GetComponentInParent<Switch>();

            if (clickedSwitch != null && clickedSwitch == this)
            {
                Activate();
            }
        }
    }

    void Activate()
    {
        if (!cameraController.IsInteracting())
            return;

        if (activeSwitch == null || activeSwitch != this)
        {
            activeSwitch = this;
            return;
        }

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

        // clear active switch
        if (activeSwitch == this)
            activeSwitch = null;
    }
}