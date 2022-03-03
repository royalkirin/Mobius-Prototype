using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//The scripting in charge of making sure the text is updated correctly. 
public class TextUpdates : MonoBehaviour
{
    [SerializeField] TMP_Text _maintext;
    [SerializeField] TMP_Text _instructionstext;

    [SerializeField]
    int _numberUp = 0;
    [SerializeField]
    bool _allowclicks = false;

    void Start()
    {
        //Checks to see if the text objects are assigned.
        if (_maintext || _instructionstext is null)
        {
            Debug.Log("Missing: instruction/main field");
        }

        StartCoroutine(TurnOffCards());
        //Shows the first text.
        Text("Welcome to the World of Mobius!", "Left click to continue!");
    }

    void Update()
    {
        //This bool is to keep the player from skipping forward in the text before the drag&drop function is disabled.
        if(_allowclicks == true)
        {
            Clicks();
            UpdateText();
        }
    }

    //This function tracks the amount of clicks the player has made.
    private void Clicks()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)) { _numberUp++; }
    }

    //This function is what updates the text each time the mouse button is clicked.
    private void UpdateText()
    {
        if (_numberUp == 1)
        {
            Text("Lets Get Started!" + " " + "Green icons represent the character's Health. ", "Left click to continue!");
        }
        else if (_numberUp == 2)
        {
            Text("Blue icons represent the character's Shield. ", "Left click to continue");
        }
        else if (_numberUp == 3)
        {
            Text("Your goal is to reduce the enemy's health to 0. ", "Left click to continue");
        }
        else if (_numberUp == 4)
        {
            Text("Bottom Colum displays all the cards in your hand. ", "Hover your mouse over the card to see more detail");
        }
        else if (_numberUp == 5)
        {
            Text("At the beginning of your turn, draw from the deck until you have 5 cards in your hand. ", "Left click to continue");
        }
        else if (_numberUp == 6)
        {
            Text("At the end of your turn, discard your hand to 5. ", "Left click to continue");
        }
        //need to continue.

        else if (_numberUp >= 7) 
        { this.gameObject.SetActive(false); }
    }
    
    //This corutine makes sure all the things we need to turn off do. 
    IEnumerator TurnOffCards()
    {
        yield return new WaitForSeconds(1f);
        //turn off stuff
        _allowclicks = true;
    }

    //Main function used to update the text boxes on the screen.
    private void Text(string main, string instructions)
    {
        _maintext.text = main.ToString();
        _instructionstext.text = instructions.ToString();
    }
}
