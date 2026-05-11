using UnityEngine;
using System.Collections;

public class PoliceSleepingSound : MonoBehaviour
{
    private AudioSource sleepingAudio;

    public float firstDelay = 5f;
    public float repeatDelay = 60f;

    void Start()
    {
        sleepingAudio = GetComponent<AudioSource>();

        StartCoroutine(PlaySleepingSound());
    }

    IEnumerator PlaySleepingSound()
    {
        yield return new WaitForSeconds(firstDelay);

        while (true)
        {
            sleepingAudio.Play();

            yield return new WaitForSeconds(sleepingAudio.clip.length);

            sleepingAudio.Stop();

            yield return new WaitForSeconds(repeatDelay);
        }
    }
}