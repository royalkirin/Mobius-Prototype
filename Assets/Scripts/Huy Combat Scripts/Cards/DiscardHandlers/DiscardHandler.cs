using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this class manages the functionality of DiscardHandler prefab
//manages logic when player needs to discard until he has a valid number of cards in hand.
public class DiscardHandler : MonoBehaviour
{   
    //after discard is done, switch TurnManager bool signifying player have valid number of cards
    TurnManager turnManager;

    [SerializeField] Text DiscardMessage; //message player how many cards he needs to discard

    PlayerHand playerHand; //get number of cards

    int numbToDiscard;

    private void Start()
    {
        FindVariables();
        UpdateDiscardMessage();
    }

    private void FindVariables()
    {
        turnManager = FindObjectOfType<TurnManager>();
        if(turnManager is null)
        {
            Debug.Log("Cannot find Turn manager in " + name);
        }

        playerHand = GameObject.FindWithTag("PlayerHand").GetComponent<PlayerHand>();
        if(playerHand is null)
        {
            Debug.Log("Cannot find Player Hand in " + name);
        }
    }

    public void UpdateDiscardMessage()
    {
        numbToDiscard = playerHand.cardsInHand.Count - PlayerHand.MAX_CARD_COUNT;
        DiscardMessage.text = "You have " + playerHand.cardsInHand.Count + " cards. " +
                              "The maximum allowed is " + PlayerHand.MAX_CARD_COUNT + "." +
                              "Please discard: " + numbToDiscard + " cards.";
    }



}
