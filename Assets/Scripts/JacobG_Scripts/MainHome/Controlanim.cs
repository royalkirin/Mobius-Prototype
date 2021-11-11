using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlanim : MonoBehaviour
{
    Animator anim;
    bool front;
   public Camera cam, cam2;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !front)
        {
     
            anim.Play("Mobius_swap_temp1");
            cam.gameObject.SetActive(false);
            cam2.gameObject.SetActive(true);
            front = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && front)
        {
           
            anim.Play("Mobius_swap_temp"); 
            front = false;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            cam.gameObject.SetActive(true);
            cam2.gameObject.SetActive(false);
        }
        
    }
}
