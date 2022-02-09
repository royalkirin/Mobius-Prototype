using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ronin_zanshin : DefenseCard
{
    //TODO: change this. We do not call the base.Play
    public override void Play(GameObject target)
    {
        base.Play(target); // remove this, implement zanshin
        isPlayed = true; //base.base.Play(), because when we remove base.play, base.base is not called.

    }
}
