using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardPlayer))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] CardPlayer cardPlayer = null;

    /////////////////
    ///these vars are used to help enemy play simple card
    ///improved by reading card information in Enemyhand and deduct proper play
    ///no longer of use.
    [SerializeField] AttackCard attackCard;
    [SerializeField] DefenseCard defenseCard;
    [SerializeField] SupportCard supportCard;
    /////////////////

    TurnManager turnManager;

    CardChain cardChain;


    bool alwaysCounter = false;

    EnemyHand enemyHand;


    bool testing = false;
   

    private void Start()
    {
        FindVariables();

    }

    private void FindVariables()
    {
        if (cardPlayer is null)
        {
            cardPlayer = GetComponent<CardPlayer>();
        }
        turnManager = FindObjectOfType<TurnManager>();

        cardChain = GameObject.FindWithTag("CardChain").GetComponent<CardChain>();
        if (cardChain is null)
        {
            Debug.Log("Cannot find cardChain in " + name);
        }
        enemyHand = GameObject.FindWithTag("EnemyHand").GetComponent<EnemyHand>();
        if (enemyHand is null)
        {
            Debug.LogWarning("Cannot find Enemy Hand in " + name);
        }



    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            alwaysCounter = !alwaysCounter;
            Debug.Log("Enemy AI always counter = " + alwaysCounter);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            CountAttackCard();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            CountDefenseCard();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            CountSupportCard();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            testing = !testing;
            Debug.Log("Testing = " + testing);
        }
    }

    //when it's enemy turn, play.
    public IEnumerator OnEnemyTurn(float sec = 8f)
    {
        yield return new WaitForSeconds(sec);
        PlayFirstCardInChain();
    }

    private void PlayFirstCardInChain()
    {
        //if enemy hand is empty
        if(enemyHand.cardsInHand.Count <= 3)
        {
            Debug.Log("Enemy hand has <= 3 cards. Give up the turn.");
            turnManager.DefaultChangeTurn();
            return;
        }


        int numbAtt = CountAttackCard() ;
        int numbDef = CountDefenseCard();
        int numbSup = CountSupportCard();

        int max = Mathf.Max(Mathf.Max(numbAtt, numbDef), numbSup);
        Debug.Log(numbAtt + " " + numbDef + " " + numbSup + " " + max);



        if(enemyHand is null)
        {
            Debug.Log("Enemy Hand is null, we proceed with simple AI");
            //very simple instruction, first crack at Enemy AI, just for testing purposes
            AttackCard newAttackCard = Instantiate(attackCard, new Vector3(0, 0, 0), Quaternion.identity);
            cardPlayer.PlayCard(newAttackCard);
            return;
        }
        else
        { //PLAY the card that we have the most in hand first.
          //priority if equal: Attack > Defense > Support
          //cardPlayer.PlayCard(enemyHand.cardsInHand[0]);

            bool isPlayed = false;
            if(numbAtt == max)
            {
                int firstAtt = FindAttackCard();
                if(firstAtt != -1)
                {
                    Debug.Log("playing attack");
                    isPlayed = cardPlayer.PlayCard(enemyHand.cardsInHand[firstAtt]);
                }

            } else if (numbDef == max)
            {
                int firstDef = FindDefenseCard();
                if (firstDef != -1)
                {
                    Debug.Log("playing defense");
                    isPlayed = cardPlayer.PlayCard(enemyHand.cardsInHand[firstDef]);
                }
            }
            else if (numbSup == max)
            {
                int firstSup = FindSupportCard();
                if (firstSup != -1)
                {
                    Debug.Log("playing support");
                    isPlayed = cardPlayer.PlayCard(enemyHand.cardsInHand[firstSup]);
                }
            }
            else
            {
                Debug.LogError("This part should not be reached.");
            }
            Debug.Log("isPlayed: " + isPlayed);
        }

    }

    public void EndEnemyTurn()
    {
        StartCoroutine(WaitEnemyEndTurn(5));
    }

    //delays little bit until we end the turn in Turn manager
    IEnumerator WaitEnemyEndTurn(int secs)
    {
        yield return new WaitForSeconds(secs);
        turnManager.DefaultChangeTurn();
    }

    //enemy reacts to player in the chain. why try his best to counter.
    public void OnEnemyReactTurn()
    {
        Card lastCardPlayed = cardChain.GetLastCardPlayed();
        if(lastCardPlayed is null)
        {
            Debug.Log("last card played in chain is null");
        }

        StartCoroutine(EnemyPlayReactionCard(1f));

    }
    //always play a reaction card if possible
    IEnumerator EnemyPlayReactionCard(float sec)
    {
        yield return new WaitForSeconds(sec);

        if (testing)
        {
            if (!cardChain.chainEnding) //give up the chain
            {
                cardChain.ChainEnd(isPlayer: false);
            }
        }
        else
        {
            Card lastCardInChain = cardChain.GetLastCardPlayed();

            /////////
            ///TRY to counter until we run out of card, or we cannot counter
            int indexToPlay = -1;
            if (lastCardInChain is AttackCard)
            {
                indexToPlay = FindDefenseCard();
            }
            else if (lastCardInChain is DefenseCard)
            {
                indexToPlay = FindSupportCard();
            }
            else //last card played =  Support card
            {
                indexToPlay = FindAttackCard();
            }
            if (indexToPlay == -1)//meaning we have 0 cards in hand, or cannot counter the last card
            {
                if (!cardChain.chainEnding) //give up the chain
                {
                    cardChain.ChainEnd(isPlayer: false);
                }
            }
            else //counter the last card in the chain
            {
                bool isPlayed = cardPlayer.PlayCard(enemyHand.cardsInHand[indexToPlay]);
                if (!isPlayed)
                {
                    Debug.LogError("Cannot play? Check counter logic.");
                }
            }
        }




        //OLD CODE
        //bool isPlayed = cardPlayer.PlayCard(newCard);
        //if (!isPlayed)
        //{
        //    Debug.Log("Destroy new card just created because fail to play");
        //    Destroy(newCard.gameObject);
        //if (!cardChain.chainEnding)
        //{
        //    cardChain.ChainEnd(isPlayer: false);
        //}
        //}

    }


    //functions to Analyze EnemyHand.cardsInHand to deduct a good play
    #region ANALYZE_HAND

    //return index of attack card in cardsInHand.
    //return -1 if fail
    private int FindAttackCard()
    {
        for(int i = 0; i < enemyHand.cardsInHand.Count; i++)
        {
            if(enemyHand.cardsInHand[i] is AttackCard)
            {
                //Debug.Log("Attack card index = " + i);
                return i;
            }
        }
        //Debug.Log(-1);
        return -1;
    }

    //return index of defense card in cardsInHand.
    //return -1 if fail
    private int FindDefenseCard()
    {
        for (int i = 0; i < enemyHand.cardsInHand.Count; i++)
        {
            if (enemyHand.cardsInHand[i] is DefenseCard)
            {
                //Debug.Log("Defense card index = " + i);
                return i;
            }
        }
        //Debug.Log(-1);
        return -1;
    }

    //return index of support card in cardsInHand.
    //return -1 if fail
    private int FindSupportCard()
    {
        for (int i = 0; i < enemyHand.cardsInHand.Count; i++)
        {
            if (enemyHand.cardsInHand[i] is SupportCard)
            {
                //Debug.Log("Support card index = " + i);
                return i;
            }
        }
        //Debug.Log(-1);
        return -1;
    }

    private int CountAttackCard()
    {
        int count = 0;
        for (int i = 0; i < enemyHand.cardsInHand.Count; i++)
        {
            if (enemyHand.cardsInHand[i] is AttackCard)
            {
                count++;
            }
        }
        //Debug.Log(count);
        return count;
    }

    private int CountDefenseCard()
    {
        int count = 0;
        for (int i = 0; i < enemyHand.cardsInHand.Count; i++)
        {
            if (enemyHand.cardsInHand[i] is DefenseCard)
            {
                count++;
            }
        }
        //Debug.Log(count);
        return count;
    }

    private int CountSupportCard()
    {
        int count = 0;
        for (int i = 0; i < enemyHand.cardsInHand.Count; i++)
        {
            if (enemyHand.cardsInHand[i] is SupportCard)
            {
                count++;
            }
        }
        //Debug.Log(count);
        return count;
    }


    #endregion
}
