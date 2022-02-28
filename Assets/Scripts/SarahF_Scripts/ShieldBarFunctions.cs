using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBarFunctions : MonoBehaviour
{
    [SerializeField] GameObject healthHolder;
    [SerializeField] HealthAndDefense healthCaller;
    [SerializeField] GameObject[] defenseSegements = new GameObject[0];

    [SerializeField] bool startShaking;

    // Start is called before the first frame update
    void Start()
    {
        healthCaller = healthHolder.GetComponent<HealthAndDefense>();
        if (healthCaller is null)
        {
            Debug.LogWarning("Defense caller is null");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (healthCaller.health <= 10)
        {
            if (healthCaller.recordedHealth > healthCaller.health)
            {
                if (healthCaller.health == 10)
                {

                }
                else
                {
                    for (int i = healthCaller.health; i < healthCaller.recordedHealth; i++)
                    {
                        defenseSegements[i].GetComponent<DefenseBarShaker>().startShake = true;
                    }

                    //healthSegements[healthCaller.health].GetComponent<HealthBarShaker>().startShake = true;
                }
            }
            else if (healthCaller.recordedHealth < healthCaller.health)
            {
                defenseSegements[healthCaller.health - 1].GetComponent<DefenseBarShaker>().startBeat = true;
            }
        }
    }
}
