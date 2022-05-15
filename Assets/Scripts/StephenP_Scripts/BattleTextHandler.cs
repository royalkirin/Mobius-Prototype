using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleTextHandler : MonoBehaviour
{
    private TextMeshProUGUI _tmpBattleText;

    public static BattleTextHandler Instance;

    float fPlayerATTACK, fPlayerDEFENSE, fPlayerSUPPORT;
    float fEnemyATTACK, fEnemyDEFENSE, fEnemySUPPORT;
    int nPlayerDraw, nEnemyDraw;

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

    public void UpdateBattleText(bool bIsPlayerTurn)
    {
        _tmpBattleText.text = string.Empty;

        if (nPlayerDraw > 0)
        {
            _tmpBattleText.text += "\n" + "<color=blue>Player</color>: Draws " + nPlayerDraw;
        }

        if (nEnemyDraw > 0)
        {
            _tmpBattleText.text += "\n" + "<color=red>Enemy</color>: Draws " + nEnemyDraw;
        }

        if (fPlayerATTACK > 0)
        {
            _tmpBattleText.text += "\n" + "<color=blue>Player</color>: Attacks for " + fPlayerATTACK + " Damage!";
        }

        if (fEnemyATTACK > 0)
        {
            _tmpBattleText.text += "\n" + "<color=red>Enemy</color>: Attacks for " + fEnemyATTACK + " Damage!";
        }

        if (fPlayerDEFENSE > 0)
        {
            _tmpBattleText.text += "\n" + "<color=blue>Player</color>: Increases their Shield by " + fPlayerDEFENSE + "!";
        }

        if (fEnemyDEFENSE > 0)
        {
            _tmpBattleText.text += "\n" + "<color=red>Enemy</color>: Increases their Shield by " + fEnemyDEFENSE + "!";
        }

        if (fPlayerSUPPORT > 0)
        {
            _tmpBattleText.text += "\n" + "<color=blue>Player</color>: Draws " + fPlayerSUPPORT + " cards.";
        }

        if (fEnemySUPPORT > 0)
        {
            _tmpBattleText.text += "\n" + "<color=red>Enemy</color>: Draws " + fEnemySUPPORT + " cards.";
        }

        if (bIsPlayerTurn)
            _tmpBattleText.text += "\n" + "\n" + "The Chain begins!" + "\n" + "What will <color=blue>Player</color> do now?";
        else
            _tmpBattleText.text += "\n" + "\n" + "The Chain begins!" + "\n" + "How will the <color=red>Enemy</color> play this turn?";

        ResetValues();
    }

    public void IncrementDAMAGEValue(float fValue, bool bIsPlayer)
    {
        if (bIsPlayer)
            fPlayerATTACK += fValue;
        else
            fEnemyATTACK += fValue;
    }

    public void IncrementDEFENSEValue(float fValue, bool bIsPlayer)
    {
        if (bIsPlayer)
            fPlayerDEFENSE += fValue;
        else
            fEnemyDEFENSE += fValue;
    }

    public void IncrementSUPPORTValue(float fValue, bool bIsPlayer)
    {
        if (bIsPlayer)
            fPlayerSUPPORT += fValue;
        else
            fEnemySUPPORT += fValue;
    }

    public void CountCardsDrawn(int nValue, bool bIsPlayer)
    {
        if (bIsPlayer)
            nPlayerDraw += nValue;
        else
            nEnemyDraw += nValue;
    }

    void ResetValues()
    {
        fPlayerATTACK = 0;
        fPlayerDEFENSE = 0;
        fPlayerSUPPORT = 0;

        fEnemyATTACK = 0;
        fEnemyDEFENSE = 0;
        fEnemySUPPORT = 0;

        nPlayerDraw = 0;
        nEnemyDraw = 0;
    }
}
