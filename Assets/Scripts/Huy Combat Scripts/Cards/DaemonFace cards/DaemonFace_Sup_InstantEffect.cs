using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Instant_Effect;
public class DaemonFace_Sup_InstantEffect : MonoBehaviour, InstantEffect {
    [SerializeField] int numbCardsToAdd = 1;

    public void ActivateInstantEffect() {
        //Find player Deck or enemy Deck
        EnemyHand enemyHand = null;
        enemyHand = GameObject.FindWithTag("EnemyHand").GetComponent<EnemyHand>();
        if (enemyHand is null) {
            Debug.LogError("Cannot find Enemey Hand");
        } else {
            enemyHand.RemoveRandomCard();
        }


    }
}
