using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//for now it's only a float value
//in the future, we can add health bars, different animations, effects... here
public class Health : MonoBehaviour
{
    [SerializeField] float health = 10f; //current Health
    private float maxHealth = 10f; //max Health
    public Slider healthBar; //display health UI

    [SerializeField] private GameObject Shield; //if gameobject active, the shielsd will be destroied and the player will not receive damage


    private void Start()
    {
        SetupVariables();
    }

    //check if everything is setup correctly
    private void SetupVariables()
    {
        if(health > maxHealth)
        {
            Debug.Log("Current health is greater than maximum health in " + name);
            health = maxHealth;
        }
        if(healthBar is null)
        {
            Debug.Log("Missing health bar in " + name);
        }
        else
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = health;
        }

    }

    //should only be called from Attackable script
    //because there are a bunch of checks there before we directly manipulate health of a character
    public void TakeDamage(float damage)
    {
        //if the shield is active, the player will not receive damage
        if (Shield.activeInHierarchy) {
            Debug.Log("Is pending put shield breacking animation");
            return;
        }

        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);//make sure health is in range
        //Debug.Log(name + " took " + damage + " damage");
        UpdateHealthBar();

        if(health <= 0)
        {
            Debug.Log(name + " health is " + health + ", destroyed");
            Destroy(gameObject);
        }
    }

    //may extend this in the future
    private void UpdateHealthBar()
    {
        healthBar.value = health;
    }

    public float GetCurrentHealth()
    {
        return health;
    }

    public void RaiseTheShield() {
        Shield.SetActive(true);
    }




}
