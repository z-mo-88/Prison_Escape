using UnityEngine;

public class PuzzleButtonLevel4 : MonoBehaviour
{
    [Header("Button")]
    public string buttonColor;
    public ButtonPuzzleManager manager;

    [Header("Movement")]
    public Transform buttonPart;
    public float offY = 0f;
    public float onY = -0.4f;

    [Header("Light")]
    public GameObject lightObject;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip clickSound;

    [Header("Camera")]
    public CameraController cameraController;

    private bool isOn = false;
    private bool selected = false;

    void Start()
    {
        // Camera
        if (cameraController == null && Camera.main != null)
            cameraController = Camera.main.GetComponent<CameraController>();

        // Audio
        if (audioSource == null)
            audioSource = GetComponentInChildren<AudioSource>();

        // Start OFF
        TurnOff();
    }

    void Update()
    {
        // Only when zoomed
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
            PuzzleButtonLevel4 clicked =
                hit.collider.GetComponentInParent<PuzzleButtonLevel4>();

            if (clicked == this)
            {
                PressButton();
            }
        }
    }

    void PressButton()
    {
        if (manager == null)
            return;

        if (!manager.powerOn)
            return;

        // FIRST CLICK = SELECT
        if (!selected)
        {
            selected = true;
            return;
        }

        // Prevent double press
        if (isOn)
            return;

        // SOUND
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        // TURN ON
        TurnOn();

        // SEND TO MANAGER
        manager.PressButton(buttonColor);
    }

    public void TurnOn()
    {
        isOn = true;

        // Button DOWN
        if (buttonPart != null)
        {
            Vector3 pos = buttonPart.localPosition;
            pos.y = onY;
            buttonPart.localPosition = pos;
        }

        // Light ON
        if (lightObject != null)
            lightObject.SetActive(true);
    }

    public void TurnOff()
    {
        isOn = false;
        selected = false;

        // Button UP
        if (buttonPart != null)
        {
            Vector3 pos = buttonPart.localPosition;
            pos.y = offY;
            buttonPart.localPosition = pos;
        }

        // Light OFF
        if (lightObject != null)
            lightObject.SetActive(false);
    }
}