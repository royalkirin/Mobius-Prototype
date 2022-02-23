using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class represent EnemyHand
//It's similar and simpler than PlayerHand
public class EnemyHand : MonoBehaviour
{
    public List<Card> cardsInHand;
    public const int MAX_CARD_COUNT = 5;
    Text Temp_Enemy_CardCount_text;
    string cardCountText = "Enemy Cards: ";
    private void Start()
    {
        Temp_Enemy_CardCount_text = GameObject.Find("Temp_Enemy_CardCount_text").GetComponent<Text>();
        if(Temp_Enemy_CardCount_text is null)
        {
            Debug.Log("Cannot find Temporary enemy card count text.");
        }
    }

    public void AddCard (Card card, bool cardLimited = true)
    {
        Debug.Log("Enemy Hand get 1 card");
        if (cardsInHand.Count >= MAX_CARD_COUNT && cardLimited)
        {
            Debug.Log("Currently have " + cardsInHand.Count + " cards in hand, card limit is reached.");
            return;
        }

        //instantiate a new card game object under Enemy char -> EnemyHand
        Card newCardInHand = GameObject.Instantiate(card, parent: this.transform);
        newCardInHand.SetOwner(false);//this card does not belong to player

        cardsInHand.Add(newCardInHand);
        newCardInHand.positionInHand = cardsInHand.Count - 1;//position = last position;
        Temp_Enemy_CardCount_text.text = cardCountText + cardsInHand.Count;
    }



    public bool RemoveCard(int cardIndex)
    {
        if(cardIndex < 0 || cardIndex >= cardsInHand.Count)
        {
            Debug.LogError("Invalid index in list. Enemy hand has " + cardsInHand.Count + " cards," +
                            " index = " + cardIndex);
            return false;
        }
        cardsInHand.RemoveAt(cardIndex);
        Debug.Log("Remove sucessfully at index = " + cardIndex);
        Temp_Enemy_CardCount_text.text = cardCountText + cardsInHand.Count;
        UpdateCardIndices();
        return true;
    }

    private void UpdateCardIndices()
    {
        for(int i = 0; i < cardsInHand.Count; i++)
        {
            cardsInHand[i].positionInHand = i;
        }

    }


    public int GetAttackCount() {
        int count = 0;
        foreach(Card card in cardsInHand) {
            if(card is AttackCard) {
                count++;
            }
        }
        //Debug.Log("Enemy attack count = " + count);
        return count;
    }

    public int GetDefenseCount() {
        int count = 0;
        foreach (Card card in cardsInHand) {
            if (card is DefenseCard) {
                count++;
            }
        }
        //Debug.Log("Enemy defense count = " + count);
        return count;
    }

    public int GetSupportCount() {
        int count = 0;
        foreach (Card card in cardsInHand) {
            if (card is SupportCard) {
                count++;
            }
        }
       // Debug.Log("Enemy support count = " + count);
        return count;
    }

    public int GetCardCount() {
        Debug.Log("Enemy total count = " + cardsInHand.Count);
        return cardsInHand.Count;
    }
}
