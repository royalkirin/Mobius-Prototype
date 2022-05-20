using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardTracking : MonoBehaviour
{
    CardAnimControllerScript playerDeckAnim;
    int cardsPlayed = 0;
    private void Start()
    {
        playerDeckAnim = GameObject.Find("Player Card Pile").GetComponent<CardAnimControllerScript>();
        if (playerDeckAnim is null)
        {
            Debug.Log(name + "is null in script Ronin_Slash");
        }
    }

    private void Update()
    {
        if(cardsPlayed <= 0)
        {
            cardsPlayed = 0;
        }
    }

    public void IncreaseCardPlayCount()
    {
        playerDeckAnim.HideCard(cardsPlayed);
        cardsPlayed++;
    }

    public void DecreaseCardPlayCount()
    {
        cardsPlayed--;
        playerDeckAnim.HideCard(cardsPlayed);
    }

}
