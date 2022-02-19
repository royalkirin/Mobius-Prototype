using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn_Indication : MonoBehaviour
{
    //Finds the turn manager and the turn indicator at the top of the combat hud screen. 
    [Header("References")]
    [SerializeField]
    private TurnManager _TM;
    [SerializeField]
    private GameObject _turnindicator;

    private void Start()
    {
        //finds the references to those to in the current scene. 
        _TM = FindObjectOfType<TurnManager>();
        _turnindicator = this.gameObject;
    }

    private void Update()
    {
        //calls the function to update the color as soon as the turn changes. 
        UpdateTurnIndicator();
    }

    //handles the color swap. (incomplete need to add code to account for coding done in turn manager.)
    private void UpdateTurnIndicator()
    {
        if (_TM.isPlayerReactTurn == true) _turnindicator.GetComponent<Image>().color = Color.green; else _turnindicator.GetComponent<Image>().color = Color.red;
    }
}
