using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardTracking : MonoBehaviour
{
    CardAnimControllerScript playerDeckAnim;
    int curCardsPlayed = 0;

    private void Start()
    {
        playerDeckAnim = GameObject.Find("Player Card Pile").GetComponent<CardAnimControllerScript>();
        if (playerDeckAnim is null)
        {
            Debug.Log(name + "is null");
        }
    }

    public void IncreaseCardPlayCount()
    {
        if(curCardsPlayed > 4)
        {
            curCardsPlayed = 0;
        }

        playerDeckAnim.HideCard(curCardsPlayed);
        curCardsPlayed++;
    }

    public void DecreaseCardPlayCount()
    {
        if (curCardsPlayed > 4 || curCardsPlayed < 0)
        {
            curCardsPlayed = 0;
        }

        curCardsPlayed--;
        playerDeckAnim.ShowCard(curCardsPlayed);
    }

}
