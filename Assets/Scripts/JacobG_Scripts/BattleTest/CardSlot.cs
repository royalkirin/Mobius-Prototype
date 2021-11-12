using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
                                       //Drop handles when the mouse lets go
public class CardSlot : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
   public bool assigned, player;
    int slotvalue;
    CardValue cv;
    HealthScript hs;

    //Slot manager could probably be removed in the transition to a whole screen. This was to specify what cards worked in what slot.
   public enum SlotManager
    {
        Attack,
        Defend,
        Support,
        None
    }
   public SlotManager CurrentSlot = SlotManager.None;
    private void Start()
    {
        //gets the script component from canvas.
         cv = GameObject.Find("Canvas").GetComponent<CardValue>();
        hs = GetComponent<HealthScript>();
    }

    private void Update()
    {
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && assigned == false)
        {
            //eventData.pointerDrag.GetComponent<CardValue>().isDropped = true;
            //Sets the slotvalue to the value from the card.
            slotvalue = eventData.pointerDrag.GetComponent<CardValue>().value;
           //this checks if the slot is a player
            if (!player)
            {
                assigned = true; 
            if (assigned)
            {   //grabs the rc function from the card
                eventData.pointerDrag.GetComponent<CardValue>().Rc();
                
            }
                 //This is a more obvious display of switch being an fat If check.
                 //As said before, the switch and enum could be removed and redone.
                switch (CurrentSlot)
                {
                    case SlotManager.Attack:
                        //display.GetComponent<Text>().text = "Attack";
                        if (eventData.pointerDrag.GetComponent<CardValue>().CurrentCard == CardValue.CardManager.Attack)
                        {
                            eventData.pointerDrag.GetComponent<CardValue>().isDropped = true;
                            hs.health = hs.health - slotvalue;
                            assigned = false;
                        }

                        break;
                    case SlotManager.Defend:
                        //display.GetComponent<Text>().text = "Defend";
                        if (eventData.pointerDrag.GetComponent<CardValue>().CurrentCard == CardValue.CardManager.Defend)
                        {
                            eventData.pointerDrag.GetComponent<CardValue>().isDropped = false;
                            // hs.health = hs.health - slotvalue;
                            assigned = false;
                        }
                        assigned = false;
                        break;
                    case SlotManager.Support:
                        // display.GetComponent<Text>().text = "Support";
                        if (eventData.pointerDrag.GetComponent<CardValue>().CurrentCard == CardValue.CardManager.Support)
                        {
                            eventData.pointerDrag.GetComponent<CardValue>().isDropped = false;
                            hs.health = hs.health + slotvalue;
                            assigned = false;
                        }
                        break;
                    default:
                        Debug.Log("Slot not set/ sequence is broke");
                        break;


                }
            }
            //everything here applies only to the player
            else if (eventData.pointerDrag.GetComponent<CardValue>().CurrentCard != CardValue.CardManager.Attack)
            {
                assigned = true;
                if (assigned)
                {
                    eventData.pointerDrag.GetComponent<CardValue>().Rc();

                }
                if (eventData.pointerDrag.GetComponent<CardValue>().CurrentCard == CardValue.CardManager.Defend)
                        {
                    //assigned = true;
                            eventData.pointerDrag.GetComponent<CardValue>().isDropped = true;
                             hs.health = hs.health - slotvalue;
                    Debug.Log(slotvalue);
                            assigned = false;
                        }
                if (eventData.pointerDrag.GetComponent<CardValue>().CurrentCard == CardValue.CardManager.Support)
                {
                    //assigned = true;
                    eventData.pointerDrag.GetComponent<CardValue>().isDropped = true;
                    hs.health = hs.health + slotvalue;
                    assigned = false;
                }
                
            }
           
        }
    }
    //This covers the enemy attack, just sets the health lower.
    public void Roundchange()
    {

        if (player)
        {
          hs.health = hs.health - 10;
        }
       
        slotvalue = 0;
            
    }
}
