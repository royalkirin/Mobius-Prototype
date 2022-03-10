using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaemonFace_Att_Main : AttackCard
{



    //slash is the same with default attack card, so we change nothing.
    public override void Play(GameObject target) {
        base.damage = 2f;
        base.Play(target);
    }


}
