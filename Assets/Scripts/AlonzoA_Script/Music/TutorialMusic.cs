using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMusic : MonoBehaviour
{
    AudioManager audiomanager;
    void Awake()
    {
        audiomanager = FindObjectOfType<AudioManager>();
    }
    private void Start()
    {
        audiomanager.Play("TutorialMusic");
    }
}
