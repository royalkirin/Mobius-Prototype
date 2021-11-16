using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
   public bool assigned, player;
    int slotvalue;
    CardValue cv;
    HealthScript hs;

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
            slotvalue = eventData.pointerDrag.GetComponent<CardValue>().value;
           
            if (!player)
            {
            assigned = true; 
            if (assigned)
            {
                eventData.pointerDrag.GetComponent<CardValue>().Rc();
                
            }

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
                            eventData.pointerDrag.GetComponent<CardValue>().isDropped = true;
                            // hs.health = hs.health - slotvalue;
                            assigned = false;
                        }
                        assigned = false;
                        break;
                    case SlotManager.Support:
                        // display.GetComponent<Text>().text = "Support";
                        if (eventData.pointerDrag.GetComponent<CardValue>().CurrentCard == CardValue.CardManager.Support)
                        {
                            eventData.pointerDrag.GetComponent<CardValue>().isDropped = true;
                            hs.health = hs.health + slotvalue;
                            assigned = false;
                        }
                        break;
                    default:
                        Debug.Log("Slot not set/ sequence is broke");
                        break;


                }
            }
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
    public void Roundchange()
    {

        if (player)
        {
          hs.health = hs.health - 10;
        }
       
        slotvalue = 0;
            
    }
}
