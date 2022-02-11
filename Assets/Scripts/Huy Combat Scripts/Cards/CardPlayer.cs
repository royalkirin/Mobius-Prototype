using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//belongs to any entities that can play cards
//ex: enemy can have a CardPlayer component to play cards
public class CardPlayer : MonoBehaviour
{
    //the character on the side of this card player. For enemy, friendlyCharacter's value is enemyCharacter
    GameObject friendlyCharacter;
    GameObject enemyCharacter; //the character on the other side.
    [SerializeField] string friendlyTag = null;
    [SerializeField] string enemyTag = null; //for players, enemyTag = EnemyCharacter. For enemy, enemyTag = PlayerCharacter


    BattleGround battleGround = null;//reference to BG to play cards on the map
    TurnManager turnManager;

    //for the player only. Enemy AI will not use this.
    //Playerhand prefab in canvas.
    PlayerHand playerhand;

    //for the enemy AI
    EnemyHand enemyHand;

    CardChain cardChain;
    Deck playerDeck; //deck of the player (enemy do not have a deck yet)

    //handle trap card plays
    TrapCardManager trapCardManager;


    private void Start()
    {
        FindVariables();
    }

    private void FindVariables()
    {
        friendlyCharacter = GameObject.FindWithTag(friendlyTag);
        if (friendlyCharacter is null)
        {
            Debug.Log(name + "cannot find friendly character with tag " + friendlyTag);
        }

        enemyCharacter = GameObject.FindWithTag(enemyTag);
        if (enemyCharacter is null)
        {
            Debug.Log(name + "cannot find enemy with tag " + enemyTag);
        }

        battleGround = GameObject.FindWithTag("Battleground").GetComponent<BattleGround>();
        if (battleGround is null)
        {
            Debug.Log("Cannot find Battle Ground component in " + name);
        }
        turnManager = FindObjectOfType<TurnManager>();

        playerhand = GameObject.FindWithTag("PlayerHand").GetComponent<PlayerHand>();
        if (playerhand is null)
        {
            Debug.LogError("Cannot find Player Hand in " + name);
        }

        enemyHand = GameObject.FindWithTag("EnemyHand").GetComponent<EnemyHand>();
        if (enemyHand is null) {
            Debug.LogError("Cannot find Enemyhand in " + name);
        }


        cardChain = GameObject.FindWithTag("CardChain").GetComponent<CardChain>();
        if (cardChain is null)
        {
            Debug.Log("Cannot find cardChain in " + name);
        }

        playerDeck = GameObject.FindWithTag("PlayerDeck").GetComponent<Deck>();
        if (playerDeck == null)
        {
            Debug.Log("Cannot find Deck for CardPlayer");
        }

        trapCardManager = GameObject.FindWithTag("TrapCardManager").GetComponent<TrapCardManager>();
        if(trapCardManager is null)
        {
            Debug.Log("Cannot find Trap Card Manager in " + name);
        }

    }



    public void CardTakesEffect(Card card)
    {
        //logics for cards face up: dealing dmg, shield up...
        if (card is AttackCard)
        {
            AttackCard attackCard = (AttackCard)card;
            if (enemyCharacter != null)
            {
                attackCard.Play(enemyCharacter);
            }
            else
            {
                Debug.Log("Cannot access enemyCharacter in CardPlayer in " + gameObject.name);
            }
            return;
        }

        if (card is DefenseCard)
        {
            DefenseCard defenseCard = (DefenseCard)card;
            if (friendlyCharacter != null)
            {
                defenseCard.Play(friendlyCharacter);
            }
            else
            {
                Debug.Log("Cannot access friendlyCharacter in CardPlayer in " + gameObject.name);
            }

            return;
        }

        if (card is SupportCard)
        {
            SupportCard supportCard = (SupportCard)card;
            supportCard.Play(playerDeck.gameObject);

        }
    }

    //set up the right owner.
    private void SetupCardOwner(Card card)
    {
        if (enemyTag == "EnemyCharacter")
        {
            card.SetOwner(belongToPlayer: true);
        }
        else
        {
            card.SetOwner(belongToPlayer: false);
        }
    }


    public bool PlayCard(Card card, bool isFaceUp = true)
    {
        SetupCardOwner(card);
        bool playedSuccessfully = false;
        if (isFaceUp)
        {
            Debug.Log("Card Player trying to play face UP");
            playedSuccessfully = PlayCardFaceUp(card);
        }
        else
        {
            Debug.Log("Card Player trying to play face DOWN");
            playedSuccessfully = PlayCardFaceDown(card);
        }

        //if its played down/up, remove it from the player hand
        if (card.BelongToPlayer() && playedSuccessfully)
        {
            //Debug.LogWarning("here");
            playerhand.RemoveCard(card);
        }else if (!card.belongToPlayer && playedSuccessfully) //remove card from enemy hand
        {
            enemyHand.RemoveCard(card.positionInHand);
        }

        return playedSuccessfully;
    }

    //helper function to handle play card face up
    private bool PlayCardFaceUp(Card card)
    {
        bool playedSuccessfully = cardChain.TryAddCardToChain(card);

        if (!playedSuccessfully)
        {
            Debug.Log("CAN'T PLAYED!");
            return false;
        }
        return true;
    }

    //helper function to handle play card face down
    private bool PlayCardFaceDown(Card card)
    {
        if(cardChain.GetTotalCard() != 0)
        {
            Debug.Log("Card chain is not empty. Cannot play face down.");
            return false;
        }
        else
        {
            Debug.Log("Card chain is empty. Can try to play");
            bool playedSuccessfully = trapCardManager.PlayTrapCardFaceDown(card);
            //Debug.LogError(playedSuccessfully);
            return playedSuccessfully;
        }
    }
}
