using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    bool assigned;
    int slotvalue;
    CardValue cv;

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
    }
    
    private void Update()
    {
        //switch (CurrentSlot)
        //{
        //    case CurrentSlot.Attack:
        //        display.GetComponent<Text>().text = "Attack";
        //        break;
        //    case CurrentSlot.Defend:
        //        display.GetComponent<Text>().text = "Defend";
        //        break;
        //    case CurrentSlot.Support:
        //        display.GetComponent<Text>().text = "Support";
        //        break;
        //    default:
        //        Debug.Log("Card not set/ sequence is broke");
        //        break;

        //}
        //if (assigned)
        //{
        //    assigned = false;
        //    Invoke("Roundchange", .001f);
        //}
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && assigned == false)
        {
            eventData.pointerDrag.GetComponent<CardValue>().isDropped = true;
            assigned = true;
            if (assigned)
            {
                eventData.pointerDrag.GetComponent<CardValue>().Rc();
                
            }
        }
    }
    public void Roundchange(PointerEventData eventData)
    {
        
        
            eventData.pointerDrag.GetComponent<Renderer>().enabled = false;
            eventData.pointerDrag.GetComponent<CardValue>().isDropped = false;
            eventData.pointerDrag.transform.position = eventData.pointerDrag.GetComponent<CardValue>().startpos;
            //gameObject.GetComponent<Renderer>().enabled = false;
            assigned = false;
        //CancelInvoke("Roundchange");
    }
}
