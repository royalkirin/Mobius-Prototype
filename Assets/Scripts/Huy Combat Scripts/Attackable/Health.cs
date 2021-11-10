using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for now it's only a float value
//in the future, we can add health bars, different animations, effects... here
public class Health : MonoBehaviour
{
    public float health = 100f;


    //should only be called from Attackable script
    public void TakeDamage(float damage)
    {
        health -= damage;
        //Debug.Log(name + " took " + damage + " damage");
        if(health <= 0)
        {
            Debug.Log(name + " health is " + health + ", destroyed");
            Destroy(gameObject);
        }
    }
}
