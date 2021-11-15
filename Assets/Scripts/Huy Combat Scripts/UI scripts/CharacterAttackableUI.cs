using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAttackableUI : MonoBehaviour
{
    [SerializeField]Text text;

    private void Start()
    {
        if(text is null)
        {
            Debug.Log("Missing Text in CharacterAttackableUI in " + name);
        }
    }

    public void UpdateText(float defenseValue, float health)
    {
        string display = "Health = " + health + "\n" + "Defense = " + defenseValue;
        text.text = display;
    }
}
