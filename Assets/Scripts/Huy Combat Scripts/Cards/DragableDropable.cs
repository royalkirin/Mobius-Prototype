using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//handle cards that can be dragged and dropped
//Using these allow the mouse to drag and drop on specific events
//begin drag is starting to move, drag is moving, end drag is letting mouse go.
//Remember that Cards belong to Enemy cannot be dragg/dropped. So, don't use this script in Enemy cards.
[RequireComponent(typeof(RectTransform))]
public class DragableDropable : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public bool isDropped = false; //is the card dropped? if so, it should be played and nulled.
    private Vector3 originalPosition;

    //for now, this do nothing. maybe in the future need reference to canvas to move withthin
    //Canvas canvas = null; 

    [SerializeField] RectTransform rectTransform;//move when dragging

    //Card Player component of the Player
    //only Cards that belong to players can be dragged and dropped.
    //we only use this reference during drag/drop events.
    CardPlayer playerCardPlayer;
    TutorialCardPlayer playerCardPlayer1;

    //these variables are for playing face down or face up.
    //dragging with left mouse is face up, right mouse is face down
    bool isFaceUp = true;
    bool tutorial;

    //Player Deck Animations
    PlayerCardTracking playerCardTracking;

    private void Start()
    {
        Scene currscene = SceneManager.GetActiveScene();
        //string activescene = currscene.name;
        if(currscene.name == "Tutorial")
        {
            tutorial = true;
           
        }
        if (rectTransform is null) //getting rectTransform
        {
            rectTransform = GetComponent<RectTransform>();
        }
        originalPosition = transform.position;

        playerCardPlayer = GameObject.FindWithTag("Player").GetComponent<CardPlayer>();
        
        if(playerCardPlayer is null)
        {
            Debug.Log("Smt is wrong in " + name + " dragabledropale");
        }
        if (tutorial)
        {
            playerCardPlayer1 = GameObject.FindWithTag("Player").GetComponent<TutorialCardPlayer>();
            Debug.Log("in Tutorial");
        }

        playerCardTracking = GameObject.Find("Player Card Pile").GetComponent<PlayerCardTracking>();
    }

    //while dragging, update position based on mouse position
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position += (Vector3)eventData.delta;

    }


    //when done dragging, check if it's dropped on a card drop zone. If so, play the card
    public void OnEndDrag(PointerEventData eventData)
    {
        TooltipUI.Instance.ResumeShowingUI();
        if (isDropped)
        {
            //if the game accepts the card, we play it
            //pass the info of trying to play it face down or up.
            if (!tutorial)
            {
                bool isPlayedSucessfully = playerCardPlayer.PlayCard(GetComponent<Card>(), isFaceUp);
            if (isPlayedSucessfully)
            {
                    playerCardTracking.IncreaseCardPlayCount();
                    return;
            }
            else //if not, we move it back
            {
                isDropped = false;
                transform.position = originalPosition;
            }
            }
            
            if (tutorial)
            {
                bool isPlayedSucessfull = playerCardPlayer1.PlayCard(GetComponent<Card>(), isFaceUp);
                if (isPlayedSucessfull)
                {
                    return;
                }
                else //if not, we move it back
                {
                    isDropped = false;
                    transform.position = originalPosition;
                }
            }
        }
        else
        {
            Debug.Log(name + " is not dropped, return to original position");
            transform.position = originalPosition;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        TooltipUI.Instance.StopShowingUI();
        //eventData.pointerDrag.GetComponent<CardValue>().isDropped = false; default value is false
        //Debug.Log(name + " begins dragged");
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log("Dragging with Left mouse, playing face up");
            isFaceUp = true;
        }else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Debug.Log("Dragging with Right mouse, playing face down");
            isFaceUp = false;
        }
    }

    //the game moves the cards in different position in player hand
    //so we need to reset the original position.
    public void ResetOriginalPosition()
    {
        originalPosition = transform.position;
    }
}
