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
    float DefenseValue = 0f;
    [SerializeField] Health health = null;
    [SerializeField] CharacterAttackableUI ui;

    //Sarah's: add UI elements here

    [SerializeField] HealthAndDefense healthAndDefenseUI;


    /// Shield stuffs
    [SerializeField] private GameObject Shield; //if gameobject active, the shielsd will be destroied and the player will not receive damage


    private void Start()
    {
        Shield.SetActive(false);

        if (health is null)
        {
            Debug.Log("Missing Health in " + name);
        }
        if(ui is null)
        {
            ui = GetComponent<CharacterAttackableUI>();
        }
         ui.UpdateText(DefenseValue, health.GetCurrentHealth());

        //Sarah's: initial update on UI elements?
        if(healthAndDefenseUI != null) {
            healthAndDefenseUI.recordedHealth = (int)health.GetCurrentHealth();
            healthAndDefenseUI.health = (int)health.GetCurrentHealth();

            healthAndDefenseUI.recordedDefense = (int)DefenseValue;
            healthAndDefenseUI.defense = (int)DefenseValue;
        }
        

    }

    //Call this function to deal damage to an object.
    public void TakeDamage(float damage)
    {
        //shield parts
        bool isShielded = TakeDamageWithShield();
        if (!isShielded) 
        {
            damage = TakeDamageWithDefense(damage);
            if (damage > 0)
            {
                health.TakeDamage(damage);
            }
        }

        ui.UpdateText(DefenseValue, health.GetCurrentHealth()) ;

        //Sarah's: Update ui based on health, defense here
        if (healthAndDefenseUI != null) {
            healthAndDefenseUI.health = (int)health.GetCurrentHealth();
            healthAndDefenseUI.defense = (int)DefenseValue;
        }


    }


    //return true if damage is absorb by the shield.
    private bool TakeDamageWithShield()
    {
        //if the shield is active, the player will not receive damage
        if (Shield.activeInHierarchy)
        {
            Debug.Log("Is pending put shield breacking animation");
            Shield.SetActive(false);
            return true;
        }
        return false;
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
        //Sarah's: Update ui based on health, defense here
        if (healthAndDefenseUI != null) {
            healthAndDefenseUI.health = (int)health.GetCurrentHealth();
            healthAndDefenseUI.defense = (int)DefenseValue;
        }
    }


    public void RaiseTheShield()
    {
        Shield.SetActive(true);
    }

    //for deactivating end of turn.
    public void LowerTheShield() {
        Shield.SetActive(false);
    }
}
