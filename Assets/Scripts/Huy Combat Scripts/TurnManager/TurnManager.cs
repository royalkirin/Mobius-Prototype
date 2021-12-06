using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this class managers the turn of the game, player or enemy, start or end of each one.
//Depends on which turn it is, certain features of the game will be deactivated/activated for players
//For example, when it's enemy turn, we deactivate the drop zone for the cards -> Player cannot play any cards.
public class TurnManager : MonoBehaviour
{
    public bool isPlayerTurn = true;


    GameObject[] PlayerActivateList;//all the things we activate when it's player turn, deactivate on Enemy Turn
    EnemyAI enemyAI;//enemy in the scene

    //when someone attacks another, the other can have a reaction turn to choose to activate his trap card
    public bool isPlayerReactTurn = false;
    public bool isEnemyReactTurn = false;

    Deck playerDeck;

    private void Start()
    {
        PlayerActivateList = GameObject.FindGameObjectsWithTag("PlayerTurn");
        enemyAI = GameObject.FindWithTag("Enemy").GetComponent<EnemyAI>();
        playerDeck = GameObject.FindWithTag("PlayerDeck").GetComponent<Deck>();
        ManageFeatures();
        PrintTurn();
    }
    
    //Only EnemyAI (Enemy side) calls this script.
    //manage basic logic for changing turn from both player and Enemy. Player have a separate function
    //he should call to change turn.
    public void DefaultChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        PrintTurn();
        if (!isPlayerTurn)//if it's enemy turn, we signify the enemy AI to play cards.
        {
            enemyAI.OnEnemyTurn();
        }

        ManageFeatures();

        //if player turn, deal the cards to player.
        if (isPlayerTurn)
        {
            playerDeck.FullDealToPlayer();
        }
    }

    //the player calls this script (by hitting EndTurn tn) to signify changing turn.
    public void PlayerChangeTurn()
    {
        if (!isPlayerTurn)//if player clicks EndTurn during Enemy Turn, we do nothing.
        {
            return;
        }
        //player clicks during his turn, we change turn
        DefaultChangeTurn();

    }


    //Decide which features to be enabled/disabled when swiching turns.
    private void ManageFeatures()
    {
        foreach (GameObject obj in PlayerActivateList)
        {
            obj.SetActive(isPlayerTurn);
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

    //When player A plays a card, gives player B a chance to react (activate trap card if possible?)
    public void CheckReactionTurn(string friendlyTag)
    {
        if(friendlyTag == "PlayerCharacter")
        {
            enemyAI.OnEnemyReactTurn();
        }
        else if(friendlyTag == "EnemyCharacter")
        {
            Debug.Log("Player reaction turn");
        }
    }
}
