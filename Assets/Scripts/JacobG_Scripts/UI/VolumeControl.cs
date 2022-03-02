using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider soundEffects;
    public Slider music;
    //for when we have separate sounds and music;
    AudioSource Sound, Music;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            PlayerPrefs.SetFloat("SoundEffects", 1f);
        }
        else LoadAudio();
       
    }
   public void UpdateAudio()
    {
        AudioListener.volume = soundEffects.value;
        SaveAudio();
    }
    void SaveAudio()
    {
        PlayerPrefs.SetFloat("SoundEffects", soundEffects.value);
    }
    void LoadAudio()
    {
        soundEffects.value = PlayerPrefs.GetFloat("SoundEffects");
    }
   public void DefaultAudio()
    {
        soundEffects.value = 1f;
        SaveAudio();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
