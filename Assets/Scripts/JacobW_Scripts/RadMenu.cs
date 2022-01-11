using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RadMenu : MonoBehaviour
{
    Transform MenuRoot;


    // Start is called before the first frame update
    void Start()
    {
        MenuRoot = this.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            MenuRoot.gameObject.SetActive(true);
            //do some quik maths
            MenuRoot.position = RectTransformUtility.PixelAdjustPoint((Vector2)Input.mousePosition, MenuRoot, this.GetComponent<Canvas>());
        }
    }
}
