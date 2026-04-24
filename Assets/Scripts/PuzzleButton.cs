using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    [Header("Button Settings")]
    public string buttonColor;
    public ButtonPuzzleManager manager;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip clickSound;

    [Header("Camera")]
    public CameraController cameraController;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        if (rend == null)
        {
            Debug.LogError("Renderer missing on " + gameObject.name);
            return;
        }

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (cameraController == null && Camera.main != null)
            cameraController = Camera.main.GetComponent<CameraController>();

        TurnOff();
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
            PuzzleButton clicked = hit.collider.GetComponent<PuzzleButton>();

            if (clicked != null && clicked == this)
            {
                Activate();
            }
        }
    }

    void Activate()
    {
        //  only when zoomed
        if (cameraController != null && !cameraController.IsInteracting())
            return;

        // sound
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        //  send to manager
        if (manager != null)
            manager.PressButton(buttonColor);

        TurnOn();
    }

    public void TurnOn()
    {
        if (rend == null) return;

        rend.material.EnableKeyword("_EMISSION");

        if (buttonColor == "Red")
            rend.material.SetColor("_EmissionColor", Color.red * 5f);
        else if (buttonColor == "Green")
            rend.material.SetColor("_EmissionColor", Color.green * 5f);
        else if (buttonColor == "Blue")
            rend.material.SetColor("_EmissionColor", Color.blue * 5f);
    }

    public void TurnOff()
    {
        if (rend == null) return;

        rend.material.EnableKeyword("_EMISSION");
        rend.material.SetColor("_EmissionColor", Color.black);
    }
}