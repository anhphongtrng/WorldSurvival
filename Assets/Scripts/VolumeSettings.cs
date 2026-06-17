using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private GameObject volumeSettingsPanel;
    [SerializeField] private Slider backgroundMusicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider uiSlider;

    private void Start()
    {

        if(PlayerPrefs.HasKey("BackgroundMusicVolume") && PlayerPrefs.HasKey("SFXVolume") && PlayerPrefs.HasKey("UIVolume"))
        {
            LoadAllTypeMusicVolume();
        }
        else
        {
            SetBackgroundMusicVolume();
            SetSFXVolume();
            SetUIVolume();
        }
    }

    public void SetBackgroundMusicVolume()
    {
        float volume = backgroundMusicSlider.value;
        mainMixer.SetFloat("BackgroundMusic", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BackgroundMusicVolume", volume);
    }

    private void LoadAllTypeMusicVolume()
    {
        backgroundMusicSlider.value = PlayerPrefs.GetFloat("BackgroundMusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        uiSlider.value = PlayerPrefs.GetFloat("UIVolume");
        SetBackgroundMusicVolume();
        SetSFXVolume();
        SetUIVolume();
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        mainMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetUIVolume()
    {
        float volume = uiSlider.value;
        mainMixer.SetFloat("UI", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("UIVolume", volume);
    }

    public void OpenVolumeSettings()
    {
        volumeSettingsPanel.SetActive(true);
    }

    public void CloseVolumeSetting()
    {
        volumeSettingsPanel.SetActive(false);
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
