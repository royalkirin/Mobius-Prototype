using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//The scripting in charge of making sure the text is updated correctly. 
public class TextUpdates : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] TMP_Text _maintext;
    [SerializeField] TMP_Text _instructionstext;

    [Header("Images")]
    [SerializeField] Image _textbackground;

    //This is for the large scale background that is sceen behind the white text box. 
    [SerializeField] GameObject _largebackground;

    [Header("References")]
    [SerializeField] TooltipCaller _TTC;
    [SerializeField] TutorialCardDropHandler _TCDH;
    [SerializeField] GameObject _TextHolder;

    [SerializeField]
    int _numberUp = 0;
    //[SerializeField]
    bool _startclicks = false;

    // _checkClicks is the bool that should be called by other scripts. 
    [HideInInspector] public bool _checkClicks = false;

    void Start()
    {
        _TTC = GameObject.FindObjectOfType<TooltipCaller>();
        _TCDH = GameObject.FindObjectOfType<TutorialCardDropHandler>();
        //Checks to see if the text objects are assigned.
        if (_maintext || _instructionstext is null)
        {
            Debug.Log("Missing: Main/Instruction Text (Check TextUpdates Script: Lines 11 || 12.)");
        }
        else if (_textbackground || _largebackground is null)
        {
            Debug.Log("Missing: Text/Large background (Check TextUpdates Script: Lines 15 || 18.)");
        }
        else if (_TTC || _TCDH is null)
        {
            Debug.Log("Missing: ToolTipCaller (Check TextUpdates Script: Line 21.)");
        }

        StartCoroutine(TurnOffCards());
        //Shows the first text.
        Text("Welcome to the World of Mobius!", "Left click to continue!");
        i = false;
    }

    void Update()
    {
        CallCardPlayed(); //This function is used as a debug tool remove later after card activates the next numberup.
        //This bool is to keep the player from skipping forward in the text before the drag&drop function is disabled.
        if (_startclicks == true)
        {
            Clicks();
            UpdateText();
        }
    }

    //This function tracks the amount of clicks the player has made.
    private void Clicks()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && _checkClicks == true) { _numberUp++; }
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
            Text("Blue icons represent the character's defense. ", "Left click to continue!");
        }
        else if (_numberUp == 3)
        {
            Text("Your goal is to reduce the enemy's health to 0! ", "Left click to continue!");
        }
        else if (_numberUp == 4)
        {
            //
            _TTC.ResumeShowingUI();
            Text("The bottom row displays all the cards in your hand!" + " " + "Hover your mouse over the card to see more detail!", "Left click when you are ready to continue!");
        }
        else if (_numberUp == 5)
        {
            _TTC.StopShowingUI();
            Text("At the beginning of your turn, draw from the deck until you have 5 cards in your hand.", "Left click to continue!");
        }
        else if (_numberUp == 6)
        {
            Text("At the end of your turn, discard your hand to 5.", "Left click to continue!");
        }
        else if(_numberUp == 7)
        {
            _TTC.ResumeShowingUI();
            _TCDH.DDOn();

            _checkClicks = false;

            HideBackground();
            StartCore();

            Text("Now Let's Play Some Cards!", "Left click and drag a card onto the battlefield!");
        }
        else if(_numberUp == 8)
        {
            _TTC.StopShowingUI();
            _TCDH.DDOff();

            ShowChildTwo();

            Text("Your Opponent reacts to your card! Whenever a player actively plays a card, the other side can counter with the played cards natural counter. When a counter happens, a chain starts.", "Left click to continue! ");
        }
        else if (_numberUp == 9)
        {
            ShowBackGround();
            Text("There are mainly three types of cards in Mobius: Attack, Defense, and Support. They counter each other following this order.", "Left click to continue! ");
        }
        else if (_numberUp == 10)
        {
            HideBackground();
            Text("The first player that chooses to skip the chain will lose in that chain, and only the winner's cards in the chain will take effect.", "Left click to continue! ");
        }
        else if (_numberUp == 11)
        {
            Text("Now you've acquired all the knowledge for this game! Have Fun Playing!", "Left click to continue!");
        }
        else if (_numberUp >= 12) 
        {
            _TTC.ResumeShowingUI();
            _TCDH.DDOn();
            this.gameObject.SetActive(false);
            _startclicks = false;
            Debug.LogWarning("Load next scene");
            //SceneManager.LoadScene("");
        }
    }
    
    //This corutine makes sure all the things we need to turn off do. 
    IEnumerator TurnOffCards()
    {
        yield return new WaitForSeconds(0.5f);
        _TTC.StopShowingUI();
        _TCDH.DDOff();
        _startclicks = true;
        _checkClicks = true;
    }

    IEnumerator HideAllChildren()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.GetComponent<Transform>().GetChild(0).gameObject.SetActive(false);
        this.gameObject.GetComponent<Transform>().GetChild(1).gameObject.SetActive(false);
        this.gameObject.GetComponent<Transform>().GetChild(2).gameObject.SetActive(false);
        StopCoroutine(HideAllChildren());
    }

    //Main function used to update the text boxes on the screen.
    private void Text(string main, string instructions)
    {
        _maintext.text = main.ToString();
        _instructionstext.text = instructions.ToString();
    }

    //Call this function after a card is played, so the text can continue. 
    public void CardPlayed()
    {
        _checkClicks = true;
        _numberUp++;
    }

    //Used for debug purposes to test the cardplayed function. 
    private void CallCardPlayed()
    {
        bool pressed = false;
        if(Input.GetKeyDown(KeyCode.Delete) && pressed == false)
        {
            pressed = true;
            CardPlayed();
            Debug.LogWarning("Delete Pressed");
        }
    }

    //Makes the gray background disappear and makes the white background transparent. 
    void HideBackground()
    {
        _largebackground.SetActive(false);

        var tempAlpha = _textbackground.color;
        tempAlpha.a = 0.6f;
        _textbackground.color = tempAlpha;
    }

    //Shows the gray background again and makes the white background less transparent.
    void ShowBackGround()
    {
        _largebackground.SetActive(true);

        var tempAlpha = _textbackground.color;
        tempAlpha.a = 0.7f;
        _textbackground.color = tempAlpha;
    }

    //This shows the child two of tutorial text and make sure it is only used once by making a bool out of i.
    bool i = false;
    void ShowChildTwo()
    {
        if(i == false) 
        {
            _TextHolder.SetActive(true);
            i = true;
        }
    }

    //Keeps the coroutine from overloading due to the call from the update text function which updates 60 frames a second. 
    bool x = false;
    void StartCore()
    {
        if(x == false)
        {
            StartCoroutine(HideAllChildren());
            x = true;
        }
    }
}