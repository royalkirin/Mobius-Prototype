using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

///*****************************************************************************************///
/// Class: MainMenu                                                                         ///
///                                                                                         ///
/// Description: Controls the button funcionalities of the Main Menu.                       ///
///                                                                                         ///
///     Date Created: 3/20/22                                                               ///
///     Date Updated: 3/22/22                                                               ///
///                                                                                         ///
///     Author: Jordan R. Douglas                                                           ///
///*****************************************************************************************///
public class MainMenu : MonoBehaviour
{
    #region UNITY_VARIABLES
    GameObject uStartGameBtn;
    GameObject uSettingsBtn;
    GameObject uCreditsBtn;
    GameObject uQuitGameBtn;

    //Level Buttons
    Button uTutorial;
    Button uLevelOne;
    Button uLevelTwo;

    GameObject uReturnBtn;
    public Sprite[] uReturnGraphics;

    GameObject uTitleScreen;
    GameObject uLevelSelect;
    GameObject uCreditScreen;
    GameObject uSettingsMenu;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //Initialize all Buttons and Backgrounds
        InitializeMenuButtons();
        InitializeBackgrounds();
        InitializeLevelSelectButtons();

        //Enable and Disable what needs to be at game launch
        TitleScreenActive(true);
    }

    void InitializeMenuButtons()
    {
        uStartGameBtn = GameObject.Find("Start Game Button");
        if (uStartGameBtn)
            uStartGameBtn.GetComponent<Button>().onClick.AddListener(StartButtonFunction);
        else
            Debug.Log("Error with Start Button");

        uSettingsBtn = GameObject.Find("Settings Button");
        if (uSettingsBtn)
            uSettingsBtn.GetComponent<Button>().onClick.AddListener(SettingsButtonFunction);
        else
            Debug.Log("Error with Settings Button");

        uCreditsBtn = GameObject.Find("Credits Button");
        if (uCreditsBtn)
            uCreditsBtn.GetComponent<Button>().onClick.AddListener(CreditsButtonFunction);
        else
            Debug.Log("Error with Credits Button");

        uQuitGameBtn = GameObject.Find("Quit Button");
        if (uQuitGameBtn)
            uQuitGameBtn.GetComponent<Button>().onClick.AddListener(QuitButtonFunction);
        else
            Debug.Log("Error with Quit Button");

        uReturnBtn = GameObject.Find("Return Button");
        if (uReturnBtn)
            uReturnBtn.GetComponent<Button>().onClick.AddListener(ReturnButtonFunction);
        else
            Debug.Log("Error with Return Button");
    }

    void InitializeBackgrounds()
    {
        uTitleScreen = GameObject.Find("Title Screen");
        if (!uTitleScreen)
            Debug.Log("Error with Title Screen");

        uSettingsMenu = GameObject.Find("ConfigureSettingsBackground");
        if (!uSettingsMenu)
            Debug.Log("Error with Configure Settings Background");

        uLevelSelect = GameObject.Find("Level Select");
        if (!uLevelSelect)
            Debug.Log("Error with Level Select");

        uCreditScreen = GameObject.Find("Credits Screen");
        if (!uCreditScreen)
            Debug.Log("Error with Credits Screen");
    }

    void InitializeLevelSelectButtons()
    {
        uTutorial = GameObject.Find("TUTORIAL").GetComponent<Button>();
        uTutorial.onClick.AddListener(() => SelectButtonFunction(1));
        
        uLevelOne = GameObject.Find("LEVEL 1").GetComponent<Button>();
        uLevelOne.onClick.AddListener(() => SelectButtonFunction(2));

        uLevelTwo = GameObject.Find("LEVEL 2").GetComponent<Button>();
        uLevelTwo.onClick.AddListener(() => SelectButtonFunction(3));
    }

    #region ActiveToggleFunctions
    void TitleScreenActive(bool isActive)
    {
        uStartGameBtn.SetActive(isActive);
        uSettingsBtn.SetActive(isActive);
        uCreditsBtn.SetActive(isActive);
        uQuitGameBtn.SetActive(isActive);

        if (isActive)
        {
            SettingScreenToggle(false);
            CreditsScreenToggle(false);
            LevelSelectScreenToggle(false);
            ReturnToggle(false, 0);
        }
    }

    void SettingScreenToggle(bool isActive)
    {
        uSettingsMenu.SetActive(isActive);
        ReturnToggle(isActive, 1);
    }

    void CreditsScreenToggle(bool isActive)
    {
        uCreditScreen.SetActive(isActive);
        ReturnToggle(isActive, 0);
        
    }

    void LevelSelectScreenToggle(bool isActive)
    {
        uLevelSelect.SetActive(isActive);
        ReturnToggle(isActive, 0);
    }

    void ReturnToggle(bool isActive, int nReturnGraphic)
    {
        uReturnBtn.SetActive(isActive);
        uReturnBtn.GetComponent<Image>().sprite = uReturnGraphics[nReturnGraphic];
    }
    #endregion

    #region BUTTON_FUNCTIONS
    void StartButtonFunction()
    {
        TitleScreenActive(false);
        LevelSelectScreenToggle(true);
        uReturnBtn.GetComponent<RectTransform>().anchoredPosition = new Vector3(-475.0f, -300.0f, 0.0f);
    }

    void SettingsButtonFunction()
    {
        //Leads to the Configure Settings Menu.
        TitleScreenActive(false);
        SettingScreenToggle(true);
        uReturnBtn.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.0f, -150.0f, 0.0f);
    }

    void CreditsButtonFunction()
    {
        //Leads to the Credits Showcase
        TitleScreenActive(false);
        CreditsScreenToggle(true);
        uReturnBtn.GetComponent<RectTransform>().anchoredPosition = new Vector3(-475.0f, -300.0f, 0.0f);
    }

    void QuitButtonFunction()
    {
        //Quit Game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SelectButtonFunction(int nLevelID)
    {
        //Select a Level to Play
        SceneManager.LoadScene(nLevelID);
    }

    void ReturnButtonFunction()
    {
        //Return to the Title Screen Options
        TitleScreenActive(true);
    }
    #endregion
}
