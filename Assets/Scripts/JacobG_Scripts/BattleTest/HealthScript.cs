using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{ // This covers the health variables.
    public int health;
    public int maxHealth = 100;
    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        //sets health to max health
        health = maxHealth;
        //sets slidermax value to max health
        healthBar.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //sets healthbar value to health
        healthBar.value = health;
        //clamps the variable to the contraints
       health = Mathf.Clamp(health, 0, maxHealth);
        if (health <= 0)
        {
            Debug.Log("I am dead, not big suprise");
        }
    }
}
