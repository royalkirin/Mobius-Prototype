using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit_Game()
    {
        Application.Quit();

        Debug.Log("If this was a build, the game would be closed. Due to laziness/time, I have not made it so that it stop play while in-engine.");
    }
}
