using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneMusic : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.Play("LevelOneMusic");
    }

    public void Stop()
    {
        AudioManager.instance.Stop("LevelOneMusic");
    }
}
