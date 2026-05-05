using UnityEngine;

public class PuzzleButtonLevel4 : MonoBehaviour
{
    public string buttonColor;
    public ButtonPuzzleManager manager;

    public Transform buttonPart;
    public float offY = 0f;
    public float onY = -0.4f;

    public GameObject lightObject;

    public AudioSource audioSource;
    public AudioClip clickSound;

    public CameraController cameraController;

    private bool isOn = false;

    void Start()
    {
        if (cameraController == null && Camera.main != null)
            cameraController = Camera.main.GetComponent<CameraController>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        TurnOff();
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
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            PuzzleButtonLevel4 clicked = hit.collider.GetComponent<PuzzleButtonLevel4>();

            if (clicked != null && clicked == this)
            {
                PressButton();
            }
        }
    }

    void PressButton()
    {
        if (manager == null || !manager.powerOn) return;

        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);

        TurnOn();

        manager.PressButton(buttonColor);
    }

    public void TurnOn()
    {
        isOn = true;

        if (buttonPart != null)
        {
            Vector3 pos = buttonPart.localPosition;
            pos.y = onY;
            buttonPart.localPosition = pos;
        }

        if (lightObject != null)
            lightObject.SetActive(true);
    }

    public void TurnOff()
    {
        isOn = false;

        if (buttonPart != null)
        {
            Vector3 pos = buttonPart.localPosition;
            pos.y = offY;
            buttonPart.localPosition = pos;
        }

        if (lightObject != null)
            lightObject.SetActive(false);
    }
}