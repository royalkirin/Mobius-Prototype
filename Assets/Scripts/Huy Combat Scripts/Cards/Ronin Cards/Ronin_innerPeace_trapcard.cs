using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this trap card doesn't countyer any card.
public class Ronin_innerPeace_trapcard : TrapCard
{
    //this card is automatically activated at beginning of turn
    //bool isAutoActivated = true;

    private void Start()
    {

        //this card doesn't counter anything
        SetCounterAttackCard(false);
        SetCounterDefenseCard(false);
        SetCounterSupportCard(false);

        TrapCardName = "ronin_inner_peace";
    }

    //when activated, set the next attack card to be invincible
    public override bool ActivateTrapCard()
    {
        //base.ActivateTrapCard();

        Card card = GetComponent<Card>();
        if (!card.belongToPlayer)
        {
            Debug.LogWarning("Trap card NOT BELONG TO PLAYER");
            return false;
        }

        GameObject friendlyChar = GameObject.FindWithTag("PlayerCharacter");
        CharacterBuffs characterBuffs = friendlyChar.GetComponent<CharacterBuffs>();
        characterBuffs.SetNextAttackCardCannotBeCountered(true);
        return true;
    }


}
