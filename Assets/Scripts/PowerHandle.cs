using UnityEngine;

public class PowerHandle : MonoBehaviour
{
    [Header("Handle Bone")]
    public Transform handleBone;

    [Header("Rotation")]
    public float upRotation = 0f;
    public float downRotation = -40f;

    [Header("Manager")]
    public ButtonPuzzleManager manager;

    [Header("Camera")]
    public CameraController cameraController;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip clickSound;

    [Header("Room Light")]
    public GameObject roomLight;

    private bool isOn = false;

    void Start()
    {
        if (cameraController == null && Camera.main != null)
            cameraController = Camera.main.GetComponent<CameraController>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        MoveUp();

        if (roomLight != null)
            roomLight.SetActive(false);

        if (manager != null)
            manager.powerOn = false;
    }

    void Update()
    {
        if (cameraController != null && !cameraController.IsInteracting())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            TryClick();
        }
    }

    void TryClick()
    {
        if (Camera.main == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.collider.transform == transform ||
                hit.collider.transform.IsChildOf(transform))
            {
                Activate();
            }
        }
    }

    void Activate()
    {
        isOn = !isOn;

        // SOUND
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        // TURN ON
        if (isOn)
        {
            MoveDown();

            if (roomLight != null)
                roomLight.SetActive(true);

            if (manager != null)
            {
                manager.powerOn = true;
                manager.ResetPuzzle();
            }
        }

        // TURN OFF
        else
        {
            ResetHandle();
        }
    }

    void MoveDown()
    {
        if (handleBone != null)
        {
            handleBone.localRotation =
                Quaternion.Euler(downRotation, 0f, 0f);
        }
    }

    void MoveUp()
    {
        if (handleBone != null)
        {
            handleBone.localRotation =
                Quaternion.Euler(upRotation, 0f, 0f);
        }
    }

    public void ResetHandle()
    {
        isOn = false;

        MoveUp();

        if (roomLight != null)
            roomLight.SetActive(false);

        if (manager != null)
            manager.powerOn = false;
    }
}