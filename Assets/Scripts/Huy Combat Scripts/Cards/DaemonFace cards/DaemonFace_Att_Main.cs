using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaemonFace_Att_Main : AttackCard
{
    public bool isVampire = false;
    float mainDamage = 1f;

    //slash is the same with default attack card, so we change nothing.
    public override void Play(GameObject target) {
        base.damage = mainDamage;
        base.Play(target);

        if (isVampire) {
            //heal 1 health.
            if (belongToPlayer) {
                Attackable FriendlyAttackable = GameObject.FindWithTag("PlayerCharacter").GetComponent<Attackable>();
                if(FriendlyAttackable is null) {
                    Debug.LogError("Cannot find friendly char");
                    return;
                } else {
                    FriendlyAttackable.TakeDamage(mainDamage * -1);
                }
            } else {
                Debug.LogError("Daemon Face cannot be an enemy.");
            }
        }
    }


    public void SetVampire(bool isVampire) {
        this.isVampire = isVampire;
        Debug.LogWarning("Main attack is Vampire.");
    }
}
