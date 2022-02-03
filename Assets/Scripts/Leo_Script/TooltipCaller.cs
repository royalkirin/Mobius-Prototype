using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TooltipCaller : MonoBehaviour {


    Card card;

    int UILayer;

    [SerializeField] private bool showUI;

    private void Start() {
        showUI = true;
        UILayer = LayerMask.NameToLayer("UI");
    }

    private void Update() {
        if (showUI) {
            ShowToolTipIfItsACard(GetEventSystemRaycastResults());
        }
    }



    private void ShowToolTipIfItsACard(List<RaycastResult> eventSystemRaysastResults) {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++) {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];

            if (curRaysastResult.gameObject.layer == UILayer) {

                card = curRaysastResult.gameObject.GetComponent<Card>();
                if (card != null) {
                    TooltipUI.Instance.Show(null, card, 0.1f);
                    return;
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
