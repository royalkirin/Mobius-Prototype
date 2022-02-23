using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFunctions : MonoBehaviour
{
    [SerializeField] GameObject healthHolder;
    [SerializeField] HealthAndDefense healthCaller;
    [SerializeField] GameObject[] healthSegements = new GameObject[0];

    [SerializeField] bool startShaking;

    // Start is called before the first frame update
    void Start()
    {
        healthCaller = healthHolder.GetComponent<HealthAndDefense>();
        if(healthCaller is null) {
            Debug.LogWarning("Health caller is null");
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
                    healthSegements[healthCaller.health].GetComponent<HealthBarShaker>().startShake = true;
                }
            }
            else if (healthCaller.recordedHealth < healthCaller.health)
            {
                healthSegements[healthCaller.health - 1].GetComponent<HealthBarShaker>().startBeat = true;
            }
        } 
    }
}
