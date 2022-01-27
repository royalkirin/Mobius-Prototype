using System;
using System.Collections.Generic;
using UnityEngine;


//represents the current cards in player hand
//there is only 1 game obj with this component: PlayerHand prefab (with tag "PlayerHand")
public class PlayerHand : MonoBehaviour
{
    public List<Card> cardsInHand;
    public const int MAX_CARD_COUNT = 5;

    //UI component, positions of the cards in the hand.
    

    //for limited cards, max 5
    [SerializeField] List<GameObject> cardPositionsLimited; 

    //for unlimited cards
    [SerializeField] GameObject cardPositionPrefab;
    [SerializeField] GameObject firstCardPosition;
    [SerializeField] GameObject lastCardPosition;
    [SerializeField] GameObject UnlimitedCards; // parent of all generated prefabs.
    [SerializeField] List<GameObject> generatedPositions; //list of positions between 1st and last

    //for discard
    TurnManager turnManager;

    private void Start()
    {
        FindVariables();
    }

    private void FindVariables()
    {
        if(UnlimitedCards is null)
        {
            Debug.Log("Missing Unlimited Cards obj reference in " + name);
        }
        turnManager = FindObjectOfType<TurnManager>();
        if (turnManager is null)
        {
            Debug.Log("Cannot find Turn manager in " + name);
        }

    }


    //the deck will add card to player hand
    //bool cardLimited = true means that the maximum allowed in player hand in MAX_CARD_COUNT
    //-> true at beginning of each turn, false during the turn, when there is no card limit.
    public void AddCard(Card card, bool cardLimited = true)
    {
        if (cardsInHand.Count >= MAX_CARD_COUNT && cardLimited)
        {
            Debug.Log("Currently have " + cardsInHand.Count + " cards in hand, card limit is reached.");
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
        //Debug.Log("Removing card");
        int index = card.positionInHand;
        if (index < 0 || index > cardsInHand.Count)
        {
            Debug.Log("Invalid index in " + name);
            return;
        }
        else
        {
            cardsInHand.RemoveAt(index);
            Debug.Log("Sucessfully remove card from hand.");
        }
        //update card UI position here
        UpdateCardPosition();
    }


    //update positions of cards on screen
    private void UpdateCardPosition()
    {
        //reset info in each card its position in the hand
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            cardsInHand[i].positionInHand = i;
        }


        if (cardsInHand.Count <= MAX_CARD_COUNT)
        {
            UpdateCardPositionLimited();
        }
        else
        {
            UpdateCardPositionUnlimited();
        }
        

    }

    //update card positions, under assumption that the number of cards is unlimited.
    private void UpdateCardPositionUnlimited()
    {
        int cardCount = cardsInHand.Count;
        //move all cards under Unlimited Game Object, so we dont destroy them
        for(int i = 0; i < cardsInHand.Count; i++)
        {
            cardsInHand[i].transform.SetParent(UnlimitedCards.transform, worldPositionStays: false);
        }

        //destroy all generated prefabs
            //they have indexes from 1 to count-2
        for(int i = 1; i < generatedPositions.Count-1; i++)
        {
            Destroy(generatedPositions[i].gameObject);
        }


        //recalculate, recreate new positions, add them back into the generatedPositions
        generatedPositions.Clear();
            //distance between each card, calculated based on first and last position
        float firstX = firstCardPosition.GetComponent<RectTransform>().anchoredPosition.x;
        float lastX = lastCardPosition.GetComponent<RectTransform>().anchoredPosition.x;
        float xDistance = (lastX - firstX) / (cardCount - 1);
        generatedPositions.Add(firstCardPosition); //add first position
            //generate position 2nd -> next to last
            //and 2nd -> next to last positions 
        for(int i = 0; i < cardCount - 2; i++)
        {
            float xPosition = firstX + xDistance * (i + 1);
            Vector3 anchoredPosition = new Vector3(xPosition, 0f, 0f);
            GameObject curGeneratedPos =  Instantiate(cardPositionPrefab, anchoredPosition, Quaternion.identity);
            curGeneratedPos.transform.SetParent(UnlimitedCards.transform, worldPositionStays: false);
            generatedPositions.Add(curGeneratedPos);
        }

        //add last position
        generatedPositions.Add(lastCardPosition);
        //make the last position the last child of Unlimited, so that the card shows properly on screen
        lastCardPosition.transform.SetParent(this.gameObject.transform, worldPositionStays: false);
        lastCardPosition.transform.SetParent(UnlimitedCards.transform, worldPositionStays: false);


        //update card positions
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            cardsInHand[i].transform.SetParent(generatedPositions[i].transform, worldPositionStays: false);
            //reset original position to new slot
            cardsInHand[i].GetComponent<DragableDropable>().ResetOriginalPosition();
            cardsInHand[i].gameObject.SetActive(true);
        }
    }

    //update card positions, under assumption that the number of cards is within MAX count
    private void UpdateCardPositionLimited()
    {


        for (int i = 0; i < cardsInHand.Count; i++)
        {
            //snap the card ui to the setup positions.
            cardsInHand[i].transform.SetParent(cardPositionsLimited[i].transform, worldPositionStays: false);
            //reset original position to new slot
            cardsInHand[i].GetComponent<DragableDropable>().ResetOriginalPosition();
            cardsInHand[i].gameObject.SetActive(true);
        }
    }

    //Discard a card from the hand, notify TurnManager to check if still need to discard.
    //only use at beginning of new turn, when player have more than 5 cards.
    //Dont use this to remove card during play.
    public void Discard(Card card)
    {
        RemoveCard(card);
        DiscardHandler discardHandler = GameObject.FindGameObjectWithTag("DiscardHandler").
            GetComponent<DiscardHandler>();
        if(discardHandler != null)
        {
            discardHandler.UpdateDiscardMessage();
        }
        Destroy(card.gameObject);
        turnManager.UpdateDiscardStatus();

    }

}
