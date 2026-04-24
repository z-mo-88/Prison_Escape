using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public string buttonColor;
    public ButtonPuzzleManager manager;

    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        TurnOff();
    }

    void OnMouseDown()
    {
        manager.PressButton(buttonColor);
    }

    public void TurnOn()
    {
        rend.material.EnableKeyword("_EMISSION");

        if (buttonColor == "Red")
            rend.material.SetColor("_EmissionColor", Color.red * 5f);

        if (buttonColor == "Green")
            rend.material.SetColor("_EmissionColor", Color.green * 5f);

        if (buttonColor == "Blue")
            rend.material.SetColor("_EmissionColor", Color.blue * 5f);
    }

    public void TurnOff()
    {
        rend.material.EnableKeyword("_EMISSION");
        rend.material.SetColor("_EmissionColor", Color.black);
    }
}