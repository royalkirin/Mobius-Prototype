using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Manages cards on the battleground when player/enemy play them.
public class BattleGround : MonoBehaviour //(BG)
{
    [SerializeField] Canvas BGCardPlayCanvas = null;

    //can make it an array but a List is easier to change in the future.
    [SerializeField] List<Image> playedCards;//initialzed in prefab with references. Length = 3.
    private const int maximumCardShowed = 3;//max number of face up card showed on BG
    private int faceUpcardsPlayed = 0; //current number of cards played face up


    private void Start()
    {
        FindVariables();
    }

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
    public void PlayCardOnBattleGround(Card card, bool isPlayedFaceUp = true)
    {

        if (!isPlayedFaceUp)
        {
            //TODO:implement playing face down here
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
}
