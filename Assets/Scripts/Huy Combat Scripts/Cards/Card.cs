using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class represents a Card on the Canvas that player can drag/drop/ to play
//For now, it's base class for AttackCard, DefenseCard
//In the future, we expand the base class depends on the need (Spell cards, sounds, animations...)

public class Card : MonoBehaviour
{
    public bool isPlayed = false;
    Image uiImage = null;

    //images of the card. We pass these values when the cards are played onto BGCardPlayCanvas
    [SerializeField] Sprite frontImage = null;
    [SerializeField] Sprite backImage = null;
    
    private void Start()
    {
        FindVariables();
    }

    private void FindVariables()
    {
        if(frontImage is null)
        {
            Debug.Log("Missing front image in: " + name);
        }
        if(backImage is null)
        {
            Debug.Log("Missing back image in: " + name);
        }

        uiImage = GetComponent<Image>();
        if(uiImage is null)
        {
            Debug.Log("No Image component in" + name);
        }
        else
        {
            //show the image of card front
            uiImage.sprite = frontImage;
        }
    }




    public void Play(GameObject target)
    {
        isPlayed = true;
        

        //Debug.Log("Card played in base class");//for reference
    }

    public Sprite GetFrontImage()
    {
        return frontImage;
    }

    public Sprite GetBackImage()
    {
        return backImage;
    }
        
}
