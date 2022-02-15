using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class represents a Card on the Canvas that player can drag/drop/ to play
//For now, it's base class for AttackCard, DefenseCard
//In the future, we expand the base class depends on the need (Spell cards, sounds, animations...)

public class Card : MonoBehaviour {
    public bool isPlayed = false;
    public bool belongToPlayer = true; //CardPlayer.cs will set right value when play the card.
    Image uiImage = null;

    //images of the card. We pass these values when the cards are played onto BGCardPlayCanvas
    [SerializeField] Sprite frontImage = null;
    [SerializeField] Sprite backImage = null;
    [SerializeField] Sprite trapImage = null;

    public int positionInHand = 0;//represent the position in the hand of the player
    CardChain cardChain;

    private void Start() {
        FindVariables();

    }

    private void FindVariables() {
        if (frontImage is null) {
            Debug.Log("Missing front image in: " + name);
        }
        if (backImage is null) {
            Debug.Log("Missing back image in: " + name);
        }
        if (trapImage is null) {
            Debug.LogError("Missing Trap immage in " + name);
        }


        uiImage = GetComponent<Image>();
        if (uiImage is null) {
            Debug.Log("No Image component in" + name);
        } else {
            //show the image of card front
            uiImage.sprite = frontImage;
        }

        cardChain = GameObject.FindWithTag("CardChain").GetComponent<CardChain>();
        if (cardChain is null) {
            Debug.Log("Cannot find cardChain in " + name);
        }
    }




    public virtual void Play(GameObject target) {
        isPlayed = true;


        //Debug.Log("Card played, belong to player = " + belongToPlayer);//for reference
    }

    public Sprite GetFrontImage() {
        return frontImage;
    }

    public Sprite GetBackImage() {
        return backImage;
    }

    public Sprite GettrapImage() {
        return trapImage;
    }

    public bool BelongToPlayer() {
        return belongToPlayer;
    }

    public void SetOwner(bool belongToPlayer) {
        this.belongToPlayer = belongToPlayer;
    }

    //static method to check if cardB counters cardA
    //return true if B counters A. False otherwise.
    public static bool Counter(Card cardA, Card cardB) {
        if (cardA is AttackCard) {
            return ( cardB is DefenseCard );
        }

        if (cardA is DefenseCard) {
            return ( cardB is SupportCard );
        }

        if (cardA is SupportCard) {
            return ( cardB is AttackCard );
        }

        Debug.Log("First card is neither Attack, Defense, or Support.");
        return false;
    }


    private void CardOnHandGlowIfCanBeUsed() {


        if (isPlayed ==false) {
            if (Card.Counter(CardChain.Instance.GetLastCardPlayed(), this)) {

            }
        }

    }




}
