using UnityEngine;
using System.Collections;

public class TimedSoundLoop : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("Timing")]
    public float playTime = 5f;
    public float stopTime = 5f;

    IEnumerator Start()
    {
        while (true)
        {
            // PLAY SOUND
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // WAIT WHILE PLAYING
            yield return new WaitForSeconds(playTime);

            // STOP SOUND
            if (audioSource != null)
            {
                audioSource.Stop();
            }

            // WAIT WHILE STOPPED
            yield return new WaitForSeconds(stopTime);
        }
    }
}