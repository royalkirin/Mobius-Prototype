using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//These two call upon the UI and events to grab prebuilt mechanisms in unity.
using UnityEngine.UI;
using UnityEngine.EventSystems;
                                         //Using these allow the mouse to drag and drop on specific events
                                         //Pointer down is click, begin drag is starting to move, drag is moving, end drag is letting mouse go.
public class CardValue : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;

    //grabs the position of a rectangle
    private RectTransform rectTransform;
    // starting position
     Vector3 startpos;
    //text, value, and bools
    public Text display;
    public int value;
    public bool isDropped;
    public bool candrag, special;
    Character ch;
    
    private void Awake()
    {
        //sets the rect to the component Rect
        rectTransform = GetComponent<RectTransform>();
        // Sets the starting position
        startpos = transform.position;
        ch = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        
    }
    private void Start()
    {
        //basically a double check to make sure the position is set
        startpos = transform.position;
    }

    //This manages the card types, basically this is a collection of states.
    public enum CardManager
    {
        Attack,
        Defend,
        Support,
        None
    }
    //this gives us a variable of what card is currently used, with the default of none
    //Mandatory to have for the next part, Switch.
    public CardManager CurrentCard = CardManager.None;
    
    private void Update()
    {
        //This is a condition check, it checks the variable currentcard to specific states and then preforms the action in that state.
        switch (CurrentCard)
        {
            //This means, if the cardmanager is attack, display = text.
            //It looks more intimidating than what it is.
           case CardManager.Attack:
               display.GetComponent<Text>().text = "Attack";
                //break is pretty much a } in this context. This is one giant If statement in parts.
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
    //This is where the card detects it is getting picked up and moved around.
    //read the beginning for descriptions on each specific one.
    //pointer event data is just the info from the events relating to the mouse pointer.
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        
            //Debug.Log("OnBeginDrag");
        //Sets the card bool isdropped to false so that the card slot can set to true.
        //this gives us the snap back to start position we see when it is not dropped on the slot.
            eventData.pointerDrag.GetComponent<CardValue>().isDropped = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
       
         //Debug.Log("OnDrag");
        //This is what controls the movement of the card
        //Taking the anchored position and combining it with the delta of eventdata
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
           // Debug.Log("OnEndDrag");
        //This is the check if the card got dropped into a slot
            if (!isDropped)
            {
                transform.position = startpos;
            }
        

    }

    public void OnPointerDown(PointerEventData eventData)
    {
      
        
           // Debug.Log("OnPointerDown");
        
    }

    //This sets the card to become invisible and untouchable after placement.
    public void Rc()
    {
        GetComponent<Image>().enabled = false;
        isDropped = false;
        display.enabled = false;
        transform.position = startpos;
    }
    //This reenables card visibility
    public void NextRound()
    {
        GetComponent<Image>().enabled = true;
        display.enabled = true;
        
    }
    public void testat()
    {
        ch.Attack();
    }
    public void testat2()
    {
        ch.Attack2();
    }
    public void testdf()
    { 
        ch.Defend();
    }
    public void testdf2()
    {
        ch.Defend2();
    }
    public void testsp()
    {
        ch.Support();
    }
}
