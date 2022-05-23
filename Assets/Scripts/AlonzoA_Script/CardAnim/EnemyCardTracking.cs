using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCardTracking : MonoBehaviour
{
    CardAnimControllerScript enemyDeckAnim;
    int curCardsPlayed = 0;
    private void Start()
    {
        enemyDeckAnim = GameObject.Find("Enemy Card Pile").GetComponent<CardAnimControllerScript>();
        if (enemyDeckAnim is null)
        {
            Debug.Log(name + "is null");
        }
    }

    public void IncreaseCardPlayCount()
    {
        if (curCardsPlayed > 4)
        {
            curCardsPlayed = 0;
        }

        enemyDeckAnim.HideCard(curCardsPlayed);
        curCardsPlayed++;
    }

    public void DecreaseCardPlayCount()
    {
        if (curCardsPlayed > 4 || curCardsPlayed < 0)
        {
            curCardsPlayed = 1;
        }

        curCardsPlayed--;
        enemyDeckAnim.ShowCard(curCardsPlayed);
    }
}
