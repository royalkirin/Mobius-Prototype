using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lure : MonoBehaviour
{
    Rigidbody rb;
    public Transform player, lur;
    GameObject playa;
    LineRenderer lineren;
    public float timer, time;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lineren = GetComponent<LineRenderer>();
        rb.AddForce(transform.forward * 10);
        playa = GameObject.Find("Player Capsule");
        player.position = playa.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;
        rb.AddForce(transform.forward * 1);
        if (timer >= time)
        {
            //player = playa.transform.position;
            //lur = lu.transform.position
            player.position = playa.transform.position;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            lineren.SetPosition(0, player.position);
            lineren.SetPosition(1, lur.position);
        }
        if (timer >= time / 2)
        {
            rb.useGravity = true;
        }
        if (timer >= time * 10)
        {
            Destroy(gameObject);
        }
    }
}
