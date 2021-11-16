using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


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

    private void Start()
    {
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
    }

    //while dragging, update position based on mouse position
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }


    //when done dragging, check if it's dropped on a card drop zone. If so, play the card
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDropped)
        {
            //Debug.Log(name + " is dropped");
            //play the card here
            playerCardPlayer.PlayCard(GetComponent<Card>());
            Destroy(this.gameObject);
        }
        else
        {
            //Debug.Log(name + " is not dropped");
            transform.position = originalPosition;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //eventData.pointerDrag.GetComponent<CardValue>().isDropped = false; default value is false
        //Debug.Log(name + " begins dragged");
    }
}
