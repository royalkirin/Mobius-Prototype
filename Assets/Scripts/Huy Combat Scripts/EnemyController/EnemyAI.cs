using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardPlayer))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] CardPlayer cardPlayer = null;
    [SerializeField] AttackCard attackCard;
    TurnManager turnManager;
    private void Start()
    {
        if(cardPlayer is null)
        {
            cardPlayer = GetComponent<CardPlayer>();
        }

        turnManager = FindObjectOfType<TurnManager>();
    }


    //when it's enemy turn, play.
    public void OnEnemyTurn()
    {
        cardPlayer.PlayCard(attackCard);
        StartCoroutine(EnemyEndTurn(7));
    }

    IEnumerator EnemyEndTurn(int secs)
    {
        yield return new WaitForSeconds(secs);
        turnManager.ChangeTurn();
    }
}
