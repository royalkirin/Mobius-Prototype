using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScript : MonoBehaviour
{
    public GameObject options, graphicstab, othertab;
    bool open;
    // Start is called before the first frame update
    public void ChangeGraphic(int num)
    {
        if(num == 0)
        {
            Screen.SetResolution(1920, 1080, false);
        }
        if (num == 1)
        {
            Screen.SetResolution(1600, 1280, false);
        }
        if (num == 2)
        {
            Screen.SetResolution(1280, 1024, false);
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !open)
        {
            options.SetActive(true);
            graphicstab.SetActive(false);
            othertab.SetActive(false);
            open = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && open)
        {
            open = false;
            options.SetActive(false);
        }
    }

}
