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

    public List<Card> playerCards = new List<Card> ();
    public List<Card> enemyCards = new List<Card>();


    //when a card is played in the chain, the card moves to this parent
    //place holder for all the cards in the inspector. Doesn't affect mechanics.
    [SerializeField] GameObject Cards = null;


    TurnManager turnManager;

    CardPlayer playerCardPlayer; //card player component of player
    CardPlayer enemyCardPlayer; //card player component of enemy

    GameObject passBtn; //pass btn for Player to surrender the chain

    //OLD WAY TO HANDLE UI
    BattleGround battleGround = null;

    //NEW WAY TO HANDLE UI
    CardChainUI chainUI;
    

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
            passBtn.GetComponent<Button>().onClick.AddListener(() => ChainEnd(isPlayer:true));
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
    }

    public void CreateNewChain()
    {
        lastCardPlayed = null;
        totalCardInChain = 0;
        playerCards.Clear();
        enemyCards.Clear();
    }

    //CardPlayer try to play a card into the chain.
    //return true if successfully played
    //return false otherwise
    public bool TryAddCardToChain(Card card)
    {

        //prevent player/enemy from playing 2 cards at once
        if(totalCardInChain != 0 && card.belongToPlayer == lastCardBelongToPlayer)
        {
            Debug.Log("Someone is trying to play 2 cards at once in the chain!");
            return false;
        }

        if(totalCardInChain != 0) //if it's not the first card, it needs to counter the last card.
        {
            //then we check for counter
            bool counter = Card.Counter(lastCardPlayed, card);
            if (!counter)// if card doesn't counter lastCardPlayed, we cannot play it
            {
                Debug.Log("Fail to counter");
                return false;
            }
        }
        //from now, either totalcardinchain = 0 (any card can be played)
        //or the card counters the last card in the chain -> we can also play it

        //show card UI
        bool isUIValid = chainUI.PlayCardUI(card, card.belongToPlayer, isPlayedFaceUp: true);
        if (!isUIValid)
        {
            Debug.Log("Chain UI stops chain from playing!");
            return false;
        }

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
        card.gameObject.transform.SetParent(Cards.transform);//move the card played into the Cards game obj for reference
        //chainUI.InitiateCardImage(card);



        card.gameObject.SetActive(false);

        //signify to turn manager that player A plays a card, now its turn to counter.
        StartCoroutine(EndReactionTurnInTime(3f));

        return true;
    }

    IEnumerator EndReactionTurnInTime(float sec)
    {
        yield return new WaitForSeconds(sec);
        turnManager.EndReactionTurn(isPlayerEndTurn: lastCardBelongToPlayer);
    }


    //Enemy.AI call this to signify surrender in the current chain -> all playerCards take effect.
    //player calls this to signify surrener in the current chain -> all enemyCards take effect.
    //isPlayer = true means Player wants to surrender the chain -> Enemy wins the chain.
    public void ChainEnd(bool isPlayer)
    {
        if(isPlayer && lastCardBelongToPlayer)
        {
            Debug.Log("Player ends when it's enemy turn to counter!!");
            return;
        }
        if(!isPlayer && !lastCardBelongToPlayer)
        {
            Debug.Log("Enemy ends when it's player turn to counter!!");
            return;
        }



        if (lastCardBelongToPlayer)
        {
            foreach(Card card in playerCards)
            {
                playerCardPlayer.CardTakesEffect(card);
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


        CreateNewChain();
        chainUI.ResetChainUI();

        //if player's chain ends, he can choose to end his turn, or starts another chain.
        if (playerGoesFirst)
        {
            turnManager.OnPlayerChainEnds();
        }

        //if enemy's turn, he automatically end his turn when his chain ends.
        //update this for more advanced AI 
        if (!playerGoesFirst)
        {
            turnManager.DefaultChangeTurn();
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

}
