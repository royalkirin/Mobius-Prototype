using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//player will choose his class here
//by changing the characterCards
//The Deck.cs will read this info, create a deck based on CharacterCards.
public class CharacterSelection : MonoBehaviour
{

    //this will hold the information of the Character name, for example, Ronin.
    //Scriptable objects in Huy's Combat Prototype/Character card collection
    public CharacterCardCollection characterCards;

    private void Start()
    {
        if(characterCards is null)
        {
            Debug.LogError("Missing character cards in " + name);
        }
    }

    public bool SetCharacter(CharacterCardCollection characterCards)
    {
        //add checking conditions here?

        this.characterCards = characterCards;
        return true;
    }

    public CharacterCardCollection GetCharacterCardCollection()
    {
        return characterCards;
    }










}
