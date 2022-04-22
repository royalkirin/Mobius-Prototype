using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Deck_SP : MonoBehaviour
{
    [SerializeField]
    private CharacterCardCollection _playerCardDeck;
    
    private List<GameObject> _discardPile;
    
    public void ShuffleDeck()
    {
        for (int i = 0; i < _playerCardDeck.cardPrefabs.Count; i++) {
            var temp = _playerCardDeck.cardPrefabs[i];
            int randomIndex = Random.Range(i, _playerCardDeck.cardPrefabs.Count);
            _playerCardDeck.cardPrefabs[i] = _playerCardDeck.cardPrefabs[randomIndex];
            _playerCardDeck.cardPrefabs[randomIndex] = temp;
        }
    }

    public void DealCards(Hand_SP hand)
    {
        for (int i = hand.Cards.Count; i < hand.MaxCards; i++)
        {
            hand.AddCard(_playerCardDeck.cardPrefabs[0]);
            RemoveCard();
        }
    }

    public void DrawCard(Hand_SP hand)
    {
        hand.AddCard(_playerCardDeck.cardPrefabs[0]);
        RemoveCard();
    }

    private void RemoveCard()
    {
        _playerCardDeck.cardPrefabs.RemoveAt(0);
        
        if (_playerCardDeck.cardPrefabs.Count <= 0)
        {
            AddDiscardToDeck();
        }
    }

    private void AddDiscardToDeck()
    {
        foreach (var card in _discardPile)
        {
            _playerCardDeck.cardPrefabs.Add(card);
        }
        ShuffleDeck();
    }

    public void AddToDiscard(GameObject card)
    {
        _discardPile.Add(card);
    }
}
