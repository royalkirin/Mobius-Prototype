using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



//This script is attached to the images on the BattleGround
//allow the player to Hover over the cards to see trap card information (implement later)
//allow the player to "try" to play the trap card when right click (IPointerClickHandler)
public class TrapCardMouseInteraction : MonoBehaviour {

    [SerializeField]
    bool isShowingTrapInfo = false; //switch to turn On/Off showing trap card info on the side 

    TrapCardManager trapCardManager;

    //Gets the image of the players trap card holder. 
    [SerializeField]  Image _trapcardimage;

    Card card; //card component of the trap card, should be assigned by TrapCardmanager when trapcard is played

    private void Start() {
        trapCardManager = GameObject.FindWithTag("TrapCardManager").GetComponent<TrapCardManager>();
        if (trapCardManager is null) {
            Debug.LogWarning("Cannot find TrapCard Manager in " + name);
        }

        //Looks for the player trap card holder, if it doesn't find the trap card holder it will return the debug.log. 
        //the script TrapCardManager.cs turn off the image making this like break. _trapcardimage = GameObject.FindWithTag("PlayerTrapCard").GetComponent<Image>();
        if (_trapcardimage is null)
        {
            Debug.Log("Player trap card holder not found");
        }
    }
   

    private void OnMouseOver() {
        //TODO: Show the card front on the side of the screen
        //can use OnMouseOver and OnMouseExit and a switch to turn the side info On/Off for better execution
        //turn on the switch here
        isShowingTrapInfo = true;

        TooltipUI.Instance.Show(null, card, 0.1f, false);
        //Try to activate the trap card when player right click it
        if (Input.GetMouseButtonDown(1)) {
            Debug.Log("Right click " + name);
            //try to activate it\
            if (card is null) {
                Debug.LogWarning("Card is null. It should be set by Trap Card Manager!");
                return;
            } else {
                trapCardManager.TryActivateTrapCard(card.belongToPlayer);
            }

        }
    }

    private void OnMouseExit() {
        //Debug.Log("Mouse EXIT "+ name);
        //TODO: Turn off the switch here
        isShowingTrapInfo = false;
    }

    public void SetCard(Card card) {
        this.card = card;

        _trapcardimage.sprite = card.GettrapImage();
        Debug.Log("Card is set in Trap Card Image");
        //if(card != null)
        //{ 
        //if (card.GetComponent<TrapCard>().counterAttackCard)
        //{
        //    Debug.Log("Counters Attack");
        //}
        //if (card.GetComponent<TrapCard>().counterDefenseCard)
        //{
        //    Debug.Log("Counters Defense");
        //}
        //if (card.GetComponent<TrapCard>().counterSupportCard)
        //{
        //    Debug.Log("Counters Support");
        //}

       // }
       
    }


}
