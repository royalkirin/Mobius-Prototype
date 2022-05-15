using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoMusic : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.Play("LevelTwoMusic");
    }

    public void Stop()
    {
        AudioManager.instance.Stop("LevelTwoMusic");
    }
}
