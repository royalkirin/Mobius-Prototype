using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightManager_Turn : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject playerLight;
    [SerializeField] GameObject enemyLight;
    [SerializeField] TurnManager turnManager;

    private void Start()
    {
        turnManager = this.GetComponent<TurnManager>();
        playerLight.SetActive(true);
    }

    private void Update()
    {
        CurrentPlayerTurn();
    }

    private void CurrentPlayerTurn()
    {
        if(turnManager.isPlayerReactTurn == true)
        {
            enemyLight.SetActive(false);
            playerLight.SetActive(true);
        }
        else if(turnManager.isPlayerReactTurn == false)
        {
            playerLight.SetActive(false);
            enemyLight.SetActive(true);
        }
    }
}
