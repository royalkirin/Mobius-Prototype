using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//This script belongs to the area that handle dropping cards
//(the colored panel in the middle of screen)
public class CardDropHandler : MonoBehaviour, IDropHandler
{
    [SerializeField] TextUpdates _TextUpdates;
    bool _ddOff = false;

    private void Start()
    {
        _TextUpdates = FindObjectOfType<TextUpdates>();
    }
    //this gets called whenever player drop smt to the area (releases mouse button)
    public void OnDrop(PointerEventData eventData)
    {
        
        GameObject draggedObject = eventData.pointerDrag;//the items being dragged/dropped
        //Debug.Log("On drop " + draggedObject.name);
        if(draggedObject is null)
        {
            Debug.Log("No object is being dragged");
            return;
        }

        //if we are dragging smt DragableDropable (in this case, only a Card has this component)
        if (draggedObject.TryGetComponent<DragableDropable>(out DragableDropable dragDrop))
        {
            dragDrop.isDropped = true;
            if(_TextUpdates != null) {
                _TextUpdates.CardPlayed();
            }
            
        }

        
    }
    public void DDOn()
    {
        _ddOff = true;
    }

    public void DDOff()
    {
        _ddOff = false;
    }
}
