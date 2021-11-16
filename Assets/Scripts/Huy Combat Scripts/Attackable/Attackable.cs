using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Belongs to any character that can be attacked
//will control Health component
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterAttackableUI))]
public class Attackable : MonoBehaviour
{
    public float DefenseValue = 0f;
    [SerializeField]Health health = null;
    [SerializeField] CharacterAttackableUI ui;
    private void Start()
    {
        if(health is null)
        {
            Debug.Log("Missing Health in " + name);
        }
        if(ui is null)
        {
            ui = GetComponent<CharacterAttackableUI>();
        }
        ui.UpdateText(DefenseValue, health.GetCurrentHealth());
    }

    //Call this function to deal damage to an object.
    public void TakeDamage(float damage)
    {
        damage = TakeDamageWithDefense(damage);
        if(damage > 0)
        {
            health.TakeDamage(damage);
        }
        ui.UpdateText(DefenseValue, health.GetCurrentHealth()) ;
    }

    //first we reduce the defense 
    //return the value of damage left after defense is reduced
    private float TakeDamageWithDefense(float damage)
    {
        //if defense is negative, we add to the damage
        if(DefenseValue  <= 0f)
        {
            damage += DefenseValue;
            return damage;
        }
        else//defense is positive
        {
            if(DefenseValue > damage)//and greater than damage
            {
                DefenseValue -= damage;
                //Debug.Log(name + " now has " + DefenseValue + " defense");
                return 0; //damage is fully negated
            }
            else //damage not fully negated
            {
                damage -= DefenseValue;
                DefenseValue = 0;
                return damage;
            }
        }
    }


    public void AddDefense(float defenseValue)
    {
        DefenseValue += defenseValue;
        //Debug.Log("Added " + defenseValue + " defense, current Defense is " + DefenseValue);
        ui.UpdateText(DefenseValue, health.GetCurrentHealth());
    }
}
