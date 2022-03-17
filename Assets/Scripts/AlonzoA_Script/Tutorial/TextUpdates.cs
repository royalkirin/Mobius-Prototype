using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//The scripting in charge of making sure the text is updated correctly. 
public class TextUpdates : MonoBehaviour
{
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

    //[SerializeField]
    int _numberUp = 0;
    //[SerializeField]
    bool _startclicks = false;

    // _checkClicks is the bool that should be called by other scripts. 
    [HideInInspector] public bool _checkClicks = false;
    #endregion

    void Start()
    {
        _TTC = GameObject.FindObjectOfType<TooltipCaller>();
        _TCDH = GameObject.FindObjectOfType<CardDropHandler>();
        _TBM = this.GetComponent<TutorialBackgroundManager>();

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

        //Makes sure the background HUD and text objects are on even if they are deactivated. 
        _TBM.ShowTutorialTextBackground();
        this.transform.GetChild(6).gameObject.SetActive(true);

        //Shows the first text.
        Text("Welcome to the World of Mobius!", "Left click to continue!");
        UpdateText();
    }

    void Update()
    {
        if (_startclicks == true)
        {
            Clicks();
        }
    }

    //This function tracks the amount of clicks the player has made.
    private void Clicks()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && _checkClicks == true) { _numberUp++; UpdateText(); }
    }

    //This function is what updates the text each time the mouse button is clicked.
    private void UpdateText()
    {
        if (_numberUp == 1)
        {
            _TBM.HideTutorialTextBackground();
            _TBM.ShowHealthBackground();
            Text("Lets Get Started!" + " " + "Green icons represent the character's Health. ", "Left click to continue!");
        }
        else if (_numberUp == 2)
        {
            _TBM.HideHealthBackground();
            _TBM.ShowShieldBackground();
            Text("Blue icons represent the character's defense. ", "Left click to continue!");
        }
        else if (_numberUp == 3)
        {
            _TBM.HideShieldBackground();
            _TBM.ShowEnemyHealthBar();
            Text("Your goal is to reduce the enemy's health to 0! ", "Left click to continue!");
        }
        else if (_numberUp == 4)
        {
            _TBM.HideEnemyHealthBar();
            _TBM.ShowCardBackground();
            _TTC.ResumeShowingUI();
            Text("The bottom row displays all the cards in your hand!" + " " + "Hover your mouse over the card to see more detail!", "Left click when you are ready to continue!");
        }
        else if (_numberUp == 5)
        {
            _TBM.HideCardBackground();
            _TBM.ShowTutorialTextBackground();
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

            _TBM.HideTutorialTextBackground();
            StartCoroutine(HideAllChildren()); //Hides all children

            Text("Now Let's Play Some Cards!", "Left click and drag a card onto the battlefield!");
        }
        else if(_numberUp == 8)
        {
            StartCoroutine(HideUI());
            _TCDH.DDOff();
            
            ShowTutorialText();

            Text("Your Opponent reacts to your card! Whenever a player actively plays a card, the other side can counter with the played cards natural counter. When a counter happens, a chain starts.", "Left click to continue! ");
        }
        else if (_numberUp == 9)
        {
            _TBM.ShowTutorialTextBackground();
            Text("There are mainly three types of cards in Mobius: Attack, Defense, and Support. They counter each other following this order.", "Left click to continue! ");
        }
        else if (_numberUp == 10)
        {
            _TBM.HideTutorialTextBackground();
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
        }
    }

    //This houses all the code for background and hiding things. 
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

    //Used to combat an issue with playing the card in the tutorial then resuming the text. My guess is ShowUI is called after a card is played which overrided my previous method of use. This wait should be enough to turn it back off again. 
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
    #endregion  //This houses all the code for background and hiding things. 
}