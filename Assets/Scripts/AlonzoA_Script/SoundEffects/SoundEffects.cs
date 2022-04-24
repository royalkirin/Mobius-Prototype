using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    AudioManager audiomanager;

    void Awake()
    {
        audiomanager = FindObjectOfType<AudioManager>();
    }
    public void ButtonClick()
    {
        audiomanager.Play("ButtonClick");
    }
    public void SliderSound()
    {
        audiomanager.Play("SliderSound");
    }
}
