using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    CharacterController c;

    public float Gravity;

    GameObject closest;

    // Start is called before the first frame update
    void Start()
    {
        c = GetComponent<CharacterController>();
        //c.enabled = true;

        closest = FindStartingSegment();

        
    }

    // Update is called once per frame
    void Update()
    {      
        c.Move(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime, -Gravity * Time.deltaTime, Input.GetAxis("Vertical") * Time.deltaTime));
    }


    private GameObject FindStartingSegment()
    {
        GameObject p = GameObject.Find("Mobius_c1");
        GameObject n = p.transform.GetChild(0).gameObject;

        for(int i = 1; i < p.transform.GetChildCount(); i++)
        {
            if(Vector3.Distance(p.transform.GetChild(1).position, c.transform.position) < Vector3.Distance(n.transform.position, c.transform.position))
            {
                n = p.transform.GetChild(i).gameObject;
            }
        }

        return n;
        
    }
}
