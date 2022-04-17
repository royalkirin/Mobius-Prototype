using UnityEngine.Audio;
using System;
using UnityEngine;

/*-------------------------------------------------------------------------------------------------------------------------------
 * Name: Youtube Creator Brackeys
 * Modified By: N/A
 * Date: 5/31/17
 * Date Modified: N/A
 * Video Used: https://www.youtube.com/watch?v=6OT43pvUyfY
 * Purpose: Creating an audiomanager that can be referenced from other scripts to play sounds.
 --------------------------------------------------------------------------------------------------------------------------*/
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sounds[] sounds;

    void Awake()
    {
        CreateInstance();
        foreach( Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, Sounds => Sounds.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sounds: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    private void CreateInstance()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
