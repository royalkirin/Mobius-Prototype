using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCardTracking : MonoBehaviour
{
    CardAnimControllerScript enemyDeckAnim;
    int cardsPlayed = 0;
    private void Start()
    {
        enemyDeckAnim = GameObject.Find("Enemy Card Pile").GetComponent<CardAnimControllerScript>();
        if (enemyDeckAnim is null)
        {
            Debug.Log(name + "is null");
        }
    }

    private void Update()
    {
        if (cardsPlayed > 4)
        {
            cardsPlayed = 0;
        }
    }

    public void IncreaseCardPlayCount()
    {
        enemyDeckAnim.HideCard(cardsPlayed);
        cardsPlayed++;
    }

    public void DecreaseCardPlayCount()
    {
        cardsPlayed--;
        enemyDeckAnim.ShowCard(cardsPlayed);
    }
}
