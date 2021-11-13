using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Character : MonoBehaviour
{
    
    [SerializeField]
    public int health;

    [SerializeField]
    public int attack;

    [SerializeField]
    public int eattack;

    [SerializeField]
    public int ehealth;

    [SerializeField]
    public int maxHealth = 100;

    [SerializeField]
    public int emaxHealth = 100;

    [SerializeField]
    public Slider healthBar;

    [SerializeField]
    public Slider ehealthBar;

    [SerializeField]
    public bool Basic, Debuff;


    // Start is called before the first frame update
   protected virtual void Start()
    {
        health = maxHealth;
        ehealth = emaxHealth;
        healthBar.maxValue = maxHealth;
        ehealthBar.maxValue = emaxHealth;
    }

    // Update is called once per frame
   protected virtual void Update()
    {
        //sets healthbar value to health
        healthBar.value = health;
        ehealthBar.value = ehealth;
        //clamps the variable to the contraints
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health <= 0)
        {
            Debug.Log("I am dead, not big suprise");
        }
    }
  public virtual void Attack()
    {
       
        ehealth = ehealth - 10;
    }
  public virtual void Defend()
    {

    }
    public virtual void Support()
    {

    }
}
