using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for now, control the CardPlayer component in the player
//CardPlayer provides the execution, we link the Input to those executions here
//in the future, link different player components here.
//[RequireComponent(typeof(CardPlayer))]
public class TutorialPlayerController : MonoBehaviour
{
    [SerializeField] TutorialCardPlayer cardPlayer = null;
    AudioSource sfx;
    Animator anim;

    [SerializeField] AudioClip Attack, Defend, Support;

    private void Start()
    {
        if(cardPlayer is null)
        {
            Debug.Log("Missing CardPlayer in " + name);
        }
        else
        {
            sfx = GetComponent<AudioSource>();
            anim = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            sfx.clip = Attack;
            sfx.Play();

            anim.SetTrigger("Attack");
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            sfx.clip = Defend;
            sfx.Play();

            //Unity wouldn't let me rename anim parameters for some stupid reason
            anim.SetTrigger("New Trigger");
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            sfx.clip = Support;
            sfx.Play();

            anim.SetTrigger("New Trigger 0");
        }
    }
}
