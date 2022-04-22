using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TurnManager_SP : MonoBehaviour
{
    [SerializeField]
    private Deck_SP _playerDeck;
    [SerializeField]
    private Hand_SP _playerHand;
    [SerializeField]
    private Deck_SP _aiDeck;
    [SerializeField]
    private Hand_SP _aiHand;
    
    
    public static TurnManager_SP Instance;
    
    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void GameStart()
    {
        switch (GameManager.Instance.CurrentState)
        {
            case GameManager.GameState.GameStart:
                _playerDeck.ShuffleDeck();
                _playerDeck.DealCards(_playerHand);
                _playerHand.DrawCards();
                _aiDeck.ShuffleDeck();
                _aiDeck.DealCards(_aiHand);
                _aiHand.DrawCards();
                GameManager.Instance.CurrentState = GameManager.GameState.PlayerTurn;
                break;
            default:
                break;
                
        }
    }
}
