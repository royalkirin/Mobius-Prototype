using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

///////////////////////////////////////////////////////////////////////////
/// Class: LossCondition                                                ///
/// Description: Handles the lose condition for matches, ensuring the   ///
///         Player is sent to the Game Over Screen Accordingly. It also ///
///         handles all logic related to the Game Over Scene.           ///
///                                                                     ///
/// Date Created: 05/18/2022                                            ///
/// Last Updated: 05/18/2022                                            ///
///                                                                     ///
/// Author: Jordan R. Douglas                                           ///
///////////////////////////////////////////////////////////////////////////
public class LossCondition : MonoBehaviour
{
    #region VARIABLES
    //Variables
    float fTimer = 3.0f;
    //Unity Variables
    #endregion

    void Start()
    {
        //GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<LossCondition>().DetermineLoss();

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            ReturntoMainMenu();
        }
    }

    public void DetermineLoss(float fPlayerHealth)
    {
        if (fPlayerHealth <= 0)
        {
            TransitionScreen(4);
        }
    }

    IEnumerator TransitionScreen(int nBuildScene)
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(nBuildScene);
    }

    void ReturntoMainMenu()
    {
        if (fTimer <= 0)
        {
            TransitionScreen(0);
        }

        fTimer -= Time.deltaTime;
    }
}
