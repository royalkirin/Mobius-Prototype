using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Trap card is a special class because each trap card has unique implementation.
//most likely, each trap card requires its own script.
//So, this is just a new "base" for trap cards. Script is simple. Just need to know if it's activated or not.
//child scripts that handle implementation of each trap card should handle isActivated depends on the situation.

public class TrapCard : Card
{


    //while isPlayed (bool in Card.cs) refers to when the card is played,
    //for trap card, isPlayed doesn't really do anything when played (except being shown on the bg)
    //it does things when activated, so we need another variable
    //so we don't mess things up with base Card logic.
    bool isActivated = false;

   



    public bool IsActivated()
    {
        return isActivated;
    }


    public void SetActivated(bool isActivated)
    {
        this.isActivated = isActivated;
    }



    //all individual trap card scripts must override this function.
    public void Activate()
    {
        SetActivated(true);
    }

}
