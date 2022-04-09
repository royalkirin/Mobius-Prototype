using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is responable for changing the spotlights that appear over the characters once it is their turn.
/// <summary>
/// This script is responable for changing the spotlights that appear over the characters once it is their turn.
//// The script works by getting the parent game objects, then finding their children, in this case the spotlights, and sets them to either true or false so they switch on or off. 
/// </summary>
public class SpotlightManager_Turn : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject playerRef;
    [SerializeField] GameObject enemyRef;
    [SerializeField] TurnManager turnManager;

    private void Start()
    {
        playerRef = GameObject.Find("Friendly Char");
        enemyRef = GameObject.Find("Enemy char");
        turnManager = this.GetComponent<TurnManager>();
        playerRef.transform.GetChild(5).gameObject.SetActive(true);
    }

    private void Update()
    {
        CurrentPlayerTurn();
    }

    //This code determines what turn it is based on the isPlayerReactTurn in the Turn Manager script.
    private void CurrentPlayerTurn()
    {
        if (turnManager.isPlayerReactTurn == true)
        {
            enemyRef.transform.GetChild(6).gameObject.SetActive(false);
            playerRef.transform.GetChild(5).gameObject.SetActive(true);
        }
        else if (turnManager.isPlayerReactTurn == false)
        {
            playerRef.transform.GetChild(5).gameObject.SetActive(false);
            enemyRef.transform.GetChild(6).gameObject.SetActive(true);
        }
    }
}
