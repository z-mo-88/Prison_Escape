using UnityEngine;

public class PowerSwitchLevel4 : MonoBehaviour
{
    public Transform switchBone;

    public float offRotation = -18.519f;
    public float onRotation = 160f;

    public ButtonPuzzleManager manager;

    private bool isZoomed = false;
    private bool isOn = false;

    void Start()
    {
        if (switchBone != null)
            switchBone.localRotation = Quaternion.Euler(offRotation, 0f, 0f);
    }

    void OnMouseDown()
    {
        if (!isZoomed)
        {
            isZoomed = true;
            Debug.Log("Zoom first");
            return;
        }

        if (!isOn)
        {
            TurnOnSwitch();
        }
    }

    void TurnOnSwitch()
    {
        isOn = true;

        if (switchBone != null)
            switchBone.localRotation = Quaternion.Euler(onRotation, 0f, 0f);

        if (manager != null)
            manager.TurnPowerOn();

        Debug.Log("Switch ON");
    }
}