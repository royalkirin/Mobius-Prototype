using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

///*****************************************************************************************///
/// Class: EnemyController                                                                  ///
///                                                                                         ///
/// Description: This script controls how the enemy acts in the overworld once spawned in.  ///
///             It should determine how they spot the Player, how they move, any animations ///
///             that should be displayed, and so forth. Whatever is needed to flesh out the ///
///             Enemy Object should be added/work alongside this script.                    ///
///                                                                                         ///
///     Date Created: 1/2/22                                                                ///
///     Date Updated: 1/12/22                                                               ///
///                                                                                         ///
///     Author: Jordan R. Douglas                                                           ///
///*****************************************************************************************///
public class EnemyController : MonoBehaviour
{
    #region VARIABLES
    //Variables
    public float fDetectionRange = 0.3f;
    public float fMovementSpeed = 1.0f;
    bool bChasingPlayer = false;
    bool bCurrentlyPatrolling = true;

    public float ShowDistance; //Debug Variable. Shows the Distance between Player and Enemy

    //Unity Variables
    public Vector3 uStartingPosition;
    public Vector3[] uPatrolPoints;
    public Object uBattleSceneToLoad;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        uStartingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //If the Player isn't being chased anymore and they aren't at their point of spawn...
        if (!bChasingPlayer && !bCurrentlyPatrolling && transform.position != uStartingPosition)
        {
            //Send them back to said spawn point to "reset" their lookout point!
            transform.position = Vector3.MoveTowards(transform.position, uStartingPosition, fMovementSpeed * Time.deltaTime);
        }
        else if (transform.position == uStartingPosition)
        {
            bCurrentlyPatrolling = true;
        }

        //Note: Although it seems mandatory for the enemy to patrol, that is decided by their Patrol Points.
        //If they have no Patrol Points, they can be considered stationary.
        if (bCurrentlyPatrolling)
        {
            EnemyPatrol();
        }
    }

    ///*********************************************************************///
    /// Function: EnemyChase                                                ///
    ///                                                                     ///
    /// Description: Activated when the Player enters the sight radius of   ///
    ///             an Enemy Object. Allows the Enemy to begin running      ///
    ///             towards the Player.                                     ///
    ///                                                                     ///
    ///     Date Created: 1/2/21                                            ///
    ///     Date Updated: 1/2/22                                            ///
    ///                                                                     ///
    ///     Author: Jordan R. Douglas                                       ///
    ///*********************************************************************///
    void EnemyChase(Collider uPlayer)
    {
        ShowDistance = Vector3.Distance(transform.position, uPlayer.transform.position);
        if (Vector3.Distance(transform.position, uPlayer.transform.position) <= fDetectionRange)
        {
            //Play Animation here

            //Send them to the battle scene
            Debug.Log("Loading: " + uBattleSceneToLoad.name);
            SceneManager.LoadScene(uBattleSceneToLoad.name);
        }
        else
        {
            //Play Running/Chasing Animation

            //Move the Enemy towards the Player
            transform.position = Vector3.MoveTowards(transform.position, uPlayer.transform.position, fMovementSpeed * Time.deltaTime);
        }
    }

    ///*********************************************************************///
    /// Function: EnemyPatrol                                               ///
    ///                                                                     ///
    /// Description: Gives the AI the ability to patrol when not chasing,   ///
    ///             allowing them to move about their own volition.         ///
    ///                                                                     ///
    ///     Date Created: 1/2/21                                            ///
    ///     Date Updated: 1/2/22                                            ///
    ///                                                                     ///
    ///     Author: Jordan R. Douglas                                       ///
    ///*********************************************************************///
    void EnemyPatrol()
    {
        //Currently empty. The functionality to be added here is as the description of the function says.
        //There can also be a free wander option here to allow the Enemy to move about randomly, but for now this is here for future functionality.
    }

    private void OnTriggerExit(Collider other)
    {
        //Check to see if it was the Player that left their sight.
        if (bChasingPlayer && other.gameObject.tag == "Player")
        {
            //If it was the Player, stop Chasing and allow return to origin spawn.
            Debug.Log("No more chasing");
            bChasingPlayer = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Check to see if it was the Player that entered their radius
        if (other.gameObject.tag == "Player")
        {
            //If it was the Player, begin chasing them
            bChasingPlayer = true;
            bCurrentlyPatrolling = false;
            EnemyChase(other);
        }
    }
}
