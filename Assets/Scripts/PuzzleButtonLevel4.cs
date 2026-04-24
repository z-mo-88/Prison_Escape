using UnityEngine;

public class PuzzleButtonLevel4 : MonoBehaviour
{
    public string buttonColor;
    public ButtonPuzzleManager manager;

    public Transform buttonPart;

    public float offY = 0f;
    public float onY = -0.4f;

    public GameObject lightObject;

    private bool isOn = false;

    void Start()
    {
        TurnOff(); // always start OFF
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    PressButton();
                }
            }
        }
    }

    public void PressButton()
    {
        if (manager == null) return;

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