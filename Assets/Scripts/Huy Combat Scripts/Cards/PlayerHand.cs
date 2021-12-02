using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//represents the current cards in player hand
public class PlayerHand : MonoBehaviour
{
    public List<Card> cardsInHand;
    public const int MAX_CARD_COUNT = 5;

    //UI component, positions of the cards in the hand.
    [SerializeField] List<GameObject> cardPositions;


    //the deck will add card to player hand
    public void AddCard(Card card)
    {
        if(cardsInHand.Count > MAX_CARD_COUNT)
        {
            Debug.Log("Currently have " + cardsInHand.Count + " cards in hand, way to much. Limit is 5!");
            return;
        }

        //instantiate a new card UI on canvas, set its parent to Player Hand
        Card newCardInHand = GameObject.Instantiate(card, parent: this.transform);
        newCardInHand.SetOwner(true);

        cardsInHand.Add(newCardInHand);
        newCardInHand.positionInHand = cardsInHand.Count - 1;//position = last position;
        //update card UI positions here
        UpdateCardPosition();
    }

    //when the player plays a card, remove it from the cardsInHand
    public void RemoveCard(Card card)
    {
        int index = card.positionInHand;
        if(index < 0 || index > cardsInHand.Count)
        {
            Debug.Log("Invalid index in " + name);
            return;
        }
        else
        {
            cardsInHand.RemoveAt(index);
        }
        //update card UI position here
        UpdateCardPosition();
    }

     
    //update positions of cards on screen
    private void UpdateCardPosition()
    {
        //reset the card positions 
        for(int i = 0; i < cardsInHand.Count; i++)
        {
            cardsInHand[i].positionInHand = i;
        }

        for (int i = 0; i < cardsInHand.Count; i++)
        {
            //snap the card UI to the setup positions.
            cardsInHand[i].transform.SetParent(cardPositions[i].transform, worldPositionStays: false);
            //reset original position to new slot
            cardsInHand[i].GetComponent<DragableDropable>().ResetOriginalPosition();
        }
    }
}
