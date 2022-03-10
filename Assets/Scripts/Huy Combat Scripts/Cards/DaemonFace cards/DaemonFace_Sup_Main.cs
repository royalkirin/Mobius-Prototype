using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaemonFace_Sup_Main : SupportCard
{
    public override void Play(GameObject target) {
        base.numbCardsToAdd = 2;
        base.Play(target);
    }
}
