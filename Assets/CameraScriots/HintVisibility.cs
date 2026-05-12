using UnityEngine;

public class HintVisibility : MonoBehaviour
{
    public GameObject hintGroup;
    public CameraController cameraController;

    void Start()
    {
        if (hintGroup != null)
            hintGroup.SetActive(false);
    }

    void Update()
    {
        if (hintGroup == null || cameraController == null)
            return;

        if (cameraController.currentTarget == transform)
        {
            hintGroup.SetActive(true);
        }
        else
        {
            hintGroup.SetActive(false);
        }
    }
}