using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public void ButtonClick()
    {
        AudioManager.instance.Play("ButtonClick");
    }
    public void SliderSound()
    {
        AudioManager.instance.Play("SliderSound");
    }
}
