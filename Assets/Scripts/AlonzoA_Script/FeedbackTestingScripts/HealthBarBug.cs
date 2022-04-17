using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The health bar has to main bugs one is the wrong module showing and the second is animations not playing for the health bar on damage taken. 
public class HealthBarBug : MonoBehaviour
{
    [SerializeField] HealthAndDefense HaD;
    [SerializeField] Attackable enemAttackable;
    [SerializeField] Health enemHealth;
    [SerializeField] GameObject shield;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HurtHealth();
        AddShield();
    }

    private void HurtHealth()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            enemAttackable.TakeDamage(1);
        }
    }

    private void AddShield()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            shield.SetActive(true);
            enemAttackable.AddDefense(1);
        }
    }
}
