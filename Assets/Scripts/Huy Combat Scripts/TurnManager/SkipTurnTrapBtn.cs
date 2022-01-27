using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipTurnTrapBtn : MonoBehaviour
{
    public void OnBtnClick()
    {
        TurnManager turnManager = FindObjectOfType<TurnManager>();
        if (turnManager is null)
        {
            Debug.LogWarning("Missing turn manager in " + name);
            return;
        }
        else
        {
            turnManager.playerChooseToSkipTurn = true;
            turnManager.didPlayerChooseATrapOption = true;

        }

    }
}
