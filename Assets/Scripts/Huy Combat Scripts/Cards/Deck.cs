using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class represents a Deck. A Deck is a list of cards.
//this class will load cards into deck depends on the character.
public class Deck : MonoBehaviour
{
    public const int CARD_COUNT = 30;
     


    
    public List<Card> cardsInDeck;//the actual deck in the game -> no order
    




    //VARS for loading initial card collection
    //the list of cards to randomized from.
    //this list can be further separated into a class 
    //with [CreateAssetMenu filename...] to specify for each character.
    public List<Card> cardGenerateList;
    //for player, friendlyTag = PlayerCharacter
    //for enemy, friendlyTag = EnemyCharacter
    [SerializeField] string friendlyTag = null;
    GameObject friendlyCharacter;
    CharacterSelection characterSelection;
    CharacterCardCollection cardCollection;


    //the hand the deck deals to
    //for now, only PlayerHand, AI doesn't have a hand.
    private PlayerHand playerHand = null;

    private void Start()
    {
        FindVariables();
        LoadCardListBasedOnCharacter();
        InitiateRandomDeck();
        FullDealToPlayer();
    }

    
    //load the initial card list to generate randomly
    //return true if sucessfully load, false otherwise
    private bool LoadCardListBasedOnCharacter()
    {
        if(friendlyCharacter is null)
        {
            return false;
        }
        characterSelection = friendlyCharacter.GetComponent<CharacterSelection>();
        cardCollection = characterSelection.characterCards;
        if(cardCollection is null)
        {
            return false;
        }

        //we have a card collection here.
        //start loading cards
        cardGenerateList.Clear();
        foreach (GameObject cardPrefab in cardCollection.cardPrefabs)
        {
            Card currentCard = cardPrefab.GetComponent<Card>();
            if(currentCard is null)
            {
                Debug.LogWarning(cardPrefab.name + " prefab doesn't have a card component.");
                return false;
            }
            cardGenerateList.Add(currentCard);
        }

        return true;
    }

    private void FindVariables()
    {
        playerHand = GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<PlayerHand>();
        if(playerHand is null)
        {
            Debug.Log("Cannot find player hand in the scene. In " + name);
        }

        friendlyCharacter = GameObject.FindWithTag(friendlyTag);
        if (friendlyCharacter is null)
        {
            Debug.Log(name + "cannot find friendly character with tag " + friendlyTag);
        }
    }


    //we randomize the cards from the same list, not just change the positions of the same list.
    //maybe need to change implementation in the future.
    private void InitiateRandomDeck()
    {
        int range = cardGenerateList.Count;
        for(int i = 0; i < CARD_COUNT; i++)
        {
            int rdIndex = UnityEngine.Random.Range(0, range);//randomize a card
            cardsInDeck.Add(cardGenerateList[rdIndex]); //add it to the deck
        }
    }

    //deal the first card on the list (top card on the deck) to the player
    public void DealToPlayer(bool cardLimited = true)
    {
        if(cardsInDeck.Count == 0)
        {
            Debug.Log("There is no card left in the deck. ");
            return;
        }

        if(cardLimited == false)
        {
           // Debug.Log("Dealing unlimited card");
        }

        playerHand.AddCard(cardsInDeck[0], cardLimited);
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
