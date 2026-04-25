using UnityEngine;

public class PowerHandle : MonoBehaviour
{
    public ButtonPuzzleManager manager;
    public CameraController cameraController;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip clickSound;

    [Header("Movement")]
    public float moveAmount = 0.5f;

    private bool isOn = false;
    private bool selected = false;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;

        if (cameraController == null && Camera.main != null)
            cameraController = Camera.main.GetComponent<CameraController>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
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
        if (cameraController != null && !cameraController.IsInteracting())
            return;

        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            PowerHandle clicked = hit.collider.GetComponentInParent<PowerHandle>();

            if (clicked != null && clicked == this)
            {
                Activate();
            }
        }
    }

    void Activate()
    {
        if (!selected)
        {
            selected = true;
            return;
        }

        isOn = !isOn;

        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);

        if (isOn)
        {
            MoveDown();

            if (manager != null)
                manager.TurnPowerOn();
        }
        else
        {
            MoveUp();

            if (manager != null)
            {
                manager.powerOn = false;
                manager.ResetPuzzle();
            }
        }
    }

    void MoveDown()
    {
        transform.position = startPosition + new Vector3(0, -moveAmount, 0);
    }

    void MoveUp()
    {
        transform.position = startPosition;
    }

    public void ResetHandle()
    {
        isOn = false;
        selected = false;

        MoveUp();

        if (manager != null)
            manager.powerOn = false;
    }
}