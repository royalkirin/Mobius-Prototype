using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class handles all the Buff and Debuff of each character.
//attach script to Friendly Char and Enemy Char.
public class CharacterBuffs : MonoBehaviour
{
    [SerializeField] bool belongToPlayer;

    //Sarah's: UI for character buffs?



    #region NEXT_ATTACK_CANNOT_BE_COUNTERED

    //remember to reset all the buffs to false after its implemented.
    private bool NextAttackCardCannotBeCountered = false;


    //this buff can only be possessed by player.
    public void SetNextAttackCardCannotBeCountered(bool newBool)
    {
        NextAttackCardCannotBeCountered = newBool;
        if (!belongToPlayer)
        {
            Debug.LogWarning("This character buff does not belong to player");
            return;
        }
        else
        {
            //set cardchain.playerInvincibleCard to attack card.
            CardChain cardChain = GameObject.FindWithTag("CardChain").GetComponent<CardChain>();
            if (newBool)
            {
                cardChain.playerInvincibleCard = CardChain.InvincibleCard.Attack;
            }
            else
            {
                cardChain.playerInvincibleCard = CardChain.InvincibleCard.None;
            }
            Debug.LogWarning("Player invincible card: " +  cardChain.playerInvincibleCard);
        }
    }
    public bool GetNextAttackCardCannotBeCountered()
    {
        return NextAttackCardCannotBeCountered;
    }

    #endregion


    

    #region ZANSHIN_SHIELD
    //remember to reset all the buffs to false after its implemented.
    private bool NextAttackNullified = false;


    //this buff can only be possessed by player.
    //Leo implemented the RaiseShield. Check  out Ronin_zanshin.cs
    public void RaiseShield(bool newBool) {
        NextAttackNullified = newBool;
        if (newBool) {
            Debug.LogWarning("Check out Ronin_zanshin.cs for Leo's implementation");
        } else{
            //we can deactivate here
            GetComponent<Attackable>().LowerTheShield();
        }
        
    }
    public bool GetNextAttackNullifief() {
        return NextAttackNullified;
    }



    #endregion

    //another buff here?


    //removes all the buffs that expires after 1 turn.
    public void RemoveBuffsEndOfTurn()
    {
        if (NextAttackCardCannotBeCountered){
            SetNextAttackCardCannotBeCountered(false);
        }
        if (NextAttackNullified) {
            RaiseShield(false);
        }
        
        ///Sarah's: update character buff elements here
        
    }
}
