using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardPlayer))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] CardPlayer cardPlayer = null;
    [SerializeField] AttackCard attackCard;
    [SerializeField] DefenseCard defenseCard;
    [SerializeField] SupportCard supportCard;
    TurnManager turnManager;

    CardChain cardChain;
    private void Start()
    {
        if(cardPlayer is null)
        {
            cardPlayer = GetComponent<CardPlayer>();
        }
        turnManager = FindObjectOfType<TurnManager>();

        cardChain = GameObject.FindWithTag("CardChain").GetComponent<CardChain>();
        if(cardChain is null)
        {
            Debug.Log("Cannot find cardChain in " + name);
        }

    }


    //when it's enemy turn, play.
    public IEnumerator OnEnemyTurn(float sec)
    {
        yield return new WaitForSeconds(sec);
        AttackCard newAttackCard = Instantiate(attackCard, new Vector3(0, 0, 0), Quaternion.identity);
        cardPlayer.PlayCard(newAttackCard);
        Debug.Log("Ending Enemy turn");
    }

    public void EndEnemyTurn()
    {
        StartCoroutine(WaitEnemyEndTurn(5));
    }

    IEnumerator WaitEnemyEndTurn(int secs)
    {
        yield return new WaitForSeconds(secs);
        turnManager.DefaultChangeTurn();
    }

    public void OnEnemyReactTurn()
    {
        Card lastCardPlayed = cardChain.GetLastCardPlayed();
        if(lastCardPlayed is null)
        {
            Debug.Log("last card played in chain is null");
        }

        StartCoroutine(EnemyPlayReactionCard(1f));

    }

    IEnumerator EnemyPlayReactionCard(float sec)
    {
        yield return new WaitForSeconds(sec);

        Card lastCardInChain = cardChain.GetLastCardPlayed();
        Card newCard;
        if (lastCardInChain is AttackCard)
        {
            newCard = Instantiate(defenseCard, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else if (lastCardInChain is DefenseCard)
        {
            newCard = Instantiate(supportCard, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else //last card played either NULL, or Support card
        {
            //try to play defense card, but will fail, and end the chain.
            newCard = Instantiate(attackCard, new Vector3(0, 0, 0), Quaternion.identity);
        }


        bool isPlayed = cardPlayer.PlayCard(newCard);
        if (!isPlayed)
        {
            Debug.Log("Destroy new card just created because fail to play");
            Destroy(newCard.gameObject);
            cardChain.ChainEnd(isPlayer: false);
        }
       
    }


}
