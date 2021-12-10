using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///*****************************************************************************************///
/// Class: DebugController                                                                  ///
///                                                                                         ///
/// Description: The Debug Controller is a script meant to enable a quick addition of       ///
///             controls overtime to help with the ease of testing features. It isn't       ///
///             required to put all debug functions here, but rather serve as an            ///
///             optional place to contain those features if needed for code cleanliness.    ///
///                                                                                         ///
///     Date Created: 12/10/21                                                              ///
///     Date Updated: 12/10/21                                                              ///
///                                                                                         ///
///     Author: Jordan R. Douglas                                                           ///
///*****************************************************************************************///
public class DebugController : MonoBehaviour
{
    #region VARIABLES
    //Variables
    
    //Unity Variables
    public GameObject uPlayer;
    public Vector3 uPlayerSpawn;
    public GameObject uLightManager; //Container for the Lighting in the Scene
    public Quaternion uLMStartRotation;
    #endregion

    //Dev Note: This script is not required for anything to work.
    //          Should it be used, it needs to be placed on the Player Character
    //          in the scene and pieces must be attached accordingly for each
    //          function to work as intended.

    // Start is called before the first frame update
    void Start()
    {
        uPlayerSpawn = uPlayer.transform.position;
        uLMStartRotation = uLightManager.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            QuickRespawn();
        }

        if (Input.GetKeyDown("l"))
        {
            ResetLighting();
        }
    }

    ///*********************************************************************///
    /// Function: QuickRespawn                                              ///
    ///                                                                     ///
    /// Description: Respawns the Player back to their intial spawn point   ///
    ///             on the map. Recommended use is only when traveling,     ///
    ///             not during combat.                                      ///
    ///                                                                     ///
    ///     Date Created: 12/10/21                                          ///
    ///     Date Updated: 12/10/21                                          ///
    ///                                                                     ///
    ///     Author: Jordan R. Douglas                                       ///
    ///*********************************************************************///
    void QuickRespawn()
    {
        uPlayer.transform.position = uPlayerSpawn;
    }

    ///*********************************************************************///
    /// Function: ResetLighting                                             ///
    ///                                                                     ///
    /// Description: Resets Light to its initial starting point on the map. ///
    ///                                                                     ///
    ///     Date Created: 12/10/21                                          ///
    ///     Date Updated: 12/10/21                                          ///
    ///                                                                     ///
    ///     Author: Jordan R. Douglas                                       ///
    ///*********************************************************************///
    void ResetLighting()
    {
        uLightManager.transform.rotation = uLMStartRotation;
    }
}
