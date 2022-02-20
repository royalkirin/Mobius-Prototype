using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealnDefChanger : MonoBehaviour
{

    public GameObject statManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void DefenseChanger(bool statRemover)
    {
        if (statRemover == false)
        {
            statManager.GetComponent<HealthAndDefense>().defense++;
        }
        else
        {
            if (statManager.GetComponent<HealthAndDefense>().defense > 0)
            {
                statManager.GetComponent<HealthAndDefense>().defense--;
            }
        }
    }

    public void HealthChanger(bool statRemover)
    {
        if (statRemover == false)
        {
            statManager.GetComponent<HealthAndDefense>().health++;
        }
        else
        {
            if (statManager.GetComponent<HealthAndDefense>().defense > 0)
            {
                statManager.GetComponent<HealthAndDefense>().defense--;
            }
            else
            {
                if (statManager.GetComponent<HealthAndDefense>().health > 0)
                {
                    statManager.GetComponent<HealthAndDefense>().health--;
                }
            }
        }
    }
}
