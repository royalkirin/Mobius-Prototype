using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipCaller : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
    Camera cameraObject;


    private void Awake() {
        cameraObject = Camera.main;
    }


    /**
     * 
     * 
     *    we are not using this for now, but will be usefull to show the card stats on the screen
     *      just remember to change the raycas layer before use!
     * 
     * 
     * 
     * 
    void Update() {

        //to get the car more informations
        ray = cameraObject.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            TooltipInfo tooltipInfo = hit.collider.gameObject.GetComponent<TooltipInfo>(); ;
            if (tooltipInfo != null) {
                print("--------------- " + hit.collider.name);
            }
        }

    }

    **/

}
