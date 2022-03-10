using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthAndDefense : MonoBehaviour
{
    [SerializeField] Attackable attackable;


    public int health; // chagnge it here
    public int recordedHealth;
    [SerializeField] Text healthText;
    [SerializeField] GameObject heartAnimator;

    public int defense;
    public int recordedDefense;
    [SerializeField] Text defenseText;
    [SerializeField] GameObject shieldAnimator;

    // Start is called before the first frame update
    void Start()
    {
        recordedHealth = health;
        recordedDefense = defense;
        //Debug.Log(name);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthText.text = "" + health;
        defenseText.text = "" + defense;

        if (recordedHealth > health)
        {
            heartAnimator.GetComponent<HealthHeartAnim>().startShake = true;
            recordedHealth = health;
        }
        else if (recordedHealth < health)
        {
            heartAnimator.GetComponent<HealthHeartAnim>().startBeat = true;
            recordedHealth = health;
        }

        if (recordedDefense > defense)
        {
            shieldAnimator.GetComponent<DefShieldBeat>().startShake = true;
            recordedDefense = defense;
        }
        else if(recordedDefense < defense)
        {
            shieldAnimator.GetComponent<DefShieldBeat>().startBeat = true;
            recordedDefense = defense;
        }
    }
}
