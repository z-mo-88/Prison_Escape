using UnityEngine;

public class PowerHandle : MonoBehaviour
{
    public ButtonPuzzleManager manager;
    private bool isOn = false;

    void OnMouseDown()
    {
        isOn = !isOn;

        if (isOn)
        {
            transform.position = transform.position + new Vector3(0, -0.5f, 0);
            manager.powerOn = true;
        }
        else
        {
            transform.position = transform.position + new Vector3(0, 0.5f, 0);
            manager.powerOn = false;
            manager.ResetPuzzle();
        }
    }
}