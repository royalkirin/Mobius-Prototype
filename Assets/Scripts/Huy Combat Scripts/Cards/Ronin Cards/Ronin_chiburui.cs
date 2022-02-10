using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ronin_chiburui : SupportCard
{
    //the only different between chiburui and default support card is the draw amount
    public override void Play(GameObject target)
    {
        base.SetNumbCardsToDraw(1);
        base.Play(target);
    }

}
