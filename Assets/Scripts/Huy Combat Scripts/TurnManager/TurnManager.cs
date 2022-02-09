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
    //when someone attacks another, the other can have a reaction turn to choose to counter the card
    public bool isPlayerReactTurn = false;


    public GameObject[] PlayerActivateList;//all the things we activate when it's player turn, deactivate on Enemy Turn
    public GameObject[] PlayerReactionActivateList; //all the things we activate when it's player turn/reaction turn, deactivate on Enemy reaction turn
    EnemyAI enemyAI;//enemy in the scene

    

    Deck playerDeck;
    CardChain cardChain;

    GameObject passBtn; //pass btn for Player to surrender the chain

    //end turn btn for Player to end the turn.
    //the end turn btn only available when it's beginning of the turn, 0 cards in chain, and it's player turn
    //that he doesn't want to play a card.
    GameObject endTurnBtn;

    //Objects that deal with the Scrolling Functionality
    GameObject uScrollUpBtn;
    GameObject uScrollDownBtn;

    //does player have a valid number of cards in hand? ( <= Maximum = 5)
    public bool playerNumberOfCardsValid = false;
    bool isDiscardModeON = false;
    PlayerHand playerHand;
    DiscardHandler discardHandler;

    //integrate trapcard
    TrapCardManager trapCardManager;
    GameObject TrapCardOptionsHandler;
    public bool didPlayerChooseATrapOption = false; //did player choose?
    public bool playerChooseToSkipTurn = false;//player options
    //help managing character Buffs from trapcards
    CharacterBuffs playerCharacterBuffs;
    CharacterBuffs enemyCharacterBuffs;
    


    //these bools are for the stages of a turn 
    //the 2nd stage have to wait until the first stage finish to continue, and so on...
    public bool firstStageCheckTrapCard = true; //some character skips this first stage
    public bool firstStageFinished = false;
    public bool secondStageCheckCardLimit = true;
    public bool secondStageFinished = false;


    private void Start()
    {
        FindVariables();
        ManageFeaturesChangingTurn();
        PrintTurn();
        cardChain.OnNewTurn(isPlayerTurn);
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

        uScrollUpBtn = GameObject.Find("Scroll Up");
        uScrollDownBtn = GameObject.Find("Scroll Down");
        if (uScrollUpBtn is null || uScrollDownBtn is null)
        {
            Debug.Log("Could not find one or both Scroll Buttons;. Check Card Play Canvas.");
        }
        CheckScrollBtnsActivated();

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

        trapCardManager = GameObject.FindWithTag("TrapCardManager").GetComponent<TrapCardManager>();
        if (trapCardManager is null)
        {
            Debug.LogWarning("Cannot find TrapCard Manager in " + name);
        }


        TrapCardOptionsHandler = GameObject.FindWithTag("TrapCardOptionsHandler");
        if(TrapCardOptionsHandler is null)
        {
            Debug.Log("Cannot find Trap Card options Handler in " + name);
        }
        else
        {
            TrapCardOptionsHandler.SetActive(false);
        }


        
        playerCharacterBuffs = GameObject.FindWithTag("PlayerCharacter").GetComponent<CharacterBuffs>();
        if(playerCharacterBuffs is null)
        {
            Debug.Log("Cannot find Player Character Buffs in " + name);
        }
        enemyCharacterBuffs = GameObject.FindWithTag("EnemyCharacter").GetComponent<CharacterBuffs>();
        if (playerCharacterBuffs is null)
        {
            Debug.Log("Cannot find Enemy Character Buffs in " + name);
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

        StartNewTurnStages();
    }

    //before we change the Turn from player A -> B, we remove some buffs from player A
    //player A can maybe have some buffs that last a turn, for example, Inner Peace.
    private void RemoveBuffs()
    {
        if (isPlayerTurn)
        {
            playerCharacterBuffs.RemoveBuffsEndOfTurn();
        }
        else
        {
            enemyCharacterBuffs.RemoveBuffsEndOfTurn();
        }
    }

    private void StartNewTurnStages()
    {
        if (firstStageCheckTrapCard == true)
        {
            firstStageFinished = false;
            FirstStageTrapCardCheck();
        }
        StartCoroutine(SecondStageCardLimitCheck());
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
        CheckScrollBtnsActivated();
        if (!isPlayerTurn)//if it's enemy turn, we signify the enemy AI to play cards.
        {
            StartCoroutine(enemyAI.OnEnemyTurn(8f));
        }
        CancelInvoke(nameof(PrintWaitingMessage));
        SetDiscardMode(false);
    }



    //the player calls this script (by hitting EndTurn tn) to signify changing turn.
    //the trapcardManager call this to end player turn after he plays a trap card.
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
        RemoveBuffs();
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
        //Debug.Log("is player reaction turn = " + isPlayerReactTurn);
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
            CheckScrollBtnsActivated();
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

    public void CheckScrollBtnsActivated()
    {
        uScrollUpBtn.SetActive(false);
        uScrollDownBtn.SetActive(false);

        if (isPlayerTurn)
        {
            if (cardChain.GetTotalCard() > 5)
            {
                uScrollUpBtn.SetActive(true);
                uScrollDownBtn.SetActive(true);
            }
        }
    }

    public void OnPlayerChainEnds()
    {
        CheckEndTurnBtnActivated();
        CheckPassBtnActivated();
        CheckScrollBtnsActivated();
        ManageFeaturesChangingTurn();
    }





    #region FIRST_STAGE_TRAP_CARD_OPTIONS

    //check if player has a trap card at the beginning of his turn
    //if so, he either discard it and play, or skip his turn.
    private void FirstStageTrapCardCheck()
    {
        //deactivate features until the check is finished
        foreach (GameObject obj in PlayerActivateList)
        {
            obj.SetActive(false);
        }
        endTurnBtn.SetActive(false);


        CheckForRoninInnerPeace();

        //check for general trap card.
        if (isPlayerTurn && trapCardManager.DoesPlayerHaveATrapCard())
        {
            didPlayerChooseATrapOption = false;
            Debug.LogWarning("Implement trap card check here");
            TrapCardOptionsHandler.SetActive(true);
            StartCoroutine(WaitUntilPlayerReactToDiscardTrapCardOptions());
        }
        else
        {
            ManageFeaturesChangingTurn();
            firstStageFinished = true;
        }
    }

    private bool CheckForRoninInnerPeace()
    {
        if (trapCardManager.DoesPlayerHaveATrapCard())
        {
            TrapCard playerTrapCard = trapCardManager.GetPlayerTrapcard();
            if(playerTrapCard != null && playerTrapCard is Ronin_innerPeace_trapcard)
            {
                //it is Ronin's Inner Peace
                //Handle Ronin's Inner Peace: automatically activate  and discard it

                trapCardManager.TryActivateRoninInnerPeace(isPlayerTurn);
                return true;
            }
        }
        return false;
    }

    IEnumerator WaitUntilPlayerReactToDiscardTrapCardOptions()
    {
        yield return new WaitUntil(() => didPlayerChooseATrapOption == true);
        if (playerChooseToSkipTurn)
        {
            Debug.Log("Player chose to skip turn");
            PlayerChangeTurn();
        }
        else
        {
            Debug.Log("Player chose to discard");
            trapCardManager.DiscardPlayerTrap();
        }
        TrapCardOptionsHandler.SetActive(false);
        firstStageFinished = true;
    }

    private void SetDiscardMode(bool isDisCardModeON)
    {
        this.isDiscardModeON = isDisCardModeON;
        if (isDiscardModeON)
        {
            StartCoroutine(ActivateDiscardModeObjects(2f));
        }
        else
        {
            discardHandler.gameObject.SetActive(false);
        }

    }
    //we have to activate a little bit later to handle race condition
    //to make sure that we read the player hand count after we remove the card 
    IEnumerator ActivateDiscardModeObjects(float sec)
    {
        yield return new WaitForSeconds(sec);
        discardHandler.gameObject.SetActive(true);
        discardHandler.UpdateDiscardMessage();
    }

    public bool GetDiscardMode()
    {
        return isDiscardModeON;
    }


    #endregion



    #region SECOND_STAGE_CARD_LIMIT

    IEnumerator SecondStageCardLimitCheck()
    {
        //wait until the first stage is finished
        yield return new WaitUntil(() => firstStageFinished);

        //start 2nd stage
        Debug.LogWarning("2nd stage, CARD LIMIT CHECK started");
        if (secondStageCheckCardLimit)
        {
            secondStageFinished = false;

            CardLimitCheck();
            secondStageFinished = true;

        }
        Debug.LogWarning("2nd stage, CARD LIMIT CHECK finished");
    }


    //we turn off all features UNTIL players discard extra cards from his hand.
    //only check if it's enemy turn.
    private void CardLimitCheck()
    {
        foreach (GameObject obj in PlayerActivateList)
        {
            obj.SetActive(false);
        }
        endTurnBtn.SetActive(false);

        UpdateDiscardStatus();//should the player discard?


        //if his turn, no need to discard   
        //if he has valid amount of card, no need to discard.
        if (isPlayerTurn || playerNumberOfCardsValid)
        {
            Debug.Log("Number of cards Validity check: True");
            AfterCheckingCardLimitSequence();

            return;
        }
        else //he should discard. Wait until he discard, then move on.
        {
            SetDiscardMode(true);
            Debug.Log("Number of cards Validity check: False");
            InvokeRepeating(nameof(PrintWaitingMessage), time: 0.5f, repeatRate: 1f);//print Waiting... on console
            StartCoroutine(WaitUntilPlayerHasValidCardNumber());//wait until he discard, then activate other features
        }

    }

    private void PrintWaitingMessage()
    {
        Debug.Log("Waiting...");
    }
    //wait until player discards all his extra cards
    IEnumerator WaitUntilPlayerHasValidCardNumber()
    {
        yield return new WaitUntil(() => playerNumberOfCardsValid == true);
        Debug.LogWarning("Player now has valid card number");
        AfterCheckingCardLimitSequence();
    }

    //after player has a valid amount of card, start the turn.
    private void AfterCheckingCardLimitSequence()
    {
        //Debug.Log("after checking sequence");
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



    public void UpdateDiscardStatus()
    {
        playerNumberOfCardsValid = (playerHand.cardsInHand.Count <= PlayerHand.MAX_CARD_COUNT);
    }
    #endregion


}
