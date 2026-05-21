using UnityEngine;
using UnityEngine.Audio;

public class AudioLoader : MonoBehaviour
{
    public AudioMixer mainMixer;

    void Start()
    {
        // 1. Fetch the saved settings values from PlayerPrefs
        float master = PlayerPrefs.GetFloat("MasterVol", 0.75f);
        float sfx = PlayerPrefs.GetFloat("SFXVol", 0.75f);
        float music = PlayerPrefs.GetFloat("MusicVol", 0.75f);

        // 2. Force apply them to this scene's active mixer instance
        mainMixer.SetFloat("MasterVol", Mathf.Log10(Mathf.Max(0.0001f, master)) * 20);
        mainMixer.SetFloat("SFXVol", Mathf.Log10(Mathf.Max(0.0001f, sfx)) * 20);
        mainMixer.SetFloat("MusicVol", Mathf.Log10(Mathf.Max(0.0001f, music)) * 20);
    }
}