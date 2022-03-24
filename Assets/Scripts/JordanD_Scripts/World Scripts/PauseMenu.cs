using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

///*****************************************************************************************///
/// Class: PauseMenu                                                                        ///
///                                                                                         ///
/// Description: Controls the Pause Menu functionality.                                     ///
///                                                                                         ///
///     Date Created: 3/03/22                                                               ///
///     Date Updated: 3/19/22                                                               ///
///                                                                                         ///
///     Author: Jordan R. Douglas                                                           ///
///*****************************************************************************************///
public class PauseMenu : MonoBehaviour
{
    float fCurrentTime;

    //The Pause Button
    GameObject uPauseButton;

    //The Pause Menu
    GameObject uPauseBackground;
    GameObject uQuitButton;
    GameObject uResumeButton;
    GameObject uSettingsButton;

    //The Configuration Options
    GameObject uSettingsBackground;
    GameObject uMasterSlider;
    GameObject uMusicSlider;
    GameObject uSoundFXSlider;
    GameObject uLeaveSettingsButton;

    //The Confirmation for Quitting
    GameObject uQuitBackground;
    GameObject uExitConfirmButton;
    GameObject uDoNotExitButton;

    // Start is called before the first frame update
    void Start()
    {
        SetBackgrounds();
        SetButtons();
        SetSliders();
        ManagePauseActiveState(false);
        ManageQuitYesNoButtons(false);
        ManageSliderActiveState(false);
        float varTstx = Screen.width / 2;
        float varTsty = Screen.height / 2;
        uPauseBackground.transform.position = new Vector3(varTstx, varTsty, 0.0f);
    }

    void SetBackgrounds()
    {
        uPauseBackground = GameObject.Find("PauseMenuBackground");
        uSettingsBackground = GameObject.Find("ConfigureSettingsBackground");
        uQuitBackground = GameObject.Find("QuitMenuBackground");
    }

    void SetButtons()
    {
        uPauseButton = GameObject.Find("Pause Button");
        if (uPauseButton)
            uPauseButton.GetComponent<Button>().onClick.AddListener(PauseGame);

        uResumeButton = GameObject.Find("Pause Resume");
        if (uResumeButton)
            uResumeButton.GetComponent<Button>().onClick.AddListener(ResumeGame);

        uSettingsButton = GameObject.Find("Pause Settings");
        if (uSettingsButton)
            uSettingsButton.GetComponent<Button>().onClick.AddListener(ConfigureSettings);

        uQuitButton = GameObject.Find("Pause Quit");
        if (uQuitButton)
            uQuitButton.GetComponent<Button>().onClick.AddListener(QuitScene);

        uExitConfirmButton = GameObject.Find("Confirm Exit");
        if (uExitConfirmButton)
            uExitConfirmButton.GetComponent<Button>().onClick.AddListener(ExitGame);

        uDoNotExitButton = GameObject.Find("Cancel Exit");
        if (uDoNotExitButton)
            uDoNotExitButton.GetComponent<Button>().onClick.AddListener(ReturnFromQuit);
    }

    void SetSliders()
    {
        uMasterSlider = GameObject.Find("Master Volume");
        if (uMasterSlider)
            uMasterSlider.GetComponent<Slider>().onValueChanged.AddListener(ChangeMasterVolume);

        uMasterSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Master Volume");

        uMusicSlider = GameObject.Find("Music Volume");
        uSoundFXSlider = GameObject.Find("Sound Effect Volume");

        uLeaveSettingsButton = GameObject.Find("Return From Settings");
        if (uLeaveSettingsButton)
            uLeaveSettingsButton.GetComponent<Button>().onClick.AddListener(ReturnFromQuit);
    }

    #region PauseMenu State Controller
    void ManagePauseActiveState(bool isActive)
    {
        uPauseButton.SetActive(!isActive);
        uPauseBackground.SetActive(isActive);

        if (isActive)
        {
            ManagePauseMenuButtons(true);
        }
    }

    void ManagePauseMenuButtons(bool isActive)
    {
        if (isActive)
        {
            ManageQuitYesNoButtons(false);
            ManageSliderActiveState(false);
        }
        else
        {
            uPauseBackground.SetActive(false);
        }
    }

    void ManageSliderActiveState(bool isActive)
    {
        uSettingsBackground.SetActive(isActive);
    }

    void ManageQuitYesNoButtons(bool isActive)
    {
        uQuitBackground.SetActive(isActive);
    }
    #endregion

    #region Pause and Unpause
    void PauseGame()
    {
        fCurrentTime = Time.timeScale;
        Time.timeScale = 0.0f;
        ManagePauseActiveState(true);
    }

    void ResumeGame()
    {
        ManagePauseActiveState(false);
        Time.timeScale = fCurrentTime;
    }
    #endregion

    #region SETTINGS Manager
    void ConfigureSettings()
    {
        ManagePauseMenuButtons(false);
        ManageSliderActiveState(true);
    }

    void ChangeMasterVolume(float fVolumeLevel)
    {
        AudioListener.volume = fVolumeLevel;
    }

    void ChangeMusicVolume(float fVolumeLevel)
    {
        //No Audio Mixer Implemented yet, go to ChangeMasterVolume
        ChangeMasterVolume(fVolumeLevel);
    }

    void ChangeSoundFXVolume(float fVolumeLevel)
    {
        //No Audio Mixer Implemented yet, go to ChangeMasterVolume
        ChangeMasterVolume(fVolumeLevel);
    }
    #endregion

    #region QUIT Mangager
    void QuitScene()
    {
        ManagePauseMenuButtons(false);
        ManageQuitYesNoButtons(true);
    }

    void ReturnFromQuit()
    {
        ManagePauseActiveState(true);
    }

    public void ExitGame()
    {
        //Scene 0 should always be the Main Menu
        SceneManager.LoadScene(0);
    }
    #endregion
}
