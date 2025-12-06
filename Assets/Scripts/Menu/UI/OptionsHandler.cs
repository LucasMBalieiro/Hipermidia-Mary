using Audio;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OptionsHandler : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider uiSlider;
    
    private static float FloatToDB(float value)
    {
        return value <= 0.001f ? -80f : Mathf.Log10(value) * 20;
    }

    private static float DBToFloat(float value)
    {
        return value >= -80f ? Mathf.Pow(10f, value / 20f) : 0;
    }
    
    #region Buttons
    
    public void OnMasterChange(float volume)
    {
        audioMixer.SetFloat("MasterVolume", FloatToDB(volume));
    }
    
    public void OnMusicChange(float volume)
    {
        audioMixer.SetFloat("MusicVolume", FloatToDB(volume));
    }

    public void OnSFXChange(float volume)
    {
        audioMixer.SetFloat("SFXVolume", FloatToDB(volume));
    }

    public void OnUIChange(float volume)
    {
        audioMixer.SetFloat("UIVolume", FloatToDB(volume));
    }

    #endregion

    private void Start()
    {
        audioMixer.GetFloat("MasterVolume", out float masterVolume);
        audioMixer.GetFloat("MusicVolume", out float musicVolume);
        audioMixer.GetFloat("SFXVolume", out float sfxVolume);
        audioMixer.GetFloat("UIVolume", out float uiVolume);
        
        masterSlider.value = DBToFloat(masterVolume);
        musicSlider.value = DBToFloat(musicVolume);
        sfxSlider.value = DBToFloat(sfxVolume);
        uiSlider.value = DBToFloat(uiVolume);
    }
    
}
