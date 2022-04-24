using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthAndDefense : MonoBehaviour
{
    [SerializeField] Attackable attackable;

    bool DEBUG;


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
    void Update()
    {
        healthText.text = "" + health;
        defenseText.text = "" + defense;

        if (Input.GetKeyUp(KeyCode.O))
        {
            health = 5;
            DEBUG = true;
        }

        if (recordedHealth > health)
        {
            if (!heartAnimator.GetComponent<HealthHeartAnim>().startShake)
            {
                heartAnimator.GetComponent<HealthHeartAnim>().startShake = true;
                recordedHealth = health;
            }
        }
        else if (recordedHealth < health)
        {
            if (!heartAnimator.GetComponent<HealthHeartAnim>().startBeat)
            {
                heartAnimator.GetComponent<HealthHeartAnim>().startBeat = true;
                recordedHealth = health;
            }
        }

        if (recordedDefense > defense)
        {
            if (!shieldAnimator.GetComponent<DefShieldBeat>().startShake)
            {
                shieldAnimator.GetComponent<DefShieldBeat>().startShake = true;
                recordedDefense--;
            }
        }
        else if(recordedDefense < defense)
        {
            if (!shieldAnimator.GetComponent<DefShieldBeat>().startBeat)
            {
                shieldAnimator.GetComponent<DefShieldBeat>().startBeat = true;
                recordedDefense++;
            }
        }
    }
}
