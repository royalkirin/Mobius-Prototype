using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Maybe this class is redundant after we do everything in Chain system.
public class BattleGround : MonoBehaviour //(BG)
{
    [SerializeField] Canvas BGCardPlayCanvas = null;


    //these vars are for cards face down
    [SerializeField] Image playerTrapCardImage = null;
    [SerializeField] Image enemyTrapCardImage = null;
    bool isPlayerTrapCardAcive = false;
    bool isEnemyTrapCardActive = false;

    private void Start()
    {
        FindVariables();
    }

    //set up things, check for nulls.
    private void FindVariables()
    {
        if (BGCardPlayCanvas is null)
        {
            Debug.Log("Need Canvas reference in " + name);
        }



        if (playerTrapCardImage is null)
        {
            Debug.Log("Need Player trap card reference in " + name);
        }
        else
        {
            playerTrapCardImage.gameObject.SetActive(false);
        }

        if (enemyTrapCardImage is null)
        {
            Debug.Log("Need Enemy trap card reference in " + name);
        }
        else
        {
            enemyTrapCardImage.gameObject.SetActive(false);
        }
    }

    public Canvas GetCanvas()
    {
        return BGCardPlayCanvas;
    }



  //REDUNTDANT?

    //play a trap card on bg. isPlayer = is this card players'? if not, the enemy plays this card.
    private void PlayCardFaceDown(Card card, bool isPlayer)
    {
        //TODO: implement this
        Debug.Log("Implement this");
    }

    public bool IsPlayerTrapCardActive()
    {
        return isPlayerTrapCardAcive;
    }

    public bool IsEnemyTrapCardActive()
    {
        return isEnemyTrapCardActive;
    }
}
