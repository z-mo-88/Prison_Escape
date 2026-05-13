using UnityEngine;

public class BreakableScreen : MonoBehaviour
{
    public Transform player;
    public float breakDistance = 3f;

    public GameObject blueScreen;
    public GameObject numberObject;
    public AudioSource breakingSound;
    public CameraController cameraController;

    private bool isBroken = false;


    void Start()
    {

        if (cameraController == null &&
            Camera.main != null)
        {
            cameraController =
                Camera.main.GetComponent<CameraController>();
        }
    }
    void Update()
    {
        if (isBroken)
        {
            HammerPickup.nearBreakableScreen = false;
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);


        if (cameraController != null &&
            cameraController.IsInteracting())
        {
            if (distance <= breakDistance)
            {
                HammerPickup.nearBreakableScreen = true;

                if (Input.GetKeyDown(KeyCode.E) && HammerPickup.hasHammer)
                {
                    BreakScreen();
                }
            }
            else
            {
                HammerPickup.nearBreakableScreen = false;
            }
        }

        void BreakScreen()
        {
            isBroken = true;
            HammerPickup.nearBreakableScreen = false;

            if (breakingSound != null)
                breakingSound.Play();

            if (blueScreen != null)
                blueScreen.SetActive(false);

            if (numberObject != null)
                numberObject.SetActive(true);
        }
    }
}