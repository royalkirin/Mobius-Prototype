using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//this component allows each card to be discarded from player hand with right mouse button click.
//only discard when TurnManager.isDiscardModeON = true
public class CardDiscard : MonoBehaviour, IPointerClickHandler
{
    TurnManager turnManager;
    PlayerHand playerHand;
     


    private void Start()
    {
        FindVariables();
    }
    private void Update()
    {

    }

    private void FindVariables()
    {
        turnManager = FindObjectOfType<TurnManager>();
        if (turnManager is null)
        {
            Debug.Log("Cannot find Turn manager in " + name);
        }

        playerHand = GameObject.FindWithTag("PlayerHand").GetComponent<PlayerHand>();
        if (playerHand is null)
        {
            Debug.Log("Cannot find Player Hand in " + name);
        }
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right Mouse Button Clicked on: " + name);
            if (turnManager.GetDiscardMode())//if discard mode is ON
            {
                playerHand.Discard(GetComponent<Card>());
            }
        }
    }
}