using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger02 : MonoBehaviour
{
    Animator characterAnim;
    Animation anim;

    public bool attStart;
    public bool defStart;
    public bool supStart;
    public bool painStart;

    public int animNum;

    // Start is called before the first frame update
    void Start()
    {
        characterAnim = GetComponent<Animator>();
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

        //attPlaying = anim.isPlaying;

        if (animNum == 2)
        {
            if (attStart == true)
            {
                characterAnim.SetBool("AttackBool", true);

                if (characterAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") == true)
                {
                    attStart = false;
                    characterAnim.SetBool("AttackBool", false);
                }
            }
            else if (supStart == true)
            {
                characterAnim.SetBool("SupportBool", true);

                if (characterAnim.GetCurrentAnimatorStateInfo(0).IsTag("Support") == true)
                {
                    supStart = false;
                    characterAnim.SetBool("SupportBool", false);
                }
            }
            else if (defStart == true)
            {
                characterAnim.SetBool("DefenseBool", true);

                if (characterAnim.GetCurrentAnimatorStateInfo(0).IsTag("Defense") == true)
                {
                    defStart = false;
                    characterAnim.SetBool("DefenseBool", false);
                }
            }
            else
            {
                animNum = 0;
            }
        }
        else if (animNum == 3)
        {
            if (supStart == true)
            {
                characterAnim.SetBool("SupportBool", true);

                if (characterAnim.GetCurrentAnimatorStateInfo(0).IsTag("Support") == true)
                {
                    supStart = false;
                    characterAnim.SetBool("SupportBool", false);
                }
            }
            else if (defStart == true)
            {
                characterAnim.SetBool("DefenseBool", true);

                if (characterAnim.GetCurrentAnimatorStateInfo(0).IsTag("Defense") == true)
                {
                    defStart = false;
                    characterAnim.SetBool("DefenseBool", false);
                }
            }
            else if (attStart == true)
            {
                characterAnim.SetBool("AttackBool", true);

                if (characterAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") == true)
                {
                    attStart = false;
                    characterAnim.SetBool("AttackBool", false);
                }
            }
            else
            {
                animNum = 0;
            }
        }
        else if (animNum == 1)
        {
            if (defStart == true)
            {
                characterAnim.SetBool("DefenseBool", true);

                if (characterAnim.GetCurrentAnimatorStateInfo(0).IsTag("Defense") == true)
                {
                    defStart = false;
                    characterAnim.SetBool("DefenseBool", false);
                }
            }
            else if (attStart == true)
            {
                characterAnim.SetBool("AttackBool", true);

                if (characterAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") == true)
                {
                    attStart = false;
                    characterAnim.SetBool("AttackBool", false);
                }
            }
            else if (supStart == true)
            {
                characterAnim.SetBool("SupportBool", true);

                if (characterAnim.GetCurrentAnimatorStateInfo(0).IsTag("Support") == true)
                {
                    supStart = false;
                    characterAnim.SetBool("SupportBool", false);
                }
            }
            else
            {
                animNum = 0;
            }
        }
        else if (animNum == 4)
        {
            if (painStart == true)
            {
                characterAnim.SetBool("DamagedBool", true);

                if (characterAnim.GetCurrentAnimatorStateInfo(0).IsTag("Damaged") == true)
                {
                    characterAnim.SetBool("DamagedBool", false);
                    painStart = false;
                }
            }
            else
            {
                animNum = 0;
            }
        }
        else if (animNum == 0)
        {
            characterAnim.SetBool("AttackBool", false);
            characterAnim.SetBool("SupportBool", false);
            characterAnim.SetBool("DefenseBool", false);
        }


    }
}
