using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBarFunctions : MonoBehaviour
{
    [SerializeField] GameObject defenseHolder;
    [SerializeField] HealthAndDefense defenseCaller;
    [SerializeField] GameObject[] defenseSegements = new GameObject[0];

    [SerializeField] bool startShaking;

    // Start is called before the first frame update
    void Start()
    {
        defenseCaller = defenseHolder.GetComponent<HealthAndDefense>();
        if (defenseCaller is null)
        {
            Debug.LogWarning("Defense caller is null");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (defenseCaller.defense >= 0)
        {
            if (defenseCaller.recordedDefense > defenseCaller.defense && defenseCaller.recordedDefense <= 10)
            {
                defenseSegements[defenseCaller.recordedDefense - 1].GetComponent<DefenseBarShaker>().startShake = true;
            }

            else if (defenseCaller.recordedDefense < defenseCaller.defense)
            {
                if (defenseCaller.recordedDefense < 10)
                    defenseSegements[defenseCaller.recordedDefense].GetComponent<DefenseBarShaker>().startBeat = true;
            }
        }
    }
}
