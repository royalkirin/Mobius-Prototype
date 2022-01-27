using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this class handle how the trapcards behave in the game
public class TrapCardManager : MonoBehaviour
{
    //references to other scripts we will need
    CardChain cardChain;



    //logics for the mechanics
    bool playerHasATrapCard = false;
    bool enemyHasATrapCard = false;

    //TODO: expands this area to actual TrapCard, not Card.
    TrapCard playerTrapCard;
    TrapCard enemyTrapCard;


    //handling the UIs
    [SerializeField] Image playerTrapCardImage;
    [SerializeField] Image enemyTrapCardImage;
    //placeholder to move the trapcard UIs under, so the card UI disappears on screen when played.
    [SerializeField] GameObject trapCards;


    private void Start()
    {
        FindVariables();
    }

    private void FindVariables()
    {
        cardChain = GameObject.FindWithTag("CardChain").GetComponent<CardChain>();
        if (cardChain is null)
        {
            Debug.Log("Cannot find cardChain in " + name);
        }
    }



    //Try to play a trap card on the battle.
    //return true if played successfully
    //return false if not
    public bool PlayTrapCardFaceDown(Card card)
    {
        //if a player already have a trap card, he cannot plays another trapcard.
        if(card.belongToPlayer && playerHasATrapCard)
        {
            Debug.Log("Player already have a trap card activated.");
            return false;
        }
        if(!card.belongToPlayer && enemyHasATrapCard)
        {
            Debug.Log("Player already have a trap card activated.");
            return false;
        }

        //double check conditions, in case someone calls this function from a wrong script.
        if(cardChain.GetTotalCard() != 0)
        {
            Debug.Log("Cannot play trap card when Card Chain is not empty!");
            return false;
        }

        //get trap card component
        TrapCard trapCard = card.gameObject.GetComponent<TrapCard>();
        if (trapCard is null)
        {
            Debug.LogWarning("The card in play does not have Trap Card component");
            return false;
        }

        //can play the card now
        if (card.belongToPlayer)
        {
            playerHasATrapCard = true;
            playerTrapCardImage.gameObject.SetActive(true);
            playerTrapCardImage.gameObject.GetComponent<TrapCardMouseInteraction>().SetCard(card);
            playerTrapCard = trapCard;
            //move UIs
            card.transform.SetParent(trapCards.transform, worldPositionStays: false);
            card.gameObject.SetActive(false);
            //end the turn
            FindObjectOfType<TurnManager>().PlayerChangeTurn();
        }
        else
        {
            Debug.LogWarning("ENEMY IS TRYING TO PLAY A TRAP CARD.");
            enemyHasATrapCard = true;
            enemyTrapCardImage.gameObject.SetActive(true);
            enemyTrapCardImage.gameObject.GetComponent<TrapCardMouseInteraction>().SetCard(card);
            enemyHasATrapCard = trapCard;
            //move UIs
            card.transform.SetParent(trapCards.transform, worldPositionStays: false);
            card.gameObject.SetActive(false);
        }

        

        return true;
    }

    //the player activate trap card by right click it
    //(check TrapCardMouseInteraction.cs)
    public bool TryActivateTrapCard(bool isPlayer)
    {
        if (!isPlayer)
        {
            //right now Enemy cannot play/activate any trap card
            Debug.LogWarning("Enemy is trying to activate a trap card. Not allowed.");
            return false;
        }

        Debug.Log("Trying to activate trap card");


        bool isActivated = cardChain.TryAddCardToChain(playerTrapCard.gameObject.GetComponent<Card>(),
                                                        isTrapCard: true, trapcard: playerTrapCard);

        if (isActivated)
        {
            //clear trap card of player
            playerTrapCard = null;
            playerTrapCardImage.gameObject.GetComponent<TrapCardMouseInteraction>().SetCard(null);
            playerTrapCardImage.gameObject.SetActive(false);
            playerHasATrapCard = false;
        }
        return false;
    }



    //dont need to explain this function
    public bool DoesPlayerHaveATrapCard()
    {
        return playerHasATrapCard;
    }

    //dont need to explain this function
    public bool DoesEnemyHaveATrapCard()
    {
        return enemyHasATrapCard;
    }

    public void DiscardPlayerTrap()
    {
        if (!playerHasATrapCard)
        {
            return;
        }
        playerHasATrapCard = false;
        Destroy(playerTrapCard.gameObject);
        playerTrapCard = null;
        playerTrapCardImage.gameObject.SetActive(false);
    }
}
