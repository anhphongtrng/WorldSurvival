using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider backgroundMusicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider uiSlider;

    private void Start()
    {
        SetBackgroundMusicVolume();
        SetSFXVolume();
        SetUIVolume();
    }

    public void SetBackgroundMusicVolume()
    {
        float volume = backgroundMusicSlider.value;
        mainMixer.SetFloat("BackgroundMusic", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        mainMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public void SetUIVolume()
    {
        float volume = uiSlider.value;
        mainMixer.SetFloat("UI", Mathf.Log10(volume) * 20);
    }

    public void CloseVolumeSetting()
    {
        UIController.instance.SetVolumeSettingsPanel(false);
    }

    public void ResetVolume()
    {
        backgroundMusicSlider.value = 0.5f;
        sfxSlider.value = 0.5f;
        uiSlider.value = 0.5f;
        SetBackgroundMusicVolume();
        SetSFXVolume();
        SetUIVolume();
    }
}
