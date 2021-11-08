using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardValue : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;


    private RectTransform rectTransform;
    public Vector3 startpos;
    public Text display;
    public int value;
    public bool isDropped;
    public bool candrag;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startpos = transform.position;
        
    }
    private void Start()
    {
        startpos = transform.position;
    }


    public enum CardManager
    {
        Attack,
        Defend,
        Support,
        None
    }
    public CardManager CurrentCard = CardManager.None;
    
    private void Update()
    {

        switch (CurrentCard)
        {
           case CardManager.Attack:
               display.GetComponent<Text>().text = "Attack";
               break;
           case CardManager.Defend:
               display.GetComponent<Text>().text = "Defend";
               break;
           case CardManager.Support:
               display.GetComponent<Text>().text = "Support";
               break;
           default:
                Debug.Log("Card not set/ sequence is broke");
               break;

        }
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        
            Debug.Log("OnBeginDrag");
            eventData.pointerDrag.GetComponent<CardValue>().isDropped = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
       
         Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
            Debug.Log("OnEndDrag");

            if (!isDropped)
            {
                transform.position = startpos;
            }
        

    }

    public void OnPointerDown(PointerEventData eventData)
    {
      
        
            Debug.Log("OnPointerDown");
        
    }

    public void Rc()
    {
        GetComponent<Image>().enabled = false;
        isDropped = false;
        display.enabled = false;
        transform.position = startpos;
    }
    public void NextRound()
    {
        GetComponent<Image>().enabled = true;
        display.enabled = true;
        
    }
}
