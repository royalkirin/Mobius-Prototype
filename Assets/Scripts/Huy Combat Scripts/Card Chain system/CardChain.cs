using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//handle the logics for the card chain

[RequireComponent(typeof(CardChainUI))]
public class CardChain : MonoBehaviour
{
    Card lastCardPlayed;
    int totalCardInChain = 0;

    bool playerGoesFirst = true;//who start this chain?
    bool lastCardBelongToPlayer = true;//when end the chain, depends on this we activate card effects
    bool lastCardIsTrapCard = false;//when a trap card is activated, special effect is handled.


    public List<Card> playerCards = new List<Card> ();
    public List<Card> enemyCards = new List<Card>();


    //when a card is played in the chain, the card moves to this parent
    //place holder for all the cards in the inspector. Doesn't affect mechanics.
    [SerializeField] GameObject Cards = null;


    TurnManager turnManager;

    CardPlayer playerCardPlayer; //card player component of player
    CardPlayer enemyCardPlayer; //card player component of enemy
    TutorialCardPlayer playercp;
    GameObject passBtn; //pass btn for Player to surrender the chain


    //old way of handle UI
    BattleGround battleGround;

    //HANDLE UI
    CardChainUI chainUI;

    //between delays, cannot play any cards.
    public bool chainEnding = false;

    //invoke EnemyAI when necessary.
    EnemyAI enemyAI;

    TutorialEnemyAI tenemyAI;


    ///////////////////////////
    //if invincible, automatically wins the chain when its played
    public enum InvincibleCard
    {
        None, //default
        Attack, //whenever someone plays this card, he wins the chain
        Defense,
        Support
    }
    public InvincibleCard playerInvincibleCard = InvincibleCard.None;
    public InvincibleCard enemyInvincibleCard = InvincibleCard.None;
    ///////////////////////////


    //singleton
    public static CardChain Instance { get; private set; }
    private void Awake() {
        Instance = this;
    }


        private void Start()
    {
        FindVariables();

    }

    private void FindVariables()
    {
        if (Cards is null)
        {
            Debug.Log("Missing Cards reference to game obj in " + name);
        }

        turnManager = FindObjectOfType<TurnManager>();
        if(turnManager is null)
        {
            Debug.Log("Missing turn manager in " + name);
        }

        playerCardPlayer = GameObject.FindWithTag("Player").GetComponent<CardPlayer>();
        if(playerCardPlayer is null)
        {
            Debug.Log("Cannot find player's card player in " + name);
            playercp = GameObject.FindWithTag("Player").GetComponent<TutorialCardPlayer>();
        }

        enemyCardPlayer = GameObject.FindWithTag("Enemy").GetComponent<CardPlayer>();
        if (enemyCardPlayer is null)
        {
            Debug.Log("Cannot find enemy's card player in " + name);
        }

        passBtn = GameObject.Find("Pass btn");
        if (passBtn is null)
        {
            Debug.Log("Could not find Pass btn in the chain. Check Card Play Canvas.");
        }
        else
        {
            //when player click this btn, signify he wants to end the chain.
            //passBtn will be activated/deactivated in TurnManager.cs
            passBtn.GetComponent<Button>().onClick.AddListener(PassBtnClick);
            Debug.Log("CardChain.cs add Btn Clink on Pass Btn");
        }

        battleGround = GameObject.FindWithTag("Battleground").GetComponent<BattleGround>();
        if (battleGround is null)
        {
            Debug.Log("Cannot find Battle Ground component in " + name);
        }

        chainUI = GetComponent<CardChainUI>();
        if(chainUI is null)
        {
            Debug.Log("Missing Card Chain UI component in " + name);
        }

        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemyList.Length > 1)
        {
            Debug.LogError("Why are there more than 1 game object with tag Enemy in the game?");
        }
        else
        {
            enemyAI = enemyList[0].GetComponent<EnemyAI>();
            if(enemyAI is null)
            {
                Debug.Log("Cannot find EnemyAI in " + name);
                tenemyAI = enemyList[0].GetComponent<TutorialEnemyAI>();
            }
        }
    }

    //create new chain when chain ends
    private void CreateNewChain()
    {
        lastCardPlayed = null;
        totalCardInChain = 0;
        playerCards.Clear();
        enemyCards.Clear();
    }

    //CardPlayer try to play a card into the chain.
    //return true if successfully played
    //return false otherwise
    //if trying to activate a trap card, isTrapCard = true, and pass in TrapCard component.
    public bool TryAddCardToChain(Card card, bool isTrapCard = false, TrapCard trapcard = null)
    {
        if (chainEnding)
        {
            return false;
        }

        bool isCardValid = CardValidityCheck(card, isTrapCard, trapcard);
        if (!isCardValid)
        {
            return false;
        }

        //from now, either totalcardinchain = 0 (any card can be played)
        //or the card counters the last card in the chain -> we can also play it



        if (totalCardInChain == 0)//take note that this Chain starts by player or enemy
        {
            playerGoesFirst = card.belongToPlayer;
        }
        totalCardInChain++;



        //add card to player list
        if (card.belongToPlayer)
        {
            playerCards.Add(card);
            lastCardBelongToPlayer = true;

        }
        else
        {
            enemyCards.Add(card);
            lastCardBelongToPlayer = false;
        }
        lastCardPlayed = card;
        HandleInstantEffect(card, isTrapCard);





        if (isTrapCard)
        {
            lastCardIsTrapCard = true;
        }


        card.gameObject.transform.SetParent(Cards.transform);//move the card played into the Cards game obj for reference
        //chainUI.InitiateCardImage(card);

        bool isInvincible = CheckInvincibleCard(card, isTrapCard);

        card.gameObject.SetActive(false);
        



        //not a trap, not invincible, we continue playing the chain
        if(isTrapCard is false && isInvincible is false)
        {
            //signify to turn manager that player A plays a card, now its turn to counter.
            StartCoroutine(EndReactionTurnInTime(0.5f));
        }
        else if(isTrapCard)//player succesfully plays a trap card. The chain ends right now.
        {
            Debug.LogWarning("PLAYER PLAYS TRAP CARD. CHAIN ENDS!");
            ChainEnd(lastCardBelongToPlayer, trapCardBypass: true);
        }else //player plays an invincible card. ends right now.
        {
            Debug.LogWarning("PLAYER PLAYS INVINCIBLE CARD. CHAIN ENDS!");
            ChainEnd(lastCardBelongToPlayer, trapCardBypass: false, invincibleCardBypass: true);
        }


        return true;
    }

    //trigger instant effect of the card.
    private void HandleInstantEffect(Card card, bool isTrapCard) {
        if (isTrapCard) {
            return;
        }

        //do not trigger instant effect of the first card in chain
        if(totalCardInChain == 1) {
            return;
        }

        //trigger PREVIOUS card
        if(totalCardInChain == 2) {
            if (card.belongToPlayer) {
                //trigger first card of enemy 
                enemyCardPlayer.ActivateInstantEffect(enemyCards[enemyCards.Count - 1]);
            } else {
                //trigger first card of player 
                if(playerCardPlayer != null)
                {
                   playerCardPlayer.ActivateInstantEffect(playerCards[playerCards.Count - 1]);
                }
                else
                {
                    playercp.ActivateInstantEffect(playerCards[playerCards.Count - 1]);
                }
                
            }
        }

        //Trigger current card\
        if (card.belongToPlayer) {
            if (playerCardPlayer != null)
            {
                playerCardPlayer.ActivateInstantEffect(card);
            }
            else
            {
                playercp.ActivateInstantEffect(card);
            }
        } 
        else 
        {
            enemyCardPlayer.ActivateInstantEffect(card);
        }
    }




    //after player successfully play a card
    //if that card is an invincible card, end the chain.
    //(for ronin's inner peace)
    private bool CheckInvincibleCard(Card card, bool isTrapCard)
    {
        //enemy card = cannot be invincible
        if (!card.belongToPlayer){
            return false;
        }
        if (isTrapCard)//invincible card cannot be trap card
        {
            return false;
        }

        if(playerInvincibleCard == InvincibleCard.Attack && card is AttackCard)
        {
            Debug.LogWarning("Implement inner peace here");
            return true;
        }
        return false;
    }

    //Helper function for ONLY TryAddCardToChain() 
    private bool CardValidityCheck(Card card, bool isTrapCard, TrapCard trapcard)
    {
        if(isTrapCard == true && trapcard == null)
        {
            Debug.LogWarning("Try to activate trap card BUT missing Trapcard component!");
            return false;
        }


        //prevent player/enemy from playing 2 cards at once
        if (totalCardInChain != 0 && card.belongToPlayer == lastCardBelongToPlayer)
        {
            if (lastCardBelongToPlayer)
            {
                Debug.LogWarning("Player is trying to play 2 cards at once in the chain!");
            }
            else
            {
                Debug.LogWarning("Enemy is trying to play 2 cards at once in the chain!");
            }
            return false;
        }

        if(totalCardInChain == 0 && isTrapCard)
        {
            Debug.LogWarning("Cannot play trap card as the FIRST card in the chain!");
            return false;
        }


        //Check for counters
        if (totalCardInChain != 0)
        {
            //check counters for normal card
            if(isTrapCard == false)
            {
                bool counter = Card.Counter(lastCardPlayed, card);
                if (!counter)// if card doesn't counter lastCardPlayed, we cannot play it
                {
                    Debug.Log("Fail to counter");
                    return false;
                }
            }

            if(isTrapCard && trapcard != null)
            {
                //isTrapCard = true, trapcard is not null
                //check counters for trap card, remember, each trap card can counter ANY type of card.
                if (CheckTrapCardCounter(lastCardPlayed, trapcard) == false)
                {
                    Debug.Log("Trap card fail to counter");
                    return false;
                }
            }

        }

        //show card UI
        bool isUIValid = chainUI.PlayCardUI(card, card.belongToPlayer, isPlayedFaceUp: true);
        if (!isUIValid)
        {
            Debug.Log("Chain UI stops chain from playing!");
            return false;
        }
        return true;
    }


    //check if the trapcard Counters the LastcardPlayed.
    // Edited this in attempt to get trap card pulse working.
    public static bool CheckTrapCardCounter(Card lastCardPlayed, TrapCard trapCard)
    {
        if(lastCardPlayed is AttackCard)
        {
            return trapCard.doesCounterAttackCard();
        }
        if(lastCardPlayed is DefenseCard)
        {
            return trapCard.doesCounterDefenseCard();
        }
        if(lastCardPlayed is SupportCard)
        {
            return trapCard.doesCounterSupportCard();
        }
        Debug.LogWarning("Code should be unreachable. Check for errors.");
        return false;
    }


    IEnumerator EndReactionTurnInTime(float sec)
    {
        yield return new WaitForSeconds(sec);
        turnManager.EndReactionTurn(isPlayerEndTurn: lastCardBelongToPlayer);
    }


    //Enemy.AI call this to signify surrender in the current chain -> all playerCards take effect.
    //player calls this to signify surrener in the current chain -> all enemyCards take effect.
    //isPlayer = true means Player wants to surrender the chain -> Enemy wins the chain.
    //trapcardbypass allows us to end the chain right away when a trapcard is activated.
    public void ChainEnd(bool isPlayer, bool trapCardBypass = false, bool invincibleCardBypass = false)
    {
        chainEnding = true;

        if (trapCardBypass is false && invincibleCardBypass is false) //if not a trap card, and not invincible card,
                                                                     //check for validities as usual
        {

            if (isPlayer && lastCardBelongToPlayer)
            {
                Debug.Log("Player ends when it's enemy turn to counter!!");
                return;
            }
            if (!isPlayer && !lastCardBelongToPlayer)
            {
                Debug.Log("Enemy ends when it's player turn to counter!!");
                return;
            }
        }
        else if (trapCardBypass is true)
        {
            Debug.Log("Trap card bypass is true. We end the chain.");
        }
        else //invincible bypass is true
        {
            Debug.Log("Invincible card bypass is true. We end the chain.");
        }

        //either trapcardbypass is true -> we end the chain
        //or invincibleCardBypass is true -> we end the chain
        //or, all conditions are passed.



        //activate trap card effect, remove it from the list
        if (lastCardIsTrapCard)
        {
            Debug.Log("Activating TRAP CARD EFFECT...");
            lastCardPlayed.GetComponent<TrapCard>().ActivateTrapCard();
            if (lastCardBelongToPlayer) //remove trap card, index = end of the list of cards.
            {
                playerCards.RemoveAt(playerCards.Count - 1);
            }
        }

        //activate other cards as usual
        if (lastCardBelongToPlayer)
        {
            foreach(Card card in playerCards)
            { if(playerCardPlayer != null)
                {
                 playerCardPlayer.CardTakesEffect(card);
                }
                else
                {
                    playercp.CardTakesEffect(card);
                }
            }
        }
        else
        {
            foreach (Card card in enemyCards)
            {
                Debug.Log("1");
                enemyCardPlayer.CardTakesEffect(card);
            }
        }

        foreach (Card card in playerCards)
        {
            Destroy(card.gameObject);
        }
        foreach (Card card in enemyCards)
        {
            Destroy(card.gameObject);
        }

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BattleCameraControl>().MoveCameraBacktoNeutral();

        RemoveBuffInvincibleCard(isPlayer);

        StartCoroutine(ChainEndDelay(5f));
    }

    //invincible card is successfully activated, we remove the buff from the character.
    private void RemoveBuffInvincibleCard(bool isPlayer)
    {
        string friendlyTag = "PlayerCharacter";
        if (isPlayer)
        {
            playerInvincibleCard = InvincibleCard.None;
        }
        else
        {
            enemyInvincibleCard = InvincibleCard.None;
            friendlyTag = "EnemyCharacter";
        }
        //remove the buff from character
        GameObject friendlyChar = GameObject.FindWithTag(friendlyTag);
        CharacterBuffs characterBuffs = friendlyChar.GetComponent<CharacterBuffs>();
        characterBuffs.SetNextAttackCardCannotBeCountered(false);
    }


    //end the chain after a few secs
    //reset the chains
    IEnumerator ChainEndDelay(float sec)
    {
        yield return new WaitForSeconds(sec);
        chainEnding = false;
        CreateNewChain();
        chainUI.ResetChainUI(playerGoesFirst);

        //if player's chain ends, he can choose to end his turn, or starts another chain.
        if (playerGoesFirst)
        {
            turnManager.OnPlayerChainEnds();
        }

        //if enemy's turn, he automatically end his turn when his chain ends.
        //update this for more advanced AI 
        if (!playerGoesFirst)
        {
            //turnManager.DefaultChangeTurn();
            if(enemyAI != null)
            {
              StartCoroutine(enemyAI.OnEnemyTurn(8f));
            }
            else
            {
                StartCoroutine(tenemyAI.OnEnemyTurn(8f));
            }
            
        }

    }


    public Card GetLastCardPlayed()
    {
        if(totalCardInChain != 0)
        {
            return lastCardPlayed;
        }
        Debug.Log("trying to access last card in chain but it's null");
        return null;
    }

    //completely new turn, reset everything.
    //only call from TurnManager.
    public void OnNewTurn(bool isPlayerTurn)
    {
        Debug.Log("Card Chain - On New Turn - called");
        lastCardPlayed = null;
        totalCardInChain = 0;
        playerGoesFirst = isPlayerTurn;
        lastCardBelongToPlayer = false; //no cards played yet
        playerCards.Clear();
        enemyCards.Clear();
        

    }


    public int GetTotalCard()
    {
        return totalCardInChain;
    }


    private void PassBtnClick()
    {
        if (chainEnding)
        {
            return;
        }
        if (lastCardBelongToPlayer)
        {
            Debug.LogWarning("cannot pass when last card belong to player");
            return;
        }
        else
        {
            ChainEnd(isPlayer: true);
        }
        
        

    }

    public void GetChainUINewRound(bool bIsPlayerTurn)
    {
        chainUI.SetChainStartPosition(bIsPlayerTurn);
    }
}
