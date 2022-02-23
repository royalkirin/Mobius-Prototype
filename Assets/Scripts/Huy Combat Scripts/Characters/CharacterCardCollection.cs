using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is for creating a card collection for each player
//at the start of the game, the Deck will read card collection based on Character
//into the list of card it generate.
//CARDS IN ORDER: Attack, Defense, Support. changing this order will mess with other parts.
[CreateAssetMenu(fileName = "CharacterCardCollection", menuName = "ScriptableObjects/CreateCharacterCardCollection")]
public class CharacterCardCollection : ScriptableObject
{
    public Character character;
    public List<GameObject> cardPrefabs;

    
}
