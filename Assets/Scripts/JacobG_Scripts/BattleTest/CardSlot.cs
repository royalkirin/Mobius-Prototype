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
    CardValue cv, dragdata;
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
            dragdata = eventData.pointerDrag.GetComponent<CardValue>();
            //this checks if the slot is a player
            if (!player)
            {
                assigned = true;
                if (assigned)
                {   //grabs the rc function from the card
                    dragdata.Rc();

                }
                //This is a more obvious display of switch being an fat If check.
                //As said before, the switch and enum could be removed and redone.
                switch (CurrentSlot)
                {
                    case SlotManager.Attack:
                        
                        if (dragdata.CurrentCard == CardValue.CardManager.Attack)
                        {
                            dragdata.isDropped = true;
                            if (dragdata.special)
                            {
                                dragdata.testat2();
                            }
                            else dragdata.testat();
                           
                            assigned = false;
                        }

                        break;
                    case SlotManager.Defend:
                        
                        if (dragdata.CurrentCard == CardValue.CardManager.Defend)
                        {
                            dragdata.isDropped = false;
                           
                            assigned = false;
                        }
                        assigned = false;
                        break;
                    case SlotManager.Support:
                       
                        if (dragdata.CurrentCard == CardValue.CardManager.Support)
                        {
                            dragdata.isDropped = false;
                           dragdata.testsp();
                            assigned = false;
                        }
                        break;
                    default:
                        Debug.Log("Slot not set/ sequence is broke");
                        break;


                }
            }
            //everything here applies only to the player
            else if (dragdata.CurrentCard != CardValue.CardManager.Attack)
            {
                assigned = true;
                if (assigned)
                {
                    dragdata.Rc();

                }
                if (dragdata.CurrentCard == CardValue.CardManager.Defend)
                {
                    //assigned = true;
                    dragdata.isDropped = true;
                    if (dragdata.special)
                    {
                        dragdata.testdf2();
                    }
                    else
                    {
                       dragdata.testdf();
                    }
                   

                    assigned = false;
                }
                if (dragdata.CurrentCard == CardValue.CardManager.Support)
                {
                    //assigned = true;
                    dragdata.isDropped = true;
                    dragdata.testsp();
                    assigned = false;
                }

            }

        }
    }
    //This covers the enemy attack, just sets the health lower.
    //public void Roundchange()
    //{

    //    if (player)
    //    {
    //        hs.health = hs.health - 10;
    //    }

    //    slotvalue = 0;

    //}
}

