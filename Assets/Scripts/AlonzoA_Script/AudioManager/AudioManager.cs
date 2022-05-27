using System.Collections.Generic;
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
    private Dictionary<string, Sounds> soundDictionary = new Dictionary<string, Sounds>();
    [SerializeField] bool enableMusic = true;

    void Awake()
    {
        CreateInstance();
        foreach (Sounds s in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            s.source = source;
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            soundDictionary.Add(s.name, s);
        }
    }

    public void Play(string name)
    {
        if (enableMusic)
        {
            soundDictionary[name].source.Play();
        }
    }

    public void Stop(string name)
    {
        soundDictionary[name].source.Stop();
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
