using UnityEngine;
using System.Collections;

public class BreakableScreen : MonoBehaviour
{
    public Transform player;
    public float breakDistance = 3f;

    public GameObject blueScreen;
    public GameObject numberObject;

    public AudioSource breakingSound;
    public AudioClip scarySound;

    public CameraController cameraController;

    private bool isBroken = false;

    void Start()
    {
        isBroken = false;

        if (cameraController == null && Camera.main != null)
        {
            cameraController = Camera.main.GetComponent<CameraController>();
        }

        if (numberObject != null)
        {
            numberObject.SetActive(false);
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

        if (cameraController != null && cameraController.IsInteracting() && distance <= breakDistance)
        {
            HammerPickup.nearBreakableScreen = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (HammerPickup.hasHammer)
                {
                    BreakScreen();
                }
                else
                {
                    Debug.Log("You need to pick up the hammer first!");
                }
            }
        }
        else
        {
            HammerPickup.nearBreakableScreen = false;
        }
    }

    void BreakScreen()
    {
        if (!HammerPickup.hasHammer)
        {
            Debug.Log("Cannot break the glass without hammer.");
            return;
        }

        isBroken = true;
        HammerPickup.nearBreakableScreen = false;

        if (blueScreen != null)
        {
            blueScreen.SetActive(false);
        }

        if (numberObject != null)
        {
            numberObject.SetActive(true);
        }

        StartCoroutine(PlaySounds());
    }

    IEnumerator PlaySounds()
    {
        if (breakingSound != null)
        {
            breakingSound.Play();

            if (breakingSound.clip != null)
            {
                yield return new WaitForSeconds(breakingSound.clip.length);
            }
        }

        if (scarySound != null)
        {
            AudioSource.PlayClipAtPoint(scarySound, transform.position);
        }
    }
}