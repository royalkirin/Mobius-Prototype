using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script is attached to DiscardAndContinueBtn
//allow the player to discard his trap card to continue his turn
public class DiscardAndContinue : MonoBehaviour
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
            turnManager.playerChooseToSkipTurn = false;
            turnManager.didPlayerChooseATrapOption = true;

        }

    }
}
