using UnityEngine;
using UnityEngine.Audio;

public class AudioLoader : MonoBehaviour
{
    public AudioMixer mainMixer;

    void Awake()
    {
        float master = PlayerPrefs.GetFloat("MasterVol", 0.75f);
        float sfx = PlayerPrefs.GetFloat("SFXVol", 0.75f);
        float music = PlayerPrefs.GetFloat("MusicVol", 0.75f);

        mainMixer.SetFloat("MasterVol",
            Mathf.Log10(Mathf.Max(0.0001f, master)) * 20);

        mainMixer.SetFloat("SFXVol",
            Mathf.Log10(Mathf.Max(0.0001f, sfx)) * 20);

        mainMixer.SetFloat("MusicVol",
            Mathf.Log10(Mathf.Max(0.0001f, music)) * 20);

        // DEBUG
        float masterDb;
        mainMixer.GetFloat("MasterVol", out masterDb);
        Debug.Log("Master dB = " + masterDb);

        float sfxDb;
        mainMixer.GetFloat("SFXVol", out sfxDb);
        Debug.Log("SFX dB = " + sfxDb);

        float musicDb;
        mainMixer.GetFloat("MusicVol", out musicDb);
        Debug.Log("Music dB = " + musicDb);
    }
}