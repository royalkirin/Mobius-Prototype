using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this is a base class for every trap card in the game.
//The TrapCard is not derived from Card because each type of card can also have a trap card effect
//the trapcard is a separate component that SOME cards have
//Each trap card component of each character will be implemented separately, derived them from this class
//this script only handles the logic of the trap, not the UI. UI is already handled in Card -> BackImage
//While this script can be attached to an object, I prefer creating new script for each trap card and attach
//those scripts into specific att/defense/supp card prefab.
public class TrapCard : MonoBehaviour
{
    public string TrapCardName = "default_trap_card";

    //each trapcard counters attack/defense/support separately, not like the usual logic 
    //(usual logic: att < defense < support < att)
    //SET UP THESE BOOLS WHEN CREATING EACH TRAP CARD.
    bool counterAttackCard = true; //does this trap counter attack card?
    bool counterDefenseCard = true;//does this trap counter defense card?
    bool counterSupportCard = true; //does this trap counter support card?




    //TODO: expand this later.
    public virtual bool ActivateTrapCard()
    {
        Debug.Log("TRAP CARD ACTIVATED. IMPLEMENT IT HERE ");


        //temporary solution: activate normal effect of the card
        GameObject.FindWithTag("Player").GetComponent<CardPlayer>().
            CardTakesEffect(GetComponent<Card>());


        return false;
    }

    public bool doesCounterAttackCard()
    {
        return counterAttackCard;
    }

    public bool doesCounterDefenseCard()
    {
        return counterDefenseCard;
    }

    public bool doesCounterSupportCard()
    {
        return counterSupportCard;
    }

    public void SetCounterAttackCard(bool counterAttackCard)
    {
        this.counterAttackCard = counterAttackCard;
    }
    public void SetCounterDefenseCard(bool counterDefenseCard)
    {
        this.counterDefenseCard = counterDefenseCard;
    }
    public void SetCounterSupportCard(bool counterSupportCard)
    {
        this.counterSupportCard = counterSupportCard;
    }
}
