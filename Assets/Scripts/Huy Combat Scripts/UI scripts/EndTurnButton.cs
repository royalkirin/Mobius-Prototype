using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//manages the End Turn button
//will communicate with TurnManager to decide logics.
public class EndTurnButton : MonoBehaviour
{
    TurnManager turnManager = null;
    Button btn;
    private void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        if(turnManager is null)
        {
            Debug.Log("Cannot find Turn Manager in " + name);
        }

        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnEndTurnButtonClick);
        Debug.Log("EndTurnBtn.cs added listenter on End Turn btn");
    }

    //when clicked, Player signifies to TurnManager that he wants to change the turn
    public void OnEndTurnButtonClick()
    {
        if (turnManager != null)
        {
            turnManager.PlayerChangeTurn();
            AudioManager.instance.Play("ButtonClick");
        }
    }
}
