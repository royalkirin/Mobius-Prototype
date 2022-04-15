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

    public bool attSet;
    public bool defSet;
    public bool supSet;
    public bool painSet;

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

        if (animNum == 1)
        {
            AnimationSwitch_ATTACK("AttackBool", "Attack");
            AnimationSwitch_SUPPORT("SupportBool", "Support");
            AnimationSwitch_DEFENSE("DefenseBool", "Defense");
        }
        else if (animNum == 2)
        {
            AnimationSwitch_SUPPORT("SupportBool", "Support");
            AnimationSwitch_DEFENSE("DefenseBool", "Defense");
            AnimationSwitch_ATTACK("AttackBool", "Attack");
        }
        else if (animNum == 3)
        {
            AnimationSwitch_DEFENSE("DefenseBool", "Defense");
            AnimationSwitch_ATTACK("AttackBool", "Attack");
            AnimationSwitch_SUPPORT("SupportBool", "Support");
        }
        else if (animNum == 4)
        {
            AnimationSwitch_PAIN("DamagedBool", "Damaged");
        }
        else if (!attStart && !defStart && !supStart && !painStart)
        {
            animNum = 0;
        }
    }

    void AnimationSwitch_ATTACK(string c_BoolName, string c_TagName)
    {
        if (attStart == true)
        {
            characterAnim.SetBool(c_BoolName, true);

            if (characterAnim.GetCurrentAnimatorStateInfo(0).IsTag(c_TagName) == true)
            {
                attStart = false;
                characterAnim.SetBool(c_BoolName, false);
            }
        }
    }

    void AnimationSwitch_DEFENSE(string c_BoolName, string c_TagName)
    {
        if (defStart == true)
        {
            characterAnim.SetBool(c_BoolName, true);

            if (characterAnim.GetCurrentAnimatorStateInfo(0).IsTag(c_TagName) == true)
            {
                defStart = false;
                characterAnim.SetBool(c_BoolName, false);
            }
        }
    }

    void AnimationSwitch_SUPPORT(string c_BoolName, string c_TagName)
    {
        if (supStart == true)
        {
            characterAnim.SetBool(c_BoolName, true);

            if (characterAnim.GetCurrentAnimatorStateInfo(0).IsTag(c_TagName) == true)
            {
                supStart = false;
                characterAnim.SetBool(c_BoolName, false);
            }
        }
    }

    void AnimationSwitch_PAIN(string c_BoolName, string c_TagName)
    {
        if (painStart == true)
        {
            characterAnim.SetBool(c_BoolName, true);

            if (characterAnim.GetCurrentAnimatorStateInfo(0).IsTag(c_TagName) == true)
            {
                painStart = false;
                characterAnim.SetBool(c_BoolName, false);
            }
        }
    }
}
