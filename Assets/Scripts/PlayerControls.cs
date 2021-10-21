using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    CharacterController c;

    public float Gravity;

    // Start is called before the first frame update
    void Start()
    {
        c = GetComponent<CharacterController>();
        c.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        c.Move(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime, -Gravity * Time.deltaTime, Input.GetAxis("Vertical") * Time.deltaTime));
    }
}
