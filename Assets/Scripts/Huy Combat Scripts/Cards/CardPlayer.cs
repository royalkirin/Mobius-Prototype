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
    [SerializeField] string enemyTag = null; //for players, enemyTag = Enemy. For enemy, enemyTag = PlayerCharacter


    BattleGround battleGround = null;//reference to BG to play cards on the map
    TurnManager turnManager;

    //for the player only. Enemy AI will not use this.
    //Playerhand prefab in canvas.
    PlayerHand playerhand;

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
    }

    //Play a random card, can be attack, defense, spell...
    //assume playing face up for now
    public void PlayCard(Card card, bool isPlayedFaceUp = true)
    {
        //set up the owner of the card being played
        SetupCardOwner(card);

        if (!isPlayedFaceUp)
        {
            Debug.Log("Playing card face down.");
            //TODO: implement card face down.
            return;
        }


        //play card face up on the BG
        battleGround.PlayCardOnBattleGround(card, isPlayedFaceUp);

        //remove the card from the player's hand
        if (card.BelongToPlayer())
        {
            playerhand.RemoveCard(card);
        }


        //give Turn Manager notification, maybe the enemy need to react
        turnManager.CheckReactionTurn(friendlyTag);

        //logics for cards face up: dealing dmg, shield up...
        if (card is AttackCard)
        {
            AttackCard attackCard = (AttackCard)card;
            if(enemyCharacter != null)
            {
                attackCard.Play(enemyCharacter);
            }
            else
            {
                Debug.Log("Cannot access enemyCharacter in CardPlayer in " + gameObject.name);
            }    
            return;
        }

        if(card is DefenseCard)
        {
            DefenseCard defenseCard = (DefenseCard)card;
            if(friendlyCharacter != null)
            {
                defenseCard.Play(friendlyCharacter);
            }
            else
            {
                Debug.Log("Cannot access friendlyCharacter in CardPlayer in " + gameObject.name);
            }
            
            return;
        }



    }

    //set up the right owner.
    private void SetupCardOwner(Card card)
    {
        if(enemyTag == "EnemyCharacter")
        {
            card.SetOwner(belongToPlayer: true);
        }
        else
        {
            card.SetOwner(belongToPlayer: false);
        }
    }
}
