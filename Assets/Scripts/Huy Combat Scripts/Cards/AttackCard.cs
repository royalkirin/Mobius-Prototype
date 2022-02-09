using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Attack card is a Card, with a Target that it will try to attack when activated.
public class AttackCard : Card
{
    float damage = 1f;

    public override void Play(GameObject target)
    {
        base.Play(target);
        Attack(target);
    }
 
    //when played, try to attack target
    public bool Attack(GameObject target)
    {
        //if the target is Attackable
        if(target.TryGetComponent<Attackable>(out Attackable targetAttackable))
        {
            targetAttackable.TakeDamage(damage);
            Debug.Log(targetAttackable.gameObject.name + " take 1 DAMAGE");
            return true;
        }
        else //if not, we cannot attack
        {
            Debug.Log(target.name + " is not Attackable");
            return false;
        }
    }
}
