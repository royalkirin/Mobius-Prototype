using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

///*****************************************************************************************///
/// Class: MobiusTeleporter                                                                 ///
///                                                                                         ///
/// Description: This script exclusively serves to act as a transition between Player's     ///
///             Mobius and a rivaling Mobius. It will teleport the Player, activate a       ///
///             corresponding animation, and put them on the new Mobius. Afterwards, they   ///
///             can use this again to leave that Mobius and return to their original.       ///
///                                                                                         ///
///     Date Created: 12/17/21                                                              ///
///     Date Updated: 1/2/22                                                                ///
///                                                                                         ///
///     Author: Jordan R. Douglas                                                           ///
///*****************************************************************************************///
public class MobiusTeleporter : MonoBehaviour
{
    #region VARIABLES
    //Variables
    public bool bIsTelported = false;

    //Unity Variables
    public Object uLevelToLoad; //ALWAYS SET THIS TO WHEN SPAWNING A PORTAL INTO A SCENE!
    #endregion

    ///*********************************************************************///
    /// Function: TeleportFunctionality                                     ///
    ///                                                                     ///
    /// Description: This function teleports the Player while also playing  ///
    ///             the animation showcasing the transition between Mobius. ///
    ///                                                                     ///
    ///     Date Created: 12/19/21                                          ///
    ///     Date Updated: 1/2/22                                            ///
    ///                                                                     ///
    ///     Author: Jordan R. Douglas                                       ///
    ///*********************************************************************///
    void TeleportFunctionality(Collider uPlayer)
    {
        //Play the Animation Sequence


        //Load new scene
        Debug.Log("Loading: " + uLevelToLoad.name);
        SceneManager.LoadScene(uLevelToLoad.name);

        //Player properly spawns


        //Should our Player character change in any way, we need to transport them
        //to the scene as accurately as possible to keep gameplay consistent.
        //Note to Self: They need to have STATIC variables to help us determine these things. Make note of this in the next meeting
    }

    //Use this to determine if Player has entered Teleporter Range.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !bIsTelported)
        {
            bIsTelported = true;
            TeleportFunctionality(other);
        }
    }

    //Use this for when the Player leaves the Teleporter Range.
    private void OnTriggerExit(Collider other)
    {
        bIsTelported = false;
    }
}
