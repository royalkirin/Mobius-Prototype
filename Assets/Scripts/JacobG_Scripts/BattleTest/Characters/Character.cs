using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Character : MonoBehaviour
{
    
    [SerializeField]
    public int health;

    [SerializeField] [Range(0,50)]
    public int attack;

    [SerializeField] [Range(0,50)]
    public int eattack;

    [SerializeField]
    public int ehealth;

    [SerializeField] [Range(0,200)]
    public int maxHealth = 100;

    [SerializeField] [Range(0,200)]
    public int emaxHealth = 100;

    [SerializeField]
    public Slider healthBar;

    [SerializeField]
    public Slider ehealthBar;

    [SerializeField]
    public bool Special, Debuff, defend, Stance, stun;


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
        //if (Special)
        //{
        //    ehealth -= attack;
        //    defend = true;
        //    Stance = true;
        //}
        //else
        //{
             ehealth -= attack;
       //}
        
       
    }
    public virtual void Attack2()
    {   
            ehealth -= attack;
            defend = true;
            Stance = true;
       
    }
    public virtual void Defend()
    {
        defend = true;
    }
    public virtual void Defend2()
    {
        Special = true;
        Stance = true;
        stun = true;

    }
    public virtual void Support()
    {
        if (Debuff)
        {
            Debuff = false;
            attack *= 2;
            Debug.Log("Player has removed all debuffs");
        }
        else Debug.Log("Player had no debuffs to remove");
    }
    public virtual void Enemyattack()
    {
        if (!defend)
        {
            health -= eattack;
            if (!Debuff)
            {
  float rnum = Random.Range(0, 5);
            if(rnum >= 3)
            {
                Debuff = true;
                attack /= 2;
                Debug.Log("Player is debuffed");
            }
            }
          
            if (Special)
            {
                ehealth -= attack;
                Special = false;
                stun = true;
            }
        }
        if (stun)
        {
            defend = true;
            float rnum2 = Random.Range(0, 5);
            if(rnum2 >= 3)
            {
                stun = false;
                defend = false;
                Debug.Log("Recovered");
            }
        }
        if (defend && !stun)
        {
            defend = false;
        }

        
    }
}
