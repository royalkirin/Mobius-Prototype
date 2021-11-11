using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class lightCycle : MonoBehaviour
{
    public bool active;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            transform.Rotate(Vector3.right, speed * Time.deltaTime);
        }
    }
}
