using UnityEngine;

public class Level5Switch : MonoBehaviour
{

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip clickSound;
    public int switchIndex;
    public bool isCorrectSwitch;
    public GameObject lightObject;
    public Level5SwitchPuzzle puzzle;
    public CameraController cameraController;
    private static Level5Switch selectedSwitch = null;

    [Header("Arm")]
    public Transform arm;
    public float offRotationZ = -32.874f;
    public float onRotationZ = -170f;

    private bool isOn = false;

    void Start()
    {
        if (cameraController == null && Camera.main != null)
            cameraController = Camera.main.GetComponent<CameraController>();

        SetArmRotation(offRotationZ);

        if (lightObject != null)
            lightObject.SetActive(false);
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
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            Level5Switch clickedSwitch = hit.collider.GetComponentInParent<Level5Switch>();

            if (clickedSwitch != null && clickedSwitch == this)
            {
                Activate();
            }
        }
    }

    void Activate()
    {
        if (cameraController != null && !cameraController.IsInteracting())
            return;

        if (selectedSwitch != this)
        {
            selectedSwitch = this;
            return;
        }

        isOn = !isOn;

        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        if (isOn)
        {
            SetArmRotation(onRotationZ);

            if (lightObject != null)
                lightObject.SetActive(true);
        }
        else
        {
            SetArmRotation(offRotationZ);

            if (lightObject != null)
                lightObject.SetActive(false);
        }

        // Wrong switch resets correct ones
        if (!isCorrectSwitch)
        {
            if (puzzle != null)
                puzzle.ResetCorrectSwitchesOnly();

            return;
        }

        if (puzzle != null)
            puzzle.RecountAndCheck();
    }

    void SetArmRotation(float z)
    {
        if (arm == null) return;

        Vector3 rot = arm.localEulerAngles;
        arm.localEulerAngles = new Vector3(rot.x, rot.y, z);
    }

    public void ResetSwitch()
    {
        isOn = false;

        SetArmRotation(offRotationZ);

        if (lightObject != null)
            lightObject.SetActive(false);
    }

    public bool IsOn()
    {
        return isOn;
    }
}