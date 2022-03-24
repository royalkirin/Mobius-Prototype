using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Instant_Effect;
using System;

public class DaemonFace_Def_InstantEffect : MonoBehaviour, InstantEffect {
    [SerializeField] float InflictionValue = 1f;

    public void ActivateInstantEffect() {
        //Find FriendlyCharacter
        GameObject FriendlyCharacter = null;
        Card card = gameObject.GetComponent<Card>();
        if (card != null) {
            if (card.belongToPlayer) {
                FriendlyCharacter = GameObject.FindWithTag("PlayerCharacter");
            } else {
                FriendlyCharacter = GameObject.FindWithTag("EnemyCharacter");
            }
        }
        if (FriendlyCharacter is null || card is null) {
            Debug.LogError("Something is null here");
        }



        //add 1 defense to FriendlyCharacter
        //if the target is Attackable -> we increase its defense
        if (FriendlyCharacter.TryGetComponent<Attackable>(out Attackable targetAttackable)) {
            Infliction(targetAttackable, InflictionValue);
            return;
        } else //if not, we cannot attack -> we cannot add defense
          {
            Debug.LogWarning("Instant effect: FAIL to implement.");
            return;
        }
    }

    private void Infliction(Attackable targetAttackable, float inflictionValue) {
        if (GetComponent<Card>().belongToPlayer) {
            GameObject playerCharacter = GameObject.FindGameObjectWithTag("PlayerCharacter");
            playerCharacter.GetComponent<CharacterBuffs>().ApplyInfliction(true, inflictionValue);
        } else {
            Debug.LogWarning("Enemy cannot use Daemon Face cards.");
        }

    }

}
