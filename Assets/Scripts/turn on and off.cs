using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    Renderer rend;
    bool isOn = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnMouseDown()
    {
        isOn = !isOn;

        if (isOn)
        {
            rend.material.EnableKeyword("_EMISSION");
            rend.material.SetColor("_EmissionColor", Color.green * 5f);
        }
        else
        {
            rend.material.SetColor("_EmissionColor", Color.black);
        }
    }
}