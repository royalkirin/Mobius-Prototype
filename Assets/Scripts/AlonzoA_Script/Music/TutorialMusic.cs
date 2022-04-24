using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMusic : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.Play("TutorialMusic");
    }
}
