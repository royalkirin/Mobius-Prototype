using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardPlayer))]
public class EnemyAI : MonoBehaviour
{

    public enum CardTypes
    {
        Attack = 0,
        Defense,
        Support

    }

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
    //for tutorial only
    [SerializeField] bool tutorial;

    bool testing = false;

    /// <summary>
    /// Keep the value between 0 and 1
    /// The lower the value, the less the AI will counter
    /// </summary>
    public float Aggression;

    //Enemy deck animations
    CardAnimControllerScript cardAnim;
    int cardsUsed = 0;

    

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

        cardAnim = GameObject.Find("Card Pile").GetComponent<CardAnimControllerScript>();
        if (cardAnim is null)
        {
            Debug.LogWarning("Cannot find CardAnimControllerScript in Enemy Ai.");
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            alwaysCounter = !alwaysCounter;
            Debug.Log("Enemy AI always counter = " + alwaysCounter);
        }
        if (tutorial && !alwaysCounter)
        {
            alwaysCounter = true;
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

        if(cardsUsed >= 5)
        {
            cardsUsed = 0;
        }
    }

    //when it's enemy turn, play.
    public IEnumerator OnEnemyTurn(float sec)
    {
        yield return new WaitForSeconds(sec);
        PlayFirstCardInChain();
    }

    private void PlayFirstCardInChain()
    {
        //if enemy hand is empty
        if (enemyHand.cardsInHand.Count <= 3)
        {
            Debug.Log("Enemy hand has <= 3 cards. Give up the turn.");
            turnManager.DefaultChangeTurn();
            return;

            
        }

        

        //int max = Mathf.Max(Mathf.Max(numbAtt, numbDef), numbSup);
        //Debug.Log(numbAtt + " " + numbDef + " " + numbSup + " " + max);



        if (enemyHand is null)
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
          //old system
            /*bool isPlayed = false;
            if (numbAtt == max)
            {
                int firstAtt = FindAttackCard();
                if (firstAtt != -1)
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
            Debug.Log("isPlayed: " + isPlayed);*/

            //new system

            //for each type of card
            //calculate probability of losing a chain

            int[] cards = new int[3];
            cards[0] = CountAttackCard();
            cards[1] = CountDefenseCard();
            cards[2] = CountSupportCard();

            float pA, pD, pS;

            pA = pD = pS = 1.0f;

            int phs = cardPlayer.getPlayerHandSize();

            for (int i = (int)CardTypes.Attack; cards[i] > 0; i = (i + 1) % 3)
            {
                //if the player has fewer cards than the amount we can counter, it is an automatic win
                if (phs <= 0)
                {
                    pA = 0.0f;
                    break;
                }
                else
                {
                    //each card in the player's has a 1/3 chance of being the correct type to counter with
                    //by inverting the probability and multiplying the odds of all cards together we can calculate the chance that at least one card will be able to counter
                    pA *= 1.0f - Mathf.Pow(2.0f / 3.0f, (float)phs);

                    phs--;
                    cards[i]--;
                }
            }

            //repeat for the other 2 card types
            cards[0] = CountAttackCard();
            cards[1] = CountDefenseCard();
            cards[2] = CountSupportCard();

            phs = cardPlayer.getPlayerHandSize();

            for (int i = (int)CardTypes.Defense; cards[i] > 0; i = (i + 1) % 3)
            {
                //if the player has fewer cards than the amount we can counter, it is an automatic win
                if (phs <= 0)
                {
                    pD = 0.0f;
                    break;
                }
                else
                {
                    pD *= 1.0f - Mathf.Pow(2.0f / 3.0f, (float)phs);

                    phs--;
                    cards[i]--;
                }
            }

            cards[0] = CountAttackCard();
            cards[1] = CountDefenseCard();
            cards[2] = CountSupportCard();

            phs = cardPlayer.getPlayerHandSize();

            for (int i = (int)CardTypes.Support; cards[i] > 0; i = (i + 1) % 3)
            {
                //if the player has fewer cards than the amount we can counter, it is an automatic win
                if (phs <= 0)
                {
                    pS = 0.0f;
                    break;
                }
                else
                {
                    
                    pS *= 1.0f - Mathf.Pow(2.0f / 3.0f, (float)phs);

                    phs--;
                    cards[i]--;
                }
            }

            

            //play the card with the lowest chance of losing a chain

            if( pS < pA && pS < pD)
            {
                int firstSup = FindSupportCard();
                if (firstSup != -1)
                {
                    Debug.Log("playing support");
                    cardPlayer.PlayCard(enemyHand.cardsInHand[firstSup]);
                    cardAnim.HideCard(cardsUsed);
                    cardsUsed++;
                }
            }
            else if ( pD < pA)
            {
                int firstDef = FindDefenseCard();
                if (firstDef != -1)
                {
                    Debug.Log("playing defense");
                    cardPlayer.PlayCard(enemyHand.cardsInHand[firstDef]);
                    cardAnim.HideCard(cardsUsed);
                    cardsUsed++;
                }
            }
            else
            {
                int firstAtt = FindAttackCard();
                if (firstAtt != -1)
                {
                    Debug.Log("playing attack");
                    cardPlayer.PlayCard(enemyHand.cardsInHand[firstAtt]);
                    cardAnim.HideCard(cardsUsed);
                    cardsUsed++;
                }
            }
            

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
        if (lastCardPlayed is null)
        {
            Debug.Log("last card played in chain is null");
        }

        StartCoroutine(EnemyPlayReactionCard(1f));

    }
    //always play a reaction card if possible
    IEnumerator EnemyPlayReactionCard(float sec)
    {
        yield return new WaitForSeconds(sec);
        cardAnim.HideCard(cardsUsed);
        cardsUsed++;

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
            float failProb = 1.0f;
            float maxFailProb = Aggression; //the maximum possibility of failure that we are willing to accept
            int phs = cardPlayer.getPlayerHandSize();

            int[] cards = new int[3];
            cards[0] = CountAttackCard();
            cards[1] = CountDefenseCard();
            cards[2] = CountSupportCard();

            /////////
            //calculate the probability that we will fail this card chain and save your cards if the risk is too extreme
            int indexToPlay = -1;
            if (lastCardInChain is AttackCard)
            {
                indexToPlay = FindDefenseCard();

                for (int i = (int)CardTypes.Defense; cards[i] > 0; i = (i + 1) % 3)
                {
                    //if the player has fewer cards than the amount we can counter, it is an automatic win
                    if (failProb <= 0)
                    {
                        failProb = 0.0f;
                        break;
                    }
                    else
                    {
                        //each card in the player's has a 1/3 chance of being the correct type to counter with
                        //by inverting the probability and multiplying the odds of all cards together we can calculate the chance that at least one card will be able to counter
                        failProb *= 1.0f - Mathf.Pow(2.0f / 3.0f, (float)phs);

                        phs--;
                        cards[i]--;
                    }
                }

            }
            else if (lastCardInChain is DefenseCard)
            {
                indexToPlay = FindSupportCard();

                for (int i = (int)CardTypes.Support; cards[i] > 0; i = (i + 1) % 3)
                {
                    //if the player has fewer cards than the amount we can counter, it is an automatic win
                    if (failProb <= 0)
                    {
                        failProb = 0.0f;
                        break;
                    }
                    else
                    {
                        //each card in the player's has a 1/3 chance of being the correct type to counter with
                        //by inverting the probability and multiplying the odds of all cards together we can calculate the chance that at least one card will be able to counter
                        failProb *= 1.0f - Mathf.Pow(2.0f / 3.0f, (float)phs);

                        phs--;
                        cards[i]--;
                    }
                }
            }
            else //last card played =  Support card
            {
                indexToPlay = FindAttackCard();

                for (int i = (int)CardTypes.Attack; cards[i] > 0; i = (i + 1) % 3)
                {
                    //if the player has fewer cards than the amount we can counter, it is an automatic win
                    if (failProb <= 0)
                    {
                        failProb = 0.0f;
                        break;
                    }
                    else
                    {
                        //each card in the player's has a 1/3 chance of being the correct type to counter with
                        //by inverting the probability and multiplying the odds of all cards together we can calculate the chance that at least one card will be able to counter
                        failProb *= 1.0f - Mathf.Pow(2.0f / 3.0f, (float)phs);

                        phs--;
                        cards[i]--;
                    }
                }
            }
            if (indexToPlay == -1)//meaning we have 0 cards in hand, or cannot counter the last card or the risk is too high
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
