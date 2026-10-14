using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider[] mixerSliders;
    
    public void SetMasterVolume()
    {
        masterMixer.SetFloat(mixerSliders[0].name, Mathf.Log10(mixerSliders[0].value)*20);
        PlayerPrefs.SetFloat(mixerSliders[0].name, mixerSliders[0].value);
    }
    
    public void SetSFXVolume()
    {
        masterMixer.SetFloat(mixerSliders[1].name, Mathf.Log10(mixerSliders[1].value)*20);
        PlayerPrefs.SetFloat(mixerSliders[1].name, mixerSliders[1].value);
    }
 
    public void SetMusicVolume()
    {
        masterMixer.SetFloat(mixerSliders[2].name, Mathf.Log10(mixerSliders[2].value)*20);
        PlayerPrefs.SetFloat(mixerSliders[2].name, mixerSliders[2].value);
    }
 
    public void SetVoiceVolume()
    {
        masterMixer.SetFloat(mixerSliders[3].name, Mathf.Log10(mixerSliders[3].value)*20);
        PlayerPrefs.SetFloat(mixerSliders[3].name, mixerSliders[3].value);
    }
    
    public void SetDialogueVolume(float dialoguevol)
    {
        masterMixer.SetFloat("VoiceVolume", Mathf.Log10(dialoguevol)*20);
        PlayerPrefs.SetFloat("VoiceVolume", mixerSliders[3].value);
        print(dialoguevol);
    }

    public void SetVolume()
    {
        for (int i = 0; i < mixerSliders.Length; i++)
        {
            print(mixerSliders[i].name + ": " +PlayerPrefs.GetFloat(mixerSliders[i].name));
            
            masterMixer.SetFloat(mixerSliders[i].name, Mathf.Log10(mixerSliders[i].value)*20);
            PlayerPrefs.SetFloat(mixerSliders[i].name, mixerSliders[i].value);
        }
    }
    
    private void Start()
    {
        InitialiseSliders();
        InitialiseMixerVolume();
    }

    public void Update()
    {
        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            PlayerPrefs.DeleteAll();
        }
    }

    private void InitialiseMixerVolume()
    {
        for (int i = 0; i < mixerSliders.Length; i++)
        {
            masterMixer.SetFloat(mixerSliders[i].name, Mathf.Log10(PlayerPrefs.GetFloat(mixerSliders[i].name))*20);
        }
    }
 
    private void InitialiseSliders()
    {
        for (int i = 0; i < mixerSliders.Length; i++)
        {
            mixerSliders[i].value = PlayerPrefs.GetFloat(mixerSliders[i].name);
        }
    }
   
}