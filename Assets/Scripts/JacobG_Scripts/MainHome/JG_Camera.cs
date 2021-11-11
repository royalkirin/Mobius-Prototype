using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_Camera : MonoBehaviour
{
    private Vector3 camRot;
    private Transform cam;

   
    public float minxAngle = -4;
    
    public float maxxAngle = 16;

    public float minyAngle = -11;
    
    public float maxyAngle = 14;

    public float mov = 500;
    
    private void Start()
    {
        cam = UnityEngine.Camera.main.transform;
    }

   private void Update()
    {
        transform.Rotate(Vector3.up * mov * Time.deltaTime * Input.GetAxis("Mouse X"));

        camRot.x -= Input.GetAxis("Mouse Y") * mov * Time.deltaTime;

        camRot.x = Mathf.Clamp(camRot.x, minxAngle, maxxAngle);

        camRot.y += Input.GetAxis("Mouse X") * mov * Time.deltaTime;

        camRot.y = Mathf.Clamp(camRot.y, minyAngle, maxyAngle);

        cam.localEulerAngles = camRot;

    }

   
}
