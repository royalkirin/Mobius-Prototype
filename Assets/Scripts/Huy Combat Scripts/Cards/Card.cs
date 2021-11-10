using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class represents a Card
//For now, it's base class for AttackCard, DefenseCard
//In the future, we expand the base class depends on the need (Spell cards, sounds, animations...)

public class Card : MonoBehaviour
{
    public bool isPlayed = false;

    //in the future, add fancy stuffs that relates to whenever a Card is Played here.
    //Sound effects, animation etc.
    public void Play(GameObject target)
    {
        isPlayed = true;
        //Debug.Log("Card played in base class");//for reference
    }

}
