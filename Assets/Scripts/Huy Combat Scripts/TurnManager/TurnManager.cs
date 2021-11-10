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
    GameObject enemyGameObject;

    private void Start()
    {
        PlayerActivateList = GameObject.FindGameObjectsWithTag("PlayerTurn");
        enemyGameObject = GameObject.FindWithTag("Enemy");
        ManageFeatures();
        PrintTurn();
    }
    public void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        PrintTurn();
        if (!isPlayerTurn)
        {
            enemyGameObject.GetComponent<EnemyAI>().OnEnemyTurn();
        }

        ManageFeatures();

    }

    //if it's player turn, all obj activated
    //else, all obj deactivated
    private void ManageFeatures()
    {
        foreach (GameObject obj in PlayerActivateList)
        {
            obj.SetActive(isPlayerTurn);
        }
    }

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
}
