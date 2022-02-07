using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///*****************************************************************************************///
/// Class: CardScrollUpButton                                                               ///
///                                                                                         ///
/// Description: Sets the Scroll Up button for the Card Chain.                              ///
///                                                                                         ///
///     Date Created: 2/07/22                                                               ///
///     Date Updated: 2/07/22                                                               ///
///                                                                                         ///
///     Author: Jordan R. Douglas                                                           ///
///*****************************************************************************************///
public class CardScrollDownButton : MonoBehaviour
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
            Debug.Log("Error! The Card Chain UI could not be found for the Scroll Button.");
        }
        else
        {
            uButton = GetComponent<Button>();
            uButton.onClick.AddListener(ScrollDownButtonClick);

        }
    }

    ///*********************************************************************///
    /// Function: ScrollDownButtonClick                                     ///
    ///                                                                     ///
    /// Description: Calls from the Card Chain UI to scroll the Card Chain  ///
    ///             appropriately.                                          ///
    ///                                                                     ///
    ///     Date Created: 2/07/22                                           ///
    ///     Date Updated: 2/07/22                                           ///
    ///                                                                     ///
    ///     Author: Jordan R. Douglas                                       ///
    ///*********************************************************************///
    void ScrollDownButtonClick()
    {
        if (cCardChainUI.faceUpcardsPlayed > 5)
            cCardChainUI.CardScrollUp();
    }

    void Start()
    {
        SetScrollingButton();
    }
}
