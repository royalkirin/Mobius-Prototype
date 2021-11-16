using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//This script belongs to the area that handle dropping cards
//(the colored panel in the middle of screen)
public class CardDropHandler : MonoBehaviour, IDropHandler
{
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

        //if we are dragging smt DragableDropable (in this case, a Card)
        if (draggedObject.TryGetComponent<DragableDropable>(out DragableDropable dragDrop))
        {
            dragDrop.isDropped = true;
        }

        
    }
}
