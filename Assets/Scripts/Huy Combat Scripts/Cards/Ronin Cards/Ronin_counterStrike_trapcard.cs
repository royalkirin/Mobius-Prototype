using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ronin_counterStrike_trapcard : TrapCard
{
    private void Start()
    {

        //this card doesn't counter anything
        SetCounterAttackCard(true);
        SetCounterDefenseCard(false);
        SetCounterSupportCard(false);

        TrapCardName = "ronin_counter_strike";
    }
    public override bool ActivateTrapCard()
    {
        Debug.LogWarning("Counter Strike just end the chain and have no other effect.");
        return true;
    }
}
