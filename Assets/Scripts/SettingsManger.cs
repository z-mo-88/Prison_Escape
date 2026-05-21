using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

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

    IEnumerator Start()
    {
       
        masterSlider.SetValueWithoutNotify(
            PlayerPrefs.GetFloat("MasterVol", 0.75f));

        sfxSlider.SetValueWithoutNotify(
            PlayerPrefs.GetFloat("SFXVol", 0.75f));

        musicSlider.SetValueWithoutNotify(
            PlayerPrefs.GetFloat("MusicVol", 0.75f));

        fullscreenToggle.isOn =
            Screen.fullScreenMode == FullScreenMode.FullScreenWindow;

        Screen.fullScreenMode = fullscreenToggle.isOn
            ? FullScreenMode.FullScreenWindow
            : FullScreenMode.Windowed;

        // APPLY SAVED AUDIO SETTINGS
        mainMixer.SetFloat(
            "MasterVol",
            Mathf.Log10(Mathf.Max(0.0001f, masterSlider.value)) * 20f);

        mainMixer.SetFloat(
            "SFXVol",
            Mathf.Log10(Mathf.Max(0.0001f, sfxSlider.value)) * 20f);

        mainMixer.SetFloat(
            "MusicVol",
            Mathf.Log10(Mathf.Max(0.0001f, musicSlider.value)) * 20f);

        yield return null;
    }

    public void SetMaster(float value)
    {
        mainMixer.SetFloat(
            "MasterVol",
            Mathf.Log10(Mathf.Max(0.0001f, value)) * 20f);

        PlayerPrefs.SetFloat("MasterVol", value);
        PlayerPrefs.Save();
    }

    public void SetSFX(float value)
    {
        mainMixer.SetFloat(
            "SFXVol",
            Mathf.Log10(Mathf.Max(0.0001f, value)) * 20f);

        PlayerPrefs.SetFloat("SFXVol", value);
        PlayerPrefs.Save();
    }

    public void SetMusic(float value)
    {
        mainMixer.SetFloat(
            "MusicVol",
            Mathf.Log10(Mathf.Max(0.0001f, value)) * 20f);

        PlayerPrefs.SetFloat("MusicVol", value);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreenMode = isFullscreen
            ? FullScreenMode.FullScreenWindow
            : FullScreenMode.Windowed;
    }
}