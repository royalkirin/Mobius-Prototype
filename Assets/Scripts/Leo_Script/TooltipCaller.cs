using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipCaller : MonoBehaviour {


    Card card;

    int UILayer;

    [SerializeField] private bool showUI;

    private void Start() {
        showUI = true;
        UILayer = LayerMask.NameToLayer("Card");
    }

    private void Update() {
        if (showUI) {
            ShowToolTipIfItsACard(GetEventSystemRaycastResults());
        }
    }



    //Ray cast and show cards on the UI canvas
    private void ShowToolTipIfItsACard(List<RaycastResult> eventSystemRaysastResults) {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++) {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];

            if (curRaysastResult.gameObject.layer == UILayer) {

                card = curRaysastResult.gameObject.GetComponent<Card>();
                if (card != null) {
                    TooltipUI.Instance.Show(null, card, 0.1f);
                    return;
                } else {

                    //if its a card on the table, will show the sprite of the card
                    Image image = curRaysastResult.gameObject.GetComponent<Image>();
                    if (image != null) {
                        TooltipUI.Instance.Show(null, null, 0.1f,true, image);
                    }
                    
                }


            }
        }
    }


    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults() {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }



    public void StopShowingUI() {
        showUI = false;
    }

    public void ResumeShowingUI() {
        showUI = true;
    }

}
