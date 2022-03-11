using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Instant_Effect;
public class DaemonFace_Sup_InstantEffect : MonoBehaviour, InstantEffect {
    [SerializeField] int numbCardsToAdd = 1;

    public void ActivateInstantEffect() {
        //Find player Deck or enemy Deck
        Deck friendlyDeck = null;
        Card card = gameObject.GetComponent<Card>();
        if (card != null) {
            if (card.belongToPlayer) {
                friendlyDeck = GameObject.FindWithTag("PlayerDeck").GetComponent<Deck>();
            } else {
                friendlyDeck = GameObject.FindWithTag("EnemyDeck").GetComponent<Deck>();
            }
        }
        if (friendlyDeck is null || card is null) {
            Debug.LogError("Something is null here");
        }


        //deal to friendly character.
        for (int i = 0; i < numbCardsToAdd; i++) {
            if (card.belongToPlayer) {
                friendlyDeck.DealToPlayer(cardLimited: false);
            } else {
                friendlyDeck.DealToEnemy(cardLimited: false);
            }

        }

    }
}
