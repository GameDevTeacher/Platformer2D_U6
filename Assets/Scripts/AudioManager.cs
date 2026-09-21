using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider[] mixerSliders;
    
    public void SetMasterVolume()
    {
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(mixerSliders[0].value)*20);
    }
    
    public void SetSFXVolume()
    {
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(mixerSliders[1].value)*20);
    }
 
    public void SetMusicVolume()
    {
        masterMixer.SetFloat("MusicVolume", Mathf.Log10(mixerSliders[2].value)*20);
    }
 
    public void SetVoiceVolume()
    {
        masterMixer.SetFloat("VoiceVolume", Mathf.Log10(mixerSliders[3].value)*20);
    }
}