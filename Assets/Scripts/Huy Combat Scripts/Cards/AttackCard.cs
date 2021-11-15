using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : Card
{
    float damage = 1f;

    public new void Play(GameObject target)
    {
        base.Play(target);
        Attack(target);
    }
 

    private bool Attack(GameObject target)
    {
        //if the target is Attackable
        if(target.TryGetComponent<Attackable>(out Attackable targetAttackable))
        {
            targetAttackable.TakeDamage(damage);
            return true;
        }
        else //if not, we cannot attack
        {
            Debug.Log(target.name + " is not Attackable");
            return false;
        }
    }
}
