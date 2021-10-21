using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_PlayerMovement : MonoBehaviour
{
    public float mSpeed = 1; 

    public float tSpeed = 90; 

    public float lSpeed = 20; 

    public float gravity = 10; 
                                 
    private Vector3 sNormal; 

    private Vector3 myNormal;
    
    private bool jumping = false; 
                                  
    private Transform myTransform;

    public BoxCollider boxCollider;

    LayerMask Ground;


    private void Start()
    {
        myNormal = transform.up; 

        myTransform = transform;

        GetComponent<Rigidbody>().freezeRotation = true; 

    }

    private void FixedUpdate()
    {
        
        GetComponent<Rigidbody>().AddForce(-gravity * GetComponent<Rigidbody>().mass * myNormal);
    }

    private void Update()
    {
        Ray ray;

        RaycastHit hit;

        ray = new Ray(myTransform.position, -myNormal);

        if (Physics.Raycast(ray, out hit))
        {
           

            sNormal = hit.normal;
        }
        else
        {
            

            sNormal = Vector3.up;
        }

        myNormal = Vector3.Lerp(myNormal, sNormal, lSpeed * Time.deltaTime);

        Vector3 myForward = Vector3.Cross(myTransform.right, myNormal);

        Quaternion targetRot = Quaternion.LookRotation(myForward, myNormal);

        myTransform.rotation = Quaternion.Lerp(myTransform.rotation, targetRot, lSpeed * Time.deltaTime);

        myTransform.Translate(0, 0, Input.GetAxis("Vertical") * mSpeed * Time.deltaTime);

        myTransform.Rotate(0, Input.GetAxis("Horizontal") * tSpeed * Time.deltaTime, 0);
    }
}
