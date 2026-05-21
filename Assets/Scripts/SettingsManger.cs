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

    System.Collections.IEnumerator Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVol", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVol", 0.75f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol", 0.75f);

        fullscreenToggle.isOn =
            Screen.fullScreenMode == FullScreenMode.FullScreenWindow;

        Screen.fullScreenMode = fullscreenToggle.isOn
            ? FullScreenMode.FullScreenWindow
            : FullScreenMode.Windowed;

        yield return null;

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
        Screen.fullScreenMode = isFullscreen
            ? FullScreenMode.FullScreenWindow
            : FullScreenMode.Windowed;
    }
}