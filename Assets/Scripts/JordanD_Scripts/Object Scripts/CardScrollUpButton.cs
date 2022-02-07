using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///*****************************************************************************************///
/// Class: CardScrollButtons                                                                ///
///                                                                                         ///
/// Description: Sets the Scroll Down button for the Card Chain.                            ///
///                                                                                         ///
///     Date Created: 2/07/22                                                               ///
///     Date Updated: 2/07/22                                                               ///
///                                                                                         ///
///     Author: Jordan R. Douglas                                                           ///
///*****************************************************************************************///
public class CardScrollUpButton : MonoBehaviour
{
    CardChainUI cCardChainUI;
    Button uButton;

    ///*********************************************************************///
    /// Function: SetScrollingButton                                        ///
    ///                                                                     ///
    /// Description: Sets the scrolling button for use.                     ///
    ///                                                                     ///
    ///     Date Created: 2/07/22                                           ///
    ///     Date Updated: 2/07/22                                           ///
    ///                                                                     ///
    ///     Author: Jordan R. Douglas                                       ///
    ///*********************************************************************///
    void SetScrollingButton()
    {
        cCardChainUI = FindObjectOfType<CardChainUI>();
        if (cCardChainUI == null)
        {
            Debug.Log("Error! The Card Chain UI could not be found for the Scroll Buttons.");
        }
        else
        {
            uButton = GetComponent<Button>();
            uButton.onClick.AddListener(ScrollUpButtonClick);
        }
    }

    ///*********************************************************************///
    /// Function: ScrollUpButtonClick                                       ///
    ///                                                                     ///
    /// Description: Calls from the Card Chain UI to scroll the Card Chain  ///
    ///             appropriately.                                          ///
    ///                                                                     ///
    ///     Date Created: 2/07/22                                           ///
    ///     Date Updated: 2/07/22                                           ///
    ///                                                                     ///
    ///     Author: Jordan R. Douglas                                       ///
    ///*********************************************************************///
    void ScrollUpButtonClick()
    {
        cCardChainUI.CardScrollDown();
    }

    void Start()
    {
        SetScrollingButton();
    }
}
