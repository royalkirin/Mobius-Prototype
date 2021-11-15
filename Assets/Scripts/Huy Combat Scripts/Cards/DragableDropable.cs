using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//handle cards that can be dragged and dropped

[RequireComponent(typeof(RectTransform))]
public class DragableDropable : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public bool isDropped = false;
    private Vector3 originalPosition;

    //for now, this do nothing. maybe in the future need reference to canvas to move withthin
    //Canvas canvas = null; 

    [SerializeField] RectTransform rectTransform;//move when dragging

    CardPlayer playerCardPlayer; //Card Player component of the Player

    private void Start()
    {
        if (rectTransform is null)
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
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

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


}
