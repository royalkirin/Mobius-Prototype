using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseCard : Card
{
    float defenseValue = 1f;

    public new void Play(GameObject target)
    {
        base.Play(target);
        Defense(target);
    }


    //try to Defense the target
    private bool Defense(GameObject target)
    {
        //if the target is Attackable -> we increase its defense
        if (target.TryGetComponent<Attackable>(out Attackable targetAttackable))
        {
            targetAttackable.AddDefense(defenseValue);
            return true;
        }
        else //if not, we cannot attack -> we cannot add defense
        {
            Debug.Log(target.name + " is not Defensible because cannot be attacked");
            return false;
        }
    }
}
