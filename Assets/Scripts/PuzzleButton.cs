using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public string buttonColor;
    public ButtonPuzzleManager manager;

    private Renderer rend;

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
        rend.material.SetColor("_EmissionColor", Color.white * 5f);
    }

    public void TurnOff()
    {
        rend.material.EnableKeyword("_EMISSION");
        rend.material.SetColor("_EmissionColor", Color.black);
    }
}