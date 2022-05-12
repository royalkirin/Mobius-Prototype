using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///*****************************************************************************************///
/// Class: BattleCameraControl                                                              ///
///                                                                                         ///
/// Description: Controls the Battle Scene Camera. This primarily is used when the Card     ///
///             Chain ends and we need to show what effects are occuring from the winner.   ///
///             Can also be used to control the camera outside of Card Chain completion.    ///
///                                                                                         ///
///     Date Created: 4/07/22                                                               ///
///     Date Updated: 4/09/22                                                               ///
///                                                                                         ///
///     Author: Jordan R. Douglas                                                           ///
///*****************************************************************************************///
public class BattleCameraControl : MonoBehaviour
{
    #region VARIABLES
    //Variables
    bool bIsLerping = false;
    bool bIsResetting = false;
    float fCameraMovSpeed = 4.5f; //1.0 is Default Speed. Increase to move Camera Faster, Decrease to slow camera down.

    //UNITY
    [SerializeField] Transform uPlayer;
    [SerializeField] Transform uEnemy;

    //[SerializeField] List<Transform> uTarget = new List<Transform>();
    Transform uTarget;
    int test;

    [SerializeField] Quaternion uBattleCamOriginTransform;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        uPlayer = GameObject.FindGameObjectWithTag("PlayerCharacter").transform;
        uEnemy = GameObject.FindGameObjectWithTag("EnemyCharacter").transform;
        //Set Camera's Starting Transform to a Variable for later use.
        uBattleCamOriginTransform = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (uBattleCamOriginTransform.x == 0)
        {
            uBattleCamOriginTransform = this.transform.rotation;
        }

        if (bIsLerping || bIsResetting)
        {
            LERPCameraRotation();
            //StartCoroutine(LERPCameraRot());
        }
    }

    #region CameraControls
    //Use when moving the camera to look at the Enemy
    public void MoveCameratoEnemy()
    {
        //this.transform.LookAt(uEnemy.transform);
        //uTarget.Add(uEnemy.transform);
        //uTarget[test] = uPlayer.transform;
        /*uTarget = uEnemy.transform;
        test++;
        bIsLerping = true;*/
    }

    //Use when moving the camera to look at the Player
    public void MoveCameratoPlayer()
    {
        //uTarget.Add(uPlayer.transform);
        //uTarget[test] = uPlayer.transform;
        /*uTarget = uPlayer.transform;
        test++;
        bIsLerping = true;*/
    }

    //Reset the Camera's Position Back to the middle of the screen.
    public void MoveCameraBacktoNeutral()
    {
        //uTarget = uBattleCamOriginTransform;
        bIsResetting = true;
    }

    public void LERPCameraRotation()
    {
        if (bIsLerping)
        {
            //Quaternion uLookAt = Quaternion.LookRotation(uTarget[0].position - this.transform.position);
            Quaternion uLookAt = Quaternion.LookRotation(uTarget.position - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, uLookAt, Time.deltaTime * fCameraMovSpeed);
            if (this.transform.rotation == uLookAt)
            {
                bIsLerping = false;
            }
            //if (uTarget.Count == 1)
            //{
            //    bIsLerping = false;
            //}
        }
        else
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, uBattleCamOriginTransform, Time.deltaTime * fCameraMovSpeed);
            if (this.transform.rotation == uBattleCamOriginTransform)
            {
                bIsResetting = false;
            }
        }
    }

    /*IEnumerator LERPCameraRot()
    {
        if (bIsLerping)
        {
            Quaternion uLookAt = Quaternion.LookRotation(uTarget[0].position - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, uLookAt, Time.deltaTime * fCameraMovSpeed);
           //if (this.transform.rotation == uLookAt)
           //{
           //    uTarget.Remove(uTarget[0]);
           //}
           //if (uTarget.Count == 1)
           //{
           //    bIsLerping = false;
           //}
            yield return new WaitForSeconds(1.0f);
        }
        else
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, uBattleCamOriginTransform, Time.deltaTime * fCameraMovSpeed);
            if (this.transform.rotation == uBattleCamOriginTransform)
            {
                test = 0;
                bIsResetting = false;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }*/
    #endregion
}
