using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleTextHandler : MonoBehaviour
{
    private TextMeshProUGUI _tmpBattleText;

    public static BattleTextHandler Instance;

    public string[] cTextUpdates;
    public int nTextCount;

    void Awake()
    {
        _tmpBattleText = GetComponent<TextMeshProUGUI>();

        cTextUpdates = new string[14];
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
        if (nTextCount >= 14)
        {
            _tmpBattleText.text = string.Empty;
            nTextCount = 0;
        }

        _tmpBattleText.text += "\n" + txtUpdate;
        nTextCount++;
    }
}
