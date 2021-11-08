using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;
        Mathf.Clamp(health, 0, maxHealth);
        if (health <= 0)
        {
            Debug.Log("I am dead, not big suprise");
        }
    }
}
