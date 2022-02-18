using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ronin_zanshin : DefenseCard {
    //TODO: change this. We do not call the base.Play
    public override void Play(GameObject target) {
        base.Play(target); // remove this, implement zanshin
        isPlayed = true; //base.base.Play(), because when we remove base.play, base.base is not called.
        Debug.Log("IMPLEMENTING Zhanshin’s card effect!");



        //####################################################
        //
        //  desc: zanshin raise a shield over the player.  Health.cs -> .RaiseTheShield();
        //        Next attack, if the shield is active, the player will not receive damage
        //
        //
        //####################################################


        GameObject[] Characters;
        
        if (belongToPlayer) {
            Characters = GameObject.FindGameObjectsWithTag("PlayerCharacter");
            foreach (GameObject Character in Characters) {
                (Character.GetComponent<Attackable>()).RaiseTheShield();
                return;
            }
        } else {
            Characters = GameObject.FindGameObjectsWithTag("EnemyCharacter");
            foreach (GameObject Character in Characters) {
                ( Character.GetComponent<Attackable>() ).RaiseTheShield();
                return;
            }
        }



    }
}
