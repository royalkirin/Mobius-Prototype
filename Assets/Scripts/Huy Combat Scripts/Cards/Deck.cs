using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class represents a Deck. A Deck is a list of cards.
public class Deck : MonoBehaviour
{
    public const int CARD_COUNT = 30;
     
    public List<Card> cardsInDeck;//the actual deck in the game -> no order

    //the list of cards to randomized from.
    //this list can be further separated into a class 
    //with [CreateAssetMenu filename...] to specify for each character.
    public List<Card> allCardsInOrder;

    //the hand the deck deals to
    //for now, only PlayerHand, AI doesn't have a hand.
    private PlayerHand playerHand = null;

    private void Start()
    {
        FindVariables();
        InitiateRandomDeck();
        FullDealToPlayer();
    }



    private void FindVariables()
    {
        playerHand = GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<PlayerHand>();
        if(playerHand is null)
        {
            Debug.Log("Cannot find player hand in the scene. In " + name);
        }
    }


    //we randomize the cards from the same list, not just change the positions of the same list.
    //maybe need to change implementation in the future.
    private void InitiateRandomDeck()
    {
        int range = allCardsInOrder.Count;
        for(int i = 0; i < CARD_COUNT; i++)
        {
            int rdIndex = UnityEngine.Random.Range(0, range);//randomize a card
            cardsInDeck.Add(allCardsInOrder[rdIndex]); //add it to the deck
        }
    }

    //deal the first card on the list (top card on the deck) to the player
    public void DealToPlayer()
    {
        if(cardsInDeck.Count == 0)
        {
            Debug.Log("There is no card left in the deck. ");
            return;
        }
        Debug.Log("Dealing");
        playerHand.AddCard(cardsInDeck[0]);
        cardsInDeck.RemoveAt(0);
    }

    //fill the player hand until he has 5 cards or the deck is out of card.
    public void FullDealToPlayer()
    {
        while(playerHand.cardsInHand.Count < 5 && cardsInDeck.Count > 0)
        {
            DealToPlayer();
        }
    }
}
