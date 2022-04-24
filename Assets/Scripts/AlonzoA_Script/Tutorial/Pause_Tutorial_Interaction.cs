using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Tutorial_Interaction : MonoBehaviour
{
    [SerializeField] TextUpdates textUpdates;

    private void Start()
    {
        textUpdates = GameObject.FindObjectOfType<TextUpdates>();
    }

    public void StopTutorial()
    {
        textUpdates._checkClicks = false;
    }

    public void StartTutorialAgain()
    {
        //StartCoroutine(ResumeClick());
    }

    IEnumerator ResumeClick()
    {
        yield return new WaitForSeconds(0.2f);
        textUpdates._checkClicks = true;
        StopCoroutine(ResumeClick());
    }
}

