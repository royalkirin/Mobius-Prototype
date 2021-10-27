using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class lightCycle : MonoBehaviour
{
    public bool active;

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            transform.Rotate(Vector3.forward, Time.deltaTime);
        }
    }
}
