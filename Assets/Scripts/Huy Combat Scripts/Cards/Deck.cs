using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class represents a Deck. A Deck is a list of cards.
//this class will load cards into deck depends on the character.
public class Deck : MonoBehaviour
{
    public int CARD_COUNT = 30;
    public int eachCardAmount = -1;
    public bool tutorial;


    public List<Card> cardsInDeck;//the actual deck in the game -> no order
    public List<int> howManyEach;
    public bool belongToPlayer = false;




    //VARS for loading initial card collection
    //the list of cards to randomized from.
    //this list can be further separated into a class 
    //with [CreateAssetMenu filename...] to specify for each character.
    public List<Card> cardGenerateList;
    //for player, friendlyTag = PlayerCharacter
    //for enemy, friendlyTag = EnemyCharacter
    [SerializeField] string friendlyTag = null;
    GameObject friendlyCharacter;
    CharacterSelection characterSelection;
    CharacterCardCollection cardCollection;


    //the hand the deck deals to
    //for now, only PlayerHand, AI doesn't have a hand.
    private PlayerHand playerHand = null;



    //Enemy Hand
    [SerializeField] EnemyHand enemyHand;

    //Deck Animations
    [SerializeField] CardAnimControllerScript enemyDeckAnim, playerDeckAnim;
    bool enemyAnimOnStart = false, isEndTurnCalled = false, playerAnimOnStart = false;
    [HideInInspector] public bool doesPlayerGetCards = false;
    TurnManager turnManager;


    private void Start()
    {
        if (friendlyTag == "PlayerCharacter") {
            belongToPlayer = true;
        } else if (friendlyTag == "EnemyCharacter") {
            belongToPlayer = false;
        }
        if (tutorial)
        {
            CARD_COUNT = 30;
        }
        FindVariables();
        LoadCardListBasedOnCharacter();
        InitiateVariables();
        InitiateRandomDeck();
        if (friendlyTag == "PlayerCharacter")
        {
            FullDealToPlayer();
            Debug.Log("This deck belongs to player");
        } else if (friendlyTag == "EnemyCharacter")
        {
            FullDealToEnemy();
            Debug.Log("This deck belongs to enemy");
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            InitiateRandomDeck();
        }
    }



    private void FindVariables() {
        playerHand = GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<PlayerHand>();
        if (playerHand is null) {
            Debug.LogError("Cannot find player hand in the scene. In " + name);
        }
        enemyHand = GameObject.FindGameObjectWithTag("EnemyHand").GetComponent<EnemyHand>();
        if (enemyHand is null) {
            Debug.LogError("Cannot find enemy hand in the scene. In " + name);
        }
        friendlyCharacter = GameObject.FindWithTag(friendlyTag);
        if (friendlyCharacter is null) {
            Debug.LogError(name + "cannot find friendly character with tag " + friendlyTag);
        }

       //Deck Animations Search
        enemyDeckAnim = GameObject.Find("Enemy Card Pile").GetComponent<CardAnimControllerScript>();
        if (enemyDeckAnim is null)
        {
            Debug.LogError(name + "cannot find card pile");
        }

        playerDeckAnim = GameObject.Find("Player Card Pile").GetComponent<CardAnimControllerScript>();
        if (playerDeckAnim is null)
        {
            Debug.LogError(name + "cannot find card pile");
        }

        turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();
    }

    private void InitiateVariables() {
        eachCardAmount = CARD_COUNT / 3;
        for (int i = 0; i < cardGenerateList.Count; i++) {
            howManyEach.Add(eachCardAmount);
        }
        string msg = "Card count in Deck Att/Def/Sup: " + string.Join(", ", howManyEach);
        Debug.Log(msg);
    }

    //load the initial card list to generate randomly
    //return true if sucessfully load, false otherwise
    private bool LoadCardListBasedOnCharacter()
    {
        if (friendlyCharacter is null)
        {
            return false;
        }
        characterSelection = friendlyCharacter.GetComponent<CharacterSelection>();
        cardCollection = characterSelection.characterCards;
        if (cardCollection is null)
        {
            Debug.Log("uh oh");
            return false;
        }

        //we have a card collection here.
        //start loading cards
        cardGenerateList.Clear();
        foreach (GameObject cardPrefab in cardCollection.cardPrefabs)
        {
            Card currentCard = cardPrefab.GetComponent<Card>();
            if (currentCard is null)
            {
                Debug.LogWarning(cardPrefab.name + " prefab doesn't have a card component.");
                return false;
            }
            cardGenerateList.Add(currentCard);
        }

        return true;
    }

    //we randomize the cards from the same list, not just change the positions of the same list.
    //maybe need to change implementation in the future.
    private void InitiateRandomDeck()
    {
        cardsInDeck.Clear();
        GetFriendlyCardCount();
        UpdateFriendlyCards();

        //cards type each = 3
        //how many each = 10 
        // add cards to deck depends on HowManyEach
        int counter = 0;
        for (int i = 0; i < howManyEach.Count * 2; i++)
        {
            for (int j = 0; j < Mathf.Ceil(howManyEach[counter] / 2); j++) {
                cardsInDeck.Add(cardGenerateList[counter]);
            }

            counter++;
            if (counter >= 3)
            {
                counter = 0;
            }
        }

        ShuffleDeck();

    }


    private void ShuffleDeck() {
        //shuffle the deck
        for (int i = 0; i < cardsInDeck.Count; i++) {
            Card temp = cardsInDeck[i];
            int randomIndex = UnityEngine.Random.Range(i, cardsInDeck.Count);
            cardsInDeck[i] = cardsInDeck[randomIndex];
            cardsInDeck[randomIndex] = temp;
        }
    }


    private int GetFriendlyCardCount() {
        int count = 0;
        if (belongToPlayer) {
            count = playerHand.GetCardCount();
        } else {
            count = enemyHand.GetCardCount();
        }
        return count;
    }

    private void UpdateFriendlyCards() {
        if (belongToPlayer) {
            if (tutorial)
            {
                /*
                 * Current code causes Tutorial to lock up and increase CPU Usage constantly until it is forcibly exited.
                 * Testing was done to attempt fixing it, but for now it is shut off until a new solution is found. - Jordan Douglas
               howManyEach[0] = 2 - playerHand.GetAttackCount();
               howManyEach[1] = 2 - playerHand.GetDefenseCount();
               howManyEach[2] = 1 - playerHand.GetSupportCount();*/

                //This code is in use until the above is fixed.
                howManyEach[0] = eachCardAmount - playerHand.GetAttackCount();
                howManyEach[1] = eachCardAmount - playerHand.GetDefenseCount();
                howManyEach[2] = eachCardAmount - playerHand.GetSupportCount();
                Debug.Log("Player: " + string.Join(", ", howManyEach));
            }
            else
            {
                howManyEach[0] = eachCardAmount - playerHand.GetAttackCount();
                howManyEach[1] = eachCardAmount - playerHand.GetDefenseCount();
                howManyEach[2] = eachCardAmount - playerHand.GetSupportCount();
                Debug.Log("Player: " + string.Join(", ", howManyEach));
            }

        } else {
            howManyEach[0] = eachCardAmount - enemyHand.GetAttackCount();
            howManyEach[1] = eachCardAmount - enemyHand.GetDefenseCount();
            howManyEach[2] = eachCardAmount - enemyHand.GetSupportCount();
            Debug.Log("Enemy: " + string.Join(", ", howManyEach));
        }
    }



    //Dealing to player needs communicatation with PlayerHand

    #region DEALING_TO_PLAYER

    //deal the first card on the list (top card on the deck) to the player
    public void DealToPlayer(bool cardLimited = true)
    {
        if (!playerAnimOnStart)
        {
            playerDeckAnim.DrawCards("Transition");
            playerAnimOnStart = !playerAnimOnStart;
        }
        else if(playerAnimOnStart && doesPlayerGetCards)
        {
            doesPlayerGetCards = !doesPlayerGetCards;
            StartCoroutine(PlayerDeckAnimations());
        }

        if (cardsInDeck.Count == 0)
        {
            Debug.Log("There is no card left in the deck. Initialize a new deck for PLAYER");
            InitiateRandomDeck();
            return;
        }

        if (cardLimited == false)
        {
            // Debug.Log("Dealing unlimited card");
        }

        playerHand.AddCard(cardsInDeck[0], cardLimited);
        AudioManager.instance.Play("DrawCards");
        cardsInDeck.RemoveAt(0);
    }

    //fill the player hand until he has 5 cards or the deck is out of card.
    public void FullDealToPlayer()
    {
        while (playerHand.cardsInHand.Count < 5)
        {
            DealToPlayer();
        }
    }

    #endregion


    //Deal to enemy doesn't need to communicate with player hand, but enemy hand
    //they are different because player hands have interactions with player (mouse drag/drop, right/left click...)
    //while enemy hand just need to show some images.
    #region DEALING_TO_ENEMY
    public void DealToEnemy(bool cardLimited = true)
    {
        //Used to call deck animations. (check CardAnimControllerScript for more info.)
        if (!enemyAnimOnStart)
        {
            enemyDeckAnim.DrawCards("ToggleCardDraw");
            enemyAnimOnStart = !enemyAnimOnStart;
        }
        else if (enemyAnimOnStart && cardsInDeck.Count == 0 || turnManager.isPlayerTurn == false)
        {
            isEndTurnCalled = !isEndTurnCalled;
            StartCoroutine(EnemyDeckAnimations());

        }

        if (friendlyTag != "EnemyCharacter")
        {
            Debug.LogError("Cannot deal to Enemy when this deck doesn't belong to Enemy.");
            return;
        }

        if(enemyHand is null)
        {
            Debug.LogError("Cannot deal to Enemy without EnemyHand");
            return;
        }

        if (cardsInDeck.Count == 0)
        {
            Debug.Log("There is no card left in the deck. Initialize a new deck for ENEMY");
            InitiateRandomDeck();
            return;
        }

        //playerHand.AddCard(cardsInDeck[0], cardLimited);
        enemyHand.AddCard(cardsInDeck[0], cardLimited);
        cardsInDeck.RemoveAt(0);
    }

    public void FullDealToEnemy()
    {
        while (enemyHand.cardsInHand.Count < 5)
        {
            //isEndTurnCalled = true;
            DealToEnemy();
        }
    }


    #endregion

    #region Animations
    // Times the functions, so the process feels smoother. 
    IEnumerator EnemyDeckAnimations()
    {
        yield return new WaitForSeconds(1f);
        enemyDeckAnim.ResetCards("ToggleCardDraw");
        yield return new WaitForSeconds(1f);
        enemyDeckAnim.CardsOn();
        yield return new WaitForSeconds(1f);
        enemyDeckAnim.DrawCards("ToggleCardDraw");
        StopCoroutine(EnemyDeckAnimations());
    }

    IEnumerator PlayerDeckAnimations()
    {
        yield return new WaitForSeconds(1f);
        playerDeckAnim.ResetCards("Transition");
        yield return new WaitForSeconds(1f);
        playerDeckAnim.CardsOn();
        yield return new WaitForSeconds(1f);
        playerDeckAnim.DrawCards("Transition");
        StopCoroutine(PlayerDeckAnimations());
    }

    #endregion
}
