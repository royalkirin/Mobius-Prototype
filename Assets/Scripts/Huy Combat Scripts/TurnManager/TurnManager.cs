using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this class managers the turn of the game, player or enemy, start or end of each one.
//Depends on which turn it is, certain features of the game will be deactivated/activated for players
//For example, when it's enemy turn, we deactivate the drop zone for the cards -> Player cannot play any cards.
public class TurnManager : MonoBehaviour
{
    public bool isPlayerTurn = true;
    

    public GameObject[] PlayerActivateList;//all the things we activate when it's player turn, deactivate on Enemy Turn
    public GameObject[] PlayerReactionActivateList; //all the things we activate when it's player turn/reaction turn, deactivate on Enemy reaction turn
    EnemyAI enemyAI;//enemy in the scene

    //when someone attacks another, the other can have a reaction turn to choose to counter the card
    public bool isPlayerReactTurn = false;

    Deck playerDeck;
    CardChain cardChain;

    GameObject passBtn; //pass btn for Player to surrender the chain

    //end turn btn for Player to end the turn.
    //the end turn btn only available when it's beginning of the turn, 0 cards in chain, and it's player turn
    //that he doesn't want to play a card.
    GameObject endTurnBtn;

    //does player have a valid number of cards in hand? ( <= Maximum = 5)
    public bool playerNumberOfCardsValid = false;
    bool isDiscardModeON = false;
    PlayerHand playerHand;
    DiscardHandler discardHandler;
    private void Start()
    {
        FindVariables();
        ManageFeaturesChangingTurn();
        PrintTurn();
        cardChain.OnNewTurn(isPlayerTurn);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            playerNumberOfCardsValid = true;
        }
    }

    private void FindVariables()
    {
        PlayerActivateList = GameObject.FindGameObjectsWithTag("PlayerTurn");
        PlayerReactionActivateList = PlayerActivateList;
        enemyAI = GameObject.FindWithTag("Enemy").GetComponent<EnemyAI>();
        playerDeck = GameObject.FindWithTag("PlayerDeck").GetComponent<Deck>();
        cardChain = GameObject.FindWithTag("CardChain").GetComponent<CardChain>();
        if (cardChain is null)
        {
            Debug.Log("Cannot find cardChain in " + name);
        }

        passBtn = GameObject.Find("Pass btn");
        if (passBtn is null)
        {
            Debug.Log("Could not find Pass btn. Check Card Play Canvas.");
        }
        CheckPassBtnActivated();

        endTurnBtn = GameObject.Find("End Turn btn");
        if(endTurnBtn is null)
        {
            Debug.Log("Could not find End Turn btn. Check Card Play Canvas.");
        }
        CheckEndTurnBtnActivated();

        playerHand = GameObject.FindWithTag("PlayerHand").GetComponent<PlayerHand>();
        if(playerHand is null)
        {
            Debug.Log("Cannot find Player hand in " + name);
        }

        discardHandler = GameObject.FindGameObjectWithTag("DiscardHandler").GetComponent<DiscardHandler>();
        if(discardHandler is null)
        {
            Debug.Log("Cannot find discard handler in " + name);
        }
        else
        {
            discardHandler.gameObject.SetActive(false);
        }
    }



    //manage basic logic for changing turn from both player and Enemy.
    //Player have a separate function he should call to change turn.
    public void DefaultChangeTurn()
    {
        Debug.Log("default change turn");
        isPlayerTurn = !isPlayerTurn;
        isPlayerReactTurn = isPlayerTurn;
        PrintTurn();


        CardLimitCheck();

    }




    //we turn off all features UNTIL players discard extra cards from his hand.
    private void CardLimitCheck()
    {
        foreach (GameObject obj in PlayerActivateList)
        {
            obj.SetActive(false);
        }
        endTurnBtn.SetActive(false);

        UpdateDiscardStatus();
        if (playerNumberOfCardsValid)
        {
            Debug.Log("Number of cards Validity check: True");
            AfterCheckingSequence();
            
            return;
        }
        else
        {
            SetDiscardMode(true);
            Debug.Log("Number of cards Validity check: False");
            InvokeRepeating(nameof(PrintWaitingMessage), time: 0.5f, repeatRate: 1f);
            StartCoroutine(WaitUntilPlayerHasValidCardNumber());
        }

    }

    private void SetDiscardMode(bool isDisCardModeON)
    {
        this.isDiscardModeON = isDisCardModeON;
        if (isDiscardModeON)
        {
            discardHandler.gameObject.SetActive(true);
            discardHandler.UpdateDiscardMessage();
        }
        else
        {
            discardHandler.gameObject.SetActive(false);
        }

    }

    public bool GetDiscardMode()
    {
        return isDiscardModeON;
    }

    public void UpdateDiscardStatus()
    {
        playerNumberOfCardsValid = (playerHand.cardsInHand.Count <= PlayerHand.MAX_CARD_COUNT);
    }


    private void PrintWaitingMessage()
    {
        Debug.Log("Waiting...");
    }
    //wait until player discards all his extra cards
    IEnumerator WaitUntilPlayerHasValidCardNumber()
    {
        yield return new WaitUntil(() => playerNumberOfCardsValid == true);
        AfterCheckingSequence();
    }

    //after player has a valid amount of card, start the turn.
    private void AfterCheckingSequence()
    {
        ManageFeaturesChangingTurn();
        if (isPlayerTurn)
        {
            playerDeck.FullDealToPlayer();
        }
        
        CheckPassBtnActivated();
        CheckEndTurnBtnActivated();
        if (!isPlayerTurn)//if it's enemy turn, we signify the enemy AI to play cards.
        {
            StartCoroutine(enemyAI.OnEnemyTurn(8f));
        }
        CancelInvoke(nameof(PrintWaitingMessage));
        SetDiscardMode(false);
    }

    //the player calls this script (by hitting EndTurn tn) to signify changing turn.
    public void PlayerChangeTurn()
    {
        if (!isPlayerTurn)//if player clicks EndTurn during Enemy Turn, we do nothing.
        {
            Debug.Log("Player cannot change turn during Enemy Turn.");
            return;
        }
        if (!isPlayerReactTurn)
        {
            Debug.Log("Player cannot change turn during Enemy Reaction Turn.");
            return;
        }

        //player clicks during his turn, we change turn
        DefaultChangeTurn();
    }


    //Decide which features to be enabled/disabled when swiching turns.
    public void ManageFeaturesChangingTurn()
    {
        
        foreach (GameObject obj in PlayerActivateList)
        {
            obj.SetActive(isPlayerTurn);
        }

        isPlayerReactTurn = isPlayerTurn;
    }


    private void ManageFeaturesReactionTurn()
    {
        Debug.Log("is player reaction turn = " + isPlayerReactTurn);
        foreach(GameObject obj in PlayerReactionActivateList)
        {
            obj.SetActive(isPlayerReactTurn);
            
        }
    }

    //debug in console.
    private void PrintTurn()
    {
        if (isPlayerTurn)
        {
            Debug.Log("Player Turn");
        }
        else
        {
            Debug.Log("Enemy Turn");
        }
    }

    //When player A plays a card, gives player B a chance to react 
    //isPlayerEndTurn = is the turn ended by player, now it's enemy turn?
    public void EndReactionTurn(bool isPlayerEndTurn)
    {
        if(isPlayerEndTurn) //when player ends his reaction turn
        {
            //Debug.Log("Enemy now reacting");
            enemyAI.OnEnemyReactTurn();
            
            isPlayerReactTurn = false;
            ManageFeaturesReactionTurn();
        }
        else  //when enemy ends his reaction turn
        {
            //Debug.Log("Player now reacting");
            isPlayerReactTurn = true;
            //activate pass btn for player
            CheckPassBtnActivated();
            CheckEndTurnBtnActivated();
            ManageFeaturesReactionTurn();
        }
    }

    public void CheckPassBtnActivated()
    {

        if(cardChain.GetTotalCard() <= 0 || !isPlayerReactTurn)
        {
            passBtn.SetActive(false);
        }
        else
        {
            passBtn.SetActive(true);
        }
    }

    public void CheckEndTurnBtnActivated()
    {
        endTurnBtn.SetActive(false);

        if (isPlayerTurn)
        {
            if(cardChain.GetTotalCard() == 0)
            {
                endTurnBtn.SetActive(true);
            }
        }
    }

    public void OnPlayerChainEnds()
    {
        CheckEndTurnBtnActivated();
        CheckPassBtnActivated();
        ManageFeaturesChangingTurn();
    }
}
