using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer mainMixer;

    [Header("UI Sliders")]
    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider musicSlider;

    [Header("UI Toggle")]
    public Toggle fullscreenToggle;

    void Start()
    {
        // 1. Load saved values (Default to 0.75 if first time playing)
        masterSlider.value = PlayerPrefs.GetFloat("MasterVol", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVol", 0.75f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol", 0.75f);

        // 2. Load Fullscreen state
        fullscreenToggle.isOn = Screen.fullScreen;

        // 3. Set the mixer levels immediately on start
        SetMaster(masterSlider.value);
        SetSFX(sfxSlider.value);
        SetMusic(musicSlider.value);
    }

    public void SetMaster(float value)
    {
        // This math converts 0-1 slider to -80 to 20 decibels
        mainMixer.SetFloat("MasterVol", Mathf.Log10(Mathf.Max(0.0001f, value)) * 20);
        PlayerPrefs.SetFloat("MasterVol", value);
    }

    public void SetSFX(float value)
    {
        mainMixer.SetFloat("SFXVol", Mathf.Log10(Mathf.Max(0.0001f, value)) * 20);
        PlayerPrefs.SetFloat("SFXVol", value);
    }

    public void SetMusic(float value)
    {
        mainMixer.SetFloat("MusicVol", Mathf.Log10(Mathf.Max(0.0001f, value)) * 20);
        PlayerPrefs.SetFloat("MusicVol", value);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}