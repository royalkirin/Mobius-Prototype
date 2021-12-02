using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Manages cards on the battleground when player/enemy play them.
public class BattleGround : MonoBehaviour //(BG)
{
    [SerializeField] Canvas BGCardPlayCanvas = null;

    //these vars are for cards face up
    //can make it an array but a List is easier to change in the future.
    [SerializeField] List<Image> playedCards;//initialzed in prefab with references. Length = 3.
    private const int maximumCardShowed = 3;//max number of face up card showed on BG
    private int faceUpcardsPlayed = 0; //current number of cards played face up

    //these vars are for cards face down
    [SerializeField] Image playerTrapCardImage = null;
    [SerializeField] Image enemyTrapCardImage = null;
    bool isPlayerTrapCardAcive = false;
    bool isEnemyTrapCardActive = false;

    private void Start()
    {
        FindVariables();
    }

    //set up things, check for nulls.
    private void FindVariables()
    {
        if (BGCardPlayCanvas is null)
        {
            Debug.Log("Need Canvas reference in " + name);
        }

        //deactivate all image slots on BG
        foreach(Image cardImage in playedCards)
        {
            cardImage.gameObject.SetActive(false);
        }
        //update counts
        if(faceUpcardsPlayed < 0) { faceUpcardsPlayed = 0; }
        if (faceUpcardsPlayed > maximumCardShowed) { faceUpcardsPlayed = maximumCardShowed; }


        if (playerTrapCardImage is null)
        {
            Debug.Log("Need Player trap card reference in " + name);
        }
        else
        {
            playerTrapCardImage.gameObject.SetActive(false);
        }

        if (enemyTrapCardImage is null)
        {
            Debug.Log("Need Enemy trap card reference in " + name);
        }
        else
        {
            enemyTrapCardImage.gameObject.SetActive(false);
        }
    }

    public Canvas GetCanvas()
    {
        return BGCardPlayCanvas;
    }

    //the CardPlayer will call this function to play a card on BG
    //This only make the card image shown on the board. May need to change in the future if need 
    //more functionalities
    //playing face up vs face down requires 2 different functions because they show the card on different
    //list, different area, cards have different functionalities after.
    public void PlayCardOnBattleGround(Card card, bool isPlayer = true, bool isPlayedFaceUp = true)
    {

        if (!isPlayedFaceUp)
        {
            PlayCardFaceDown(card, isPlayer);
            return;
        }

        PlayCardFaceUp(card);

    }

    //helper: play the card face up on the bg
    private void PlayCardFaceUp(Card card)
    {
        //if the list is full, we moved everything back 1 index and remove the first card.
        if(faceUpcardsPlayed == playedCards.Count)
        {
            //move the images up 1 index in the playedCards list
            for (int i = 0; i < faceUpcardsPlayed - 1; i++)
            {
                playedCards[i].sprite = playedCards[i + 1].sprite;
            }
        }

        //Update count
        faceUpcardsPlayed++;
        if (faceUpcardsPlayed > playedCards.Count)
        {
            faceUpcardsPlayed = playedCards.Count;
        }

        //last spot on the list is the image of the card that is just played
        playedCards[faceUpcardsPlayed - 1].sprite = card.GetFrontImage();

        //set active for the images
        for (int i = 0; i < faceUpcardsPlayed; i++)
        {
            playedCards[i].gameObject.SetActive(true);
        }
    }

    //play a trap card on bg. isPlayer = is this card players'? if not, the enemy plays this card.
    private void PlayCardFaceDown(Card card, bool isPlayer)
    {
        //TODO: implement this
        Debug.Log("Implement this");
    }

    public bool IsPlayerTrapCardActive()
    {
        return isPlayerTrapCardAcive;
    }

    public bool IsEnemyTrapCardActive()
    {
        return isEnemyTrapCardActive;
    }
}
