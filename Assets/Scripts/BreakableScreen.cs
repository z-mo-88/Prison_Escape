using UnityEngine;

public class BreakableScreen : MonoBehaviour
{
    public Transform player;
    public float breakDistance = 3f;

    public GameObject blueScreen;
    public GameObject numberObject;
    public AudioSource breakingSound;

    private bool isBroken = false;

    void Update()
    {
        if (isBroken)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= breakDistance && Input.GetKeyDown(KeyCode.E))
        {
            if (HammerPickup.hasHammer)
            {
                BreakScreen();
            }
        }
    }

    void BreakScreen()
    {
        isBroken = true;

        if (breakingSound != null)
            breakingSound.Play();

        if (blueScreen != null)
            blueScreen.SetActive(false);

        if (numberObject != null)
            numberObject.SetActive(true);
    }
}