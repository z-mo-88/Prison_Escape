using System.Collections;
using UnityEngine;

public class Switch : MonoBehaviour
{

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip clickSound;
    public int switchIndex;
    public GameObject lightObject;
    public SwitchPuzzle puzzle;
    public CameraController cameraController;

    [Header("Arm Movement")]
    public Transform arm; 
    public float offRotation = -40f;
    public float onRotation = -10f;
    public float moveSpeed = 5f;

    private bool isOn = false;
    private static Switch selectedSwitch = null;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (cameraController == null && Camera.main != null)
            cameraController = Camera.main.GetComponent<CameraController>();

        if (lightObject != null)
            lightObject.SetActive(false);

        // Set initial OFF position
        if (arm != null)
        {
            Vector3 rot = arm.localEulerAngles;
            rot.x = offRotation;
            arm.localEulerAngles = rot;
        }
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

        if (Physics.SphereCast(ray, 0.2f, out hit, 100f))
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
        if (cameraController != null && !cameraController.IsInteracting())
            return;

        
        if (selectedSwitch != this)
        {
            selectedSwitch = this;
            return;
        }

        
        if (isOn)
            return;

        isOn = true;

      
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        if (lightObject != null)
            lightObject.SetActive(true);

        if (puzzle != null)
            puzzle.RegisterInput(switchIndex);

        // Move arm DOWN
        if (arm != null)
            StartCoroutine(MoveArm(onRotation));
    }

    IEnumerator MoveArm(float targetX)
    {
        while (true)
        {
            Vector3 current = arm.localEulerAngles;

            float newX = Mathf.LerpAngle(current.x, targetX, Time.deltaTime * moveSpeed);

            if (Mathf.Abs(Mathf.DeltaAngle(newX, targetX)) < 0.5f)
            {
                current.x = targetX;
                arm.localEulerAngles = current;
                break;
            }

            current.x = newX;
            arm.localEulerAngles = current;

            yield return null;
        }
    }

    public void ResetSwitch()
    {
        isOn = false;

        if (lightObject != null)
            lightObject.SetActive(false);

        // Move arm UP (OFF)
        if (arm != null)
            StartCoroutine(MoveArm(offRotation));

        if (selectedSwitch == this)
            selectedSwitch = null;
    }
}