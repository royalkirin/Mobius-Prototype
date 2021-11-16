using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//belongs to any entities that can play cards
//ex: enemy can have a CardPlayer component to play cards
public class CardPlayer : MonoBehaviour
{
    //the character on the side of this card player. For enemy, friendlyCharacter's value is enemyCharacter
    GameObject friendlyCharacter; 
    GameObject enemyCharacter; //the character on the other side.
    [SerializeField] string friendlyTag = null;
    [SerializeField] string enemyTag = null; //for players, enemyTag = Enemy. For enemy, enemyTag = PlayerCharacter

    private void Start()
    {
        friendlyCharacter = GameObject.FindWithTag(friendlyTag);
        if (friendlyCharacter is null)
        {
            Debug.Log(name + "cannot find friendly character with tag " + friendlyTag);
        }

        enemyCharacter = GameObject.FindWithTag(enemyTag);
        if(enemyCharacter is null)
        {
            Debug.Log(name + "cannot find enemy with tag " + enemyTag);
        }
    }

    //Play a random card, can be attack, defense, spell...
    public void PlayCard(Card card)
    {
        if (card is AttackCard)
        {
            AttackCard attackCard = (AttackCard)card;
            if(enemyCharacter != null)
            {
                attackCard.Play(enemyCharacter);
            }
            else
            {
                Debug.Log("Cannot access enemyCharacter in CardPlayer in " + gameObject.name);
            }    
            return;
        }

        if(card is DefenseCard)
        {
            DefenseCard defenseCard = (DefenseCard)card;
            if(friendlyCharacter != null)
            {
                defenseCard.Play(friendlyCharacter);
            }
            else
            {
                Debug.Log("Cannot access friendlyCharacter in CardPlayer in " + gameObject.name);
            }
            
            return;
        }
    }
   
}
