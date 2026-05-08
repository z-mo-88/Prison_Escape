using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashEffect : MonoBehaviour
{
    public Image flashImage;
    public float flashDuration = 0.2f;

    public AudioSource errorAudio;

    public void WrongFeedback()
    {
        Debug.Log("Wrong Feedback Triggered");

        errorAudio.Play();
        StartCoroutine(FlashCoroutine());
    }

    IEnumerator FlashCoroutine()
    {
        Color c = flashImage.color;

        c.a = 0.5f;
        flashImage.color = c;

        yield return new WaitForSeconds(flashDuration);

        c.a = 0f;
        flashImage.color = c;
    }
}