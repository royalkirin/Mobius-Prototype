using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This script is in-charge of the tutorial text. It has many moving parts and each section will be explained.
/// </summary>
public class TextUpdates : MonoBehaviour
{
    //This region is for all the variables/references needed to make the tutorial text work. It has many references, some of which are shortened, so the variable name is not too long. 
    #region Variables
    [Header("Text")]
    [SerializeField] TMP_Text _MainText;
    [SerializeField] TMP_Text _InstructionsText;

    [Header("Images")]
    [SerializeField] Image _TextBackground;

    [Header("References")]
    [SerializeField] TooltipCaller _TTC;
    [SerializeField] CardDropHandler _TCDH;
    [SerializeField] TutorialBackgroundManager _TBM;
    [SerializeField] GameObject _TextHolder;
    [SerializeField] PageCount _pageCountScript;
    [SerializeField] TextObjectAlignmentTutorial _textObjectAlignmentTutorial;
    [SerializeField] TextBoxAlignmentTutorial _textBoxAlignmentTutorial;
    [SerializeField] Toggle ToggleTutorialDelay;

    int _numberUp = 0;
    bool _startclicks = false;

    bool _delayClicks = true;
    public bool _turnOffDelay = false;

    // _checkClicks is the bool that should be called by other scripts. 
    [HideInInspector] public bool _checkClicks = false;
    #endregion

    /// <summary>
    /// The start finds the refences for all the different references in the tutorial to make sure they are assigned without need for manual input. 
    /// The checks region is to check if any of the references are not assigned because if that is the case errors could arise. 
    /// After that, it is mostly code used to start the tutorial and get everything to the right place. 
    /// </summary>
    void Start()
    {
        _TTC = GameObject.FindObjectOfType<TooltipCaller>();
        _TCDH = GameObject.FindObjectOfType<CardDropHandler>();
        _TBM = this.GetComponent<TutorialBackgroundManager>();
        _pageCountScript = this.GetComponent<PageCount>();
        _textObjectAlignmentTutorial = GameObject.FindObjectOfType<TextObjectAlignmentTutorial>();
        _textBoxAlignmentTutorial = GameObject.FindObjectOfType<TextBoxAlignmentTutorial>();

        #region Checks
        //Checks to see if the text objects are assigned.
        if (_MainText || _InstructionsText is null)
        {
            Debug.Log("Missing: Main/Instruction Text (Check TextUpdates Script: Lines 12 || 13.)");
        }
        else if (_TextBackground is null)
        {
            Debug.Log("Missing: Text (Check TextUpdates Script: Lines 16.)");
        }
        else if (_TTC || _TCDH is null)
        {
            Debug.Log("Missing: ToolTipCaller/TutorialCardDropHandler (Check TextUpdates Script: Line 22 || 23.)");
        }
        else if (_TBM is null)
        {
            Debug.Log("Missing: Tutorial  (Check TextUpdates Script: Line 24.)");
        }
        #endregion

        StartCoroutine(TurnOffCards());

        //Makes sure the background HUD and text objects are shown even if they are deactivated. 
        _TBM.ShowTutorialTextBackground();
        this.transform.GetChild(5).gameObject.SetActive(true);
        _textBoxAlignmentTutorial.NewPosition();
        _delayClicks = false;

        //Sets the first text that appears in the text boxes.
        Text("Welcome to the World of Mobius! You will be run through a quick tutorial to explain the main mehanics of the game! ", "Left click to continue!");
        UpdateText();
    }

    //This is a simple call, so clicks is detected with almost no delay.
    void Update()
    {
        if (_startclicks == true)
        {
            Clicks();
        }
    }

    //This function tracks the amount of clicks the player has made. Then, it updates the int numberUp and calles the update text function.
    private void Clicks()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0) && _checkClicks == true && _delayClicks == false) 
        { 
            _numberUp++; 
            UpdateText();
            if(_turnOffDelay == false)
            {
                _delayClicks = true;
                if(_numberUp < 12)
                {
                    StartCoroutine(ClickDelay());
                }
            } 
        }
    }

    //This function is what updates the text each time the mouse button is clicked. This is the main part of the script, it is resonsible for many of the functions the tutorial has. It's the brains of the operation. 
    private void UpdateText()
    {
        if (_numberUp == 1)
        {
            _pageCountScript.IncreasePageNumb();
            _TBM.HideTutorialTextBackground();
            _TBM.ShowHealthBackground();
            Text("Lets Get Started!" + " " + "Green icons represent the character's Health. You can see it highlighted on the right. ", "When you are done reading, Left-Click to continue through the rest of the tutorial!");
        }
        else if (_numberUp == 2)
        {
            _textObjectAlignmentTutorial.NewPosition();
            _pageCountScript.IncreasePageNumb();
            _TBM.HideHealthBackground();
            _TBM.ShowShieldBackground();
            Text("Blue icons represent the character's defense. You can see it highlighted on the right above health.", "");
        }
        else if (_numberUp == 3)
        {
            _pageCountScript.IncreasePageNumb();
            _TBM.HideShieldBackground();
            _TBM.ShowEnemyHealthBar();
            Text("Your goal is to reduce the enemy's health to 0! The enemy health is highlighted on the left.", "");
        }
        else if (_numberUp == 4)
        {
            _textBoxAlignmentTutorial.OriginalPos();
            _textObjectAlignmentTutorial.OriginalPos();
            _pageCountScript.IncreasePageNumb();
            _TBM.HideEnemyHealthBar();
            _TBM.ShowCardBackground();
            _TTC.ResumeShowingUI();
            Text("The bottom row displays all the cards in your hand!" + " " + "Hover your mouse over the cards to see more details!", "When done, left-click to continue!");
        }
        else if (_numberUp == 5)
        {
            _textBoxAlignmentTutorial.NewPosition();
            _textObjectAlignmentTutorial.NewPosition();
            _pageCountScript.IncreasePageNumb();
            _TBM.HideCardBackground();
            _TBM.ShowTutorialTextBackground();
            _TTC.StopShowingUI();
            Text("At the beginning of your turn, draw from the deck until you have 5 cards in your hand. (This is done automatically).", "");
        }
        else if (_numberUp == 6)
        {
            _pageCountScript.IncreasePageNumb();
            Text("At the end of your turn, discard your hand to 5. (This is also done automatically).", "");
        }
        else if(_numberUp == 7)
        {
            _textObjectAlignmentTutorial.OriginalPos();
            _pageCountScript.IncreasePageNumb();
            _TTC.ResumeShowingUI();
            _TCDH.DDOn();

            _checkClicks = false;

            _TBM.HideTutorialTextBackground();
            StartCoroutine(HideAllChildren()); //Hides all children

            Text("Now Let's Play Some Cards!", "Left click and drag any card onto the battlefield!");
        }
        else if(_numberUp == 8)
        {
            _textObjectAlignmentTutorial.NewPosition();
            _pageCountScript.IncreasePageNumb();
            StartCoroutine(HideUI());
            _TCDH.DDOff();
            
            ShowTutorialText();

            Text("Your Opponent reacts to your card! Whenever a player plays a card, the enemy can counter with the played cards counter. When a counter happens, a chain starts.", "");
        }
        else if (_numberUp == 9)
        {
            _pageCountScript.IncreasePageNumb();
            _TBM.ShowTutorialTextBackground();
            _TBM._battleCycle.SetActive(true);
            Text("There are mainly three types of cards in Mobius: Attack, Defense, and Support. They counter each other following the order located to right.", "");
        }
        else if (_numberUp == 10)
        {
            _pageCountScript.IncreasePageNumb();
            _TBM.HideTutorialTextBackground();
            Text("The first player that chooses to skip the chain will lose in that chain, and only the winner's cards in the chain will take effect.", "");
        }
        else if (_numberUp == 11)
        {
            _textObjectAlignmentTutorial.OriginalPos();
            _pageCountScript.IncreasePageNumb();
            Text("Now you've a basic understanding of the game! Play through the rest of the tutorial to gain a better understanding! Have fun playing!", "The tutorial is now over!");
        }
        else if (_numberUp >= 12) 
        {
            _TTC.ResumeShowingUI();
            _TCDH.DDOn();
            this.gameObject.SetActive(false);
            _startclicks = false;
        }
    }

    //This houses all the code for background functions. 
    #region BackEnd

    //This corutine is to make sure everything shuts off after the scene loads, so no problems arise of something still running. 
    IEnumerator TurnOffCards()
    {
        yield return new WaitForSeconds(0.5f);
        _TTC.StopShowingUI();
        _TCDH.DDOff();
        _startclicks = true;
        _checkClicks = true;
    }

    //This coroutine is for hiding all the children of the object this script is attached too, so we can play the game without the tutorial getting in the way. 
    IEnumerator HideAllChildren()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        StopCoroutine(HideAllChildren());
    }

    //This function is used to combat an issue that was arising. The comment below explains a little more. 
    ///Used to combat an issue with playing the cards in the tutorial then resuming the text. My guess is ShowUI is called after a card is played which overrided my previous method of use. This wait should be enough to turn it back off again. 
    IEnumerator HideUI()
    {
        yield return new WaitForSeconds(1f);
        _TTC.StopShowingUI();
        StopCoroutine(HideUI());
    }

    //Main function used to update the text boxes on the screen.
    private void Text(string main, string instructions)
    {
        _MainText.text = main.ToString();
        _InstructionsText.text = instructions.ToString();
    }

    //Call this function after a card is played, so the text can continue. 
    public void CardPlayed()
    {
        _checkClicks = true;
        _numberUp++;
        UpdateText();
    }

    //This shows the tutorial text and make sure it is only used once by making a bool out of i.
    bool i = false;
    void ShowTutorialText()
    {
        if(i == false) 
        {
            _TextHolder.SetActive(true);
            i = true;
        }
    }

    IEnumerator ClickDelay()
    {
        yield return new WaitForSeconds(2);
        _delayClicks = false;
        StopCoroutine(ClickDelay());
    }

    public void ToggleDelay()
    {
        if(ToggleTutorialDelay.isOn == false)
        {
            _turnOffDelay = false;
        }
        else if(ToggleTutorialDelay.isOn == true)
        {
            _turnOffDelay = true;
        }
    }
    #endregion  //This houses all the code for background and hiding things. 
}