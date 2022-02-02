using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipCaller : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
    Camera cameraObject;

    [SerializeField] bool liga;


    private void Awake() {
        cameraObject = Camera.main;
    }

    void Update() {
        if (liga) {

            ray = cameraObject.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                TooltipInfo tooltipInfo = hit.collider.gameObject.GetComponent<TooltipInfo>(); ;
                if (tooltipInfo != null) {
                    print("--------------- " + hit.collider.name);
                }

            }
        }
    }
}
