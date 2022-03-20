using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to hide and show the backgrounds when discussing different elements in the tutorial. 
public class TutorialBackgroundManager : MonoBehaviour
{
    [Header("References")]
    public GameObject _battleCycle;
    [SerializeField] GameObject _ShieldHighlight = null;
    [SerializeField] GameObject _HealthHighlight = null;
    [SerializeField] GameObject _CardHighlight = null;
    [SerializeField] GameObject _EnemyHealthHighlight = null;
    [SerializeField] GameObject _TutorialTextBackground = null;

    private void Start()
    {
        //Tests if the references are set, if not returns a warning. 
        #region Checks
        if (_ShieldHighlight is null)
            Debug.LogWarning("_ShieldHighlight is null");
        else if (_battleCycle is null)
            Debug.LogWarning("_battleCycle is null");
        else if (_HealthHighlight is null)
            Debug.LogWarning("_HealthHighlight is null");
        else if (_CardHighlight is null)
            Debug.LogWarning("_CardHighlight is null");
        else if (_EnemyHealthHighlight is null)
            Debug.LogWarning("_EnemyHealthHighlight is null");
        else if (_TutorialTextBackground is null)
            Debug.LogWarning("_TutorialTextBackground is null");
        #endregion
    }

    //Hide & Show functions for the Shield Background.
    #region ShieldBackground
    //Hides the gray background that appears when the tutorial talks about the Shield.
    public void HideShieldBackground()
    {
        _ShieldHighlight.SetActive(false);
    }

    //Shows the gray background that appears when the tutorial talks about the Shield.
    public void ShowShieldBackground()
    {
        _ShieldHighlight.SetActive(true);
    }
    #endregion

    //Hide & Show functions for the Health Background.
    #region HealthBackground
    //Hides the gray background that appears when the tutorial talks about the health.
    public void HideHealthBackground()
    {
        _HealthHighlight.SetActive(false);
    }

    //Shows the gray background that appears when the tutorial talks about the health.
    public void ShowHealthBackground()
    {
        _HealthHighlight.SetActive(true);
    }
    #endregion
    
    //Hide & Show functions for the Card Background.
    #region CardBackground
    //Hides the gray background that appears when the tutorial talks about the cards.  
    public void HideCardBackground()
    {
        _CardHighlight.SetActive(false);
    }

    //Shows the gray background that appears when the tutorial talks about the cards.  
    public void ShowCardBackground()
    {
        _CardHighlight.SetActive(true);
    }
    #endregion

    //Hide & Show functions for the text Background.
    #region TextBackground
    //Hides the gray background that appears behind the text.  
    public void HideTutorialTextBackground()
    {
        _TutorialTextBackground.SetActive(false);
    }

    //Shows the gray background that behind the text.  
    public void ShowTutorialTextBackground()
    {
        _TutorialTextBackground.SetActive(true);
    }

    #endregion

    ///Need to add functions for the enemies health bar. 
    ///
    #region EnemyHealthBarBackground
    public void ShowEnemyHealthBar()
    {
        _EnemyHealthHighlight.SetActive(true);
    }

    public void HideEnemyHealthBar()
    {
        _EnemyHealthHighlight.SetActive(false);
    }
    #endregion
}
