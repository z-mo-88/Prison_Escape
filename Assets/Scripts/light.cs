
using UnityEngine;

public class ButtonLight : MonoBehaviour
{
    public Light myLight;

    private bool isOn = false;

    void OnMouseDown()
    {
        isOn = !isOn;
        myLight.enabled = isOn;
    }
}