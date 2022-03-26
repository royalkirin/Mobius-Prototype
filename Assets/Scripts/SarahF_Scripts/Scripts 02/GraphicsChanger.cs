using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsChanger : MonoBehaviour
{

    [SerializeField] int selectedScreenRes;

    [SerializeField] int[] screenResX;
    [SerializeField] int[] screenResY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Screen.SetResolution(screenResX[selectedScreenRes], screenResY[selectedScreenRes], false);

        gameObject.GetComponentInChildren<Text>().text = screenResX[selectedScreenRes]+" x "+screenResY[selectedScreenRes];
    }


    public void ChangeScreenRes()
    {   
    selectedScreenRes++;
        if (selectedScreenRes >= screenResY.Length)
        {
            selectedScreenRes = 0;
        }
        else
        {
            
        }
    }
}
