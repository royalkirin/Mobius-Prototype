using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.Play("MainMenuMusic");
    }

    public void StopMusic()
    {
        AudioManager.instance.Stop("MainMenuMusic");
    }
}
