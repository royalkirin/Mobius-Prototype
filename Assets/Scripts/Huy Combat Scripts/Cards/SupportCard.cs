using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportCard : Card
{

    // Update is called once per frame
    public new void Play(GameObject target)
    {
        base.Play(target);
        //TODO: Add code to remove debuffs once debuffs are implemented
        //Also add juicy stuff like animations and whatnot
    }
}
