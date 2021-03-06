using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Defense card is a Card, with a Target that it will try to Defend when activated.
public class DefenseCard : Card
{
    public float defenseValue = 1f;

    public override void Play(GameObject target)
    {
        base.Play(target);
        Defense(target);
    }


    //when played, try to Defense the target
    public bool Defense(GameObject target)
    {
        //if the target is Attackable -> we increase its defense
        if (target.TryGetComponent<Attackable>(out Attackable targetAttackable))
        {
            targetAttackable.AddDefense(defenseValue);
            AudioManager.instance.Play("DefenseCard");
            if (targetAttackable.gameObject.name == "Friendly Char")
            {
                BattleTextHandler.Instance.IncrementDEFENSEValue(1, true);
            }
            else
            {
                BattleTextHandler.Instance.IncrementDEFENSEValue(1, false);
            }
            return true;
        }
        else //if not, we cannot attack -> we cannot add defense
        {
            Debug.Log(target.name + " is not Defensible because cannot be attacked");
            return false;
        }
    }
}
