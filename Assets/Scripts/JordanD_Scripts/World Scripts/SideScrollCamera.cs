using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///*****************************************************************************************///
/// Class: SideScrollCamera                                                                 ///
///                                                                                         ///
/// Description: This is to keep the Camera in check and ensure it follows the Player       ///
///             properly and smoothly.                                                      ///
///                                                                                         ///
///     Date Created: 1/26/22                                                               ///
///     Date Updated: 2/02/22                                                               ///
///                                                                                         ///
///     Author: Jordan R. Douglas                                                           ///
///*****************************************************************************************///
public class SideScrollCamera : MonoBehaviour
{
    #region VARIABLES
    //Variables

    //Unity Variables
    public Transform uPlayerTransform;
    [SerializeField] Transform uCameraFollowPoint;
    [SerializeField] Quaternion PlayerRotate;
    Vector3 uCamDistance = new Vector3(2.0f, -2.0f, 2.0f);
    #endregion

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    ///*********************************************************************///
    /// Function: FollowPlayer                                              ///
    ///                                                                     ///
    /// Description: Keeps the camera looking at the Player as smoothly as  ///
    ///             possible no matter where they walk on the Mobius.       ///
    ///                                                                     ///
    ///     Date Created: 1/31/22                                           ///
    ///     Date Updated: 2/27/22                                           ///
    ///                                                                     ///
    ///     Author: Jordan R. Douglas                                       ///
    ///*********************************************************************///
    void FollowPlayer()
    {
        //Vector3 newthing = new Vector3(Mathf.Cos(uPlayerTransform.position.x) + uCamDistance.x, uCamDistance.y, Mathf.Sin(uPlayerTransform.position.z) + uCamDistance.z);
        //Vector3 newthing = new Vector3(Mathf.Cos(uPlayerTransform.position.x) + uCamDistance.x, uCamDistance.y, uCamDistance.z);

        //transform.position = uPlayerTransform.position + newthing;
        //transform.position = uPlayerTransform.position * -1.0f;

        /*transform.position = new Vector3(uPlayerTransform.position.x - uCamDistance.x, uCamDistance.y, uPlayerTransform.position.z - uCamDistance.z);
        transform.position = uPlayerTransform.position - uCamDistance;*/

        //Keeps eyes on Players
        //tempContainer = uPlayerTransform.transform.position + Quaternion.AngleAxis(Camera.main.transform.eulerAngles.x, Vector3.up) * Vector3.forward * -5.0f;  
        //transform.position = tempContainer;

        transform.position = uPlayerTransform.position + uCamDistance;
        Vector3 uCross = Vector3.Cross((uPlayerTransform.position - transform.position), (uCamDistance - transform.position));
        Quaternion uCamTilt = Quaternion.FromToRotation(transform.up, uCross);
        transform.rotation = uCamTilt * transform.rotation;
        //transform.LookAt(uPlayerTransform.position);
    }
}
