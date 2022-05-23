using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportCard : Card
{
    public int numbCardsToAdd = 1;
    // Update is called once per frame
    public override void Play(GameObject playerDeckGameObject)
    {
        AudioManager.instance.Play("SupportCard");
        base.Play(playerDeckGameObject);
        //TODO: Add code to remove debuffs once debuffs are implemented
        //Also add juicy stuff like animations and whatnot

        //add cards to player hand IF SUPPORT CARD BELONGS TO PLAYER
        if (belongToPlayer)
        {
            Debug.Log("Implement support card for player");
            SupportCardEffect(playerDeckGameObject);
        }
        else
        {
            Debug.Log("Implement support card for enemy");
            SupportCardEffect(playerDeckGameObject);
        }
        
    }


    //effect of support card FOR PLAYER ONLY
    public bool SupportCardEffect(GameObject playerDeckGameObject)
    {
        Deck playerDeck = playerDeckGameObject.GetComponent<Deck>();
        if(playerDeck is null)
        {
            Debug.Log("Invalid deck gameobject passed.");
            return false;
        }

        for(int i = 0; i < numbCardsToAdd; i++)
        {
            if (belongToPlayer)
            {
                playerDeck.DealToPlayer(cardLimited: false);//this is special: deal unlimited cards.
                CardPileDecrease();
            }
            else
            {
                EnemyCPD();
                playerDeck.DealToEnemy(cardLimited: false);
            }
            
        }
        return true;
    }

    public void SetNumbCardsToDraw(int drawThisMuch)
    {
        numbCardsToAdd = drawThisMuch;
        Debug.Log(numbCardsToAdd);
    }
}
