using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleTextHandler : MonoBehaviour
{
    private TextMeshProUGUI _tmpBattleText;

    public static BattleTextHandler Instance;

    void Awake()
    {
        _tmpBattleText = GetComponent<TextMeshProUGUI>();
        
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void UpdateBattleText(string txtUpdate)
    {
        _tmpBattleText.text += "\n" + txtUpdate;
    }
}
