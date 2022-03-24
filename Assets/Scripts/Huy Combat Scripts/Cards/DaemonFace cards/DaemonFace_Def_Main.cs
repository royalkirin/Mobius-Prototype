using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaemonFace_Def_Main : DefenseCard
{
    public override void Play(GameObject target) {
        base.defenseValue = 1f;
        base.Play(target);
    }
}
