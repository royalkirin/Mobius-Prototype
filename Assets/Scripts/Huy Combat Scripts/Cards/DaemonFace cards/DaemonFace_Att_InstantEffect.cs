using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Instant_Effect;


public class DaemonFace_Att_InstantEffect : MonoBehaviour, InstantEffect {

    [SerializeField] float damage = 1f;

    //instant effect: deal 1 dmg to enemy character
    public void ActivateInstantEffect() {
        //Find enemy character.
        GameObject EnemyCharacter = null;
        Card card = gameObject.GetComponent<Card>();
        if(card != null) {
            if (card.belongToPlayer) {
                EnemyCharacter = GameObject.FindWithTag("EnemyCharacter");
            } else {
                EnemyCharacter = GameObject.FindWithTag("PlayerCharacter");
            }
        }
        if(EnemyCharacter is null || card is null) {
            Debug.LogError("Something is null here");
        }



        //deal dmg to Enemy character.
        //if the target is Attackable
        if (EnemyCharacter.TryGetComponent<Attackable>(out Attackable targetAttackable)) {
            targetAttackable.TakeDamage(damage);
            Debug.Log("Instant effect: " + targetAttackable.gameObject.name + " take " + damage + " DAMAGE");
            return;
        } else //if not, we cannot attack
          {
            Debug.LogWarning("Instant effect: FAIL to implement.");
            return;
        }
    }
}
