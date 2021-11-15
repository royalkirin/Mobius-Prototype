using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    public void OnEndTurnButtonClick()
    {
        if(turnManager != null)
        {
            turnManager.ChangeTurn();
        }
    }
}
