using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this class mainly lerps the card chain UP in the battleground
//so that the player can see all the cards in the chain.
[RequireComponent(typeof(RectTransform))]
public class CardChainUI : MonoBehaviour
{
    //These vars are for Lerping the list
    [SerializeField] RectTransform rect;//move this transform
    float lerpLength = 1f;
    float timePassedSinceLerping = 0f;
    float eachLerpSize = 8.0f; //bottom Y = -30, top Y = 0, lerp from card 4th -> 10th = 7 times
    Vector3 targetLerpingPosition;
    Vector3 startLerpingPosition;
    Vector3 orininalPosition; //when chain finishes, reset back to original position, reset all values.
    bool isLerping = false;//when lerping, switch this

    //Variables used for calculating functionality to the chain's flexbility.
    float fSwitchCardPosX = -4.9f;
    float fAlphaReduction = 1.0f;
    float fAlphaIncrease = 0.0f;

    //Determines what to do for Scrolling.
    bool bGoingUp = false;
    bool bManualScroll = false;

    //This variable is meant to be a switch. 1 = Scrolling Down, 2 = Scrolling Up
    short sPriorFlip = 0;

    short sIncreasePace = 4; // Never should be 0, increasing it higher speeds up various markers such as LERPing and Fade In/Fade Out for Cards.

    //Tracks the most recently played Card and Oldest Card still visible on screen.
    [SerializeField] int nRecentCard;
    [SerializeField] int nNextOldestCard;

    //Temporary Bool for Debug Purposes Only
    bool bChainManualReset = false;
    public bool bDEBUGENABLE = false;

    //These vars are for the mechanics and logics of adding cards to the list
    [SerializeField] GameObject CardImages; //place holder for card images. Not affect mechanics.
    [SerializeField] List<Image> cardsInChain;//fixed list of 10 images.
    private const int MaxWithoutLerping = 3;
    public int faceUpcardsPlayed = 0; //current number of cards played face up
    public Image uCardTemplate;
    //private const int MAX_CARDS_ALLOWED = 10; //maximum 10 cards in chain.

    private void Start()
    {
        FindVariables();
        if (bDEBUGENABLE)
            cardsInChain[faceUpcardsPlayed].GetComponent<Image>().color = new Color(1, 1, 1, 1.0f);
    }

    ///*********************************************************************///
    /// Function: InitiateCardImage                                         ///
    ///                                                                     ///
    /// Description: Used to take a card template (provided in the prefab   ///
    ///             as Card Template) and duplicates it whenever we add a   ///
    ///             new card to the ongoing chain.                          ///
    ///                                                                     ///
    ///     Date Created: 1/3/21                                            ///
    ///     Date Updated: 2/05/22                                           ///
    ///                                                                     ///
    ///     Author: Jordan R. Douglas                                       ///
    ///*********************************************************************///
    public void InitiateCardImage(Card card)
    {
        Image uNewCard = Instantiate<Image>(uCardTemplate);
        uNewCard.transform.SetParent(CardImages.transform, false);
        uNewCard.transform.position = cardsInChain[faceUpcardsPlayed - 1].transform.position - new Vector3(fSwitchCardPosX, 0.0f, 8.0f);
        fSwitchCardPosX = fSwitchCardPosX * -1;
        FindVariables();
        cardsInChain[faceUpcardsPlayed - 1].sprite = card.GetFrontImage();
        cardsInChain[faceUpcardsPlayed - 1].gameObject.SetActive(true);
        if (!bDEBUGENABLE)
            cardsInChain[faceUpcardsPlayed - 1].GetComponent<Image>().color = new Color(1, 1, 1, 1.0f);
        else
            cardsInChain[faceUpcardsPlayed].GetComponent<Image>().color = new Color(1, 1, 1, 1.0f);
    }

    ///*********************************************************************///
    /// Function: HideCardImage                                             ///
    ///                                                                     ///
    /// Description: This will hide a Card based on the number given        ///
    ///             (its position/play in the current chain).               ///
    ///                                                                     ///
    ///     Date Created: 1/12/22                                           ///
    ///     Date Updated: 2/06/22                                           ///
    ///                                                                     ///
    ///     Author: Jordan R. Douglas                                       ///
    ///*********************************************************************///
    private void HideCardImage(int nCard)
    {
        if (cardsInChain[nCard].GetComponent<Image>().color.a > 0.0f)
        {
            fAlphaReduction -= Time.deltaTime * sIncreasePace;
            cardsInChain[nCard].GetComponent<Image>().color = new Color(1, 1, 1, fAlphaReduction);
        }
        else
            fAlphaReduction = 0.0f;
    }

    ///*********************************************************************///
    /// Function: RevealCardImage                                           ///
    ///                                                                     ///
    /// Description: This will reveal a Card based on the number given      ///
    ///             (its position/play in the current chain). Will not be   ///
    ///             called during normal play, only when the Player chooses ///
    ///             to manually scroll through the Card Chain.              ///
    ///                                                                     ///
    ///     Date Created: 2/04/22                                           ///
    ///     Date Updated: 2/06/22                                           ///
    ///                                                                     ///
    ///     Author: Jordan R. Douglas                                       ///
    ///*********************************************************************///
    private void RevealCardImage(int nCard)
    {
        if (!bDEBUGENABLE)
        {
           if (cardsInChain[nCard].GetComponent<Image>().sprite == null)
            {
                fAlphaIncrease = 1.0f;
                return;
            }
        }
        if (cardsInChain[nCard].GetComponent<Image>().color.a < 1.0f)
        {
            fAlphaIncrease += Time.deltaTime * sIncreasePace;
            cardsInChain[nCard].GetComponent<Image>().color = new Color(1, 1, 1, fAlphaIncrease);
        }
        else
            fAlphaIncrease = 1.0f;
    }

    ///*********************************************************************///
    /// Function: ReturnToRecent                                            ///
    ///                                                                     ///
    /// Description: Use to reset the Card Chain's position back to what    ///
    ///             it should be with the most recently played card.        ///
    ///             Effectively resets scroll position back to most         ///
    ///             recently played card. Should not be called unless a     ///
    ///             new Card is played and the Chain View is not focused    ///
    ///             on the most recent card.                                ///
    ///                                                                     ///
    ///     Date Created: 2/05/22                                           ///
    ///     Date Updated: 2/06/22                                           ///
    ///                                                                     ///
    ///     Author: Jordan R. Douglas                                       ///
    ///*********************************************************************///
    private void ReturnToRecent()
    {
        while (nRecentCard != faceUpcardsPlayed)
        {
            //Determine if we start Lerping or Continue the Current Lerp.
            if (!isLerping)
                SetupLerping(true);
            else
            {
                timePassedSinceLerping += Time.deltaTime;
                LerpUI(true);
            }

            //Once Oldest Card has disappeared, increment Card Trackers and let the
            //loop determine if it continues or breaks.
            if (timePassedSinceLerping > lerpLength && cardsInChain[nRecentCard].GetComponent<Image>().color.a >= 1.0f)
            {
                nNextOldestCard++;
                nRecentCard++;
            }
        }
    }

    ///*********************************************************************///
    /// Function: CardScrollUp                                              ///
    ///                                                                     ///
    /// Description: Scrolls the card chain upwards until it reaches the    ///
    ///             bottom of the chain.                                    ///
    ///                                                                     ///
    ///     Date Created: 2/07/22                                           ///
    ///     Date Updated: 2/07/22                                           ///
    ///                                                                     ///
    ///     Author: Jordan R. Douglas                                       ///
    ///*********************************************************************///
    public void CardScrollUp()
    {
        if (!isLerping && nRecentCard != faceUpcardsPlayed + 1)
        {
            if (!bManualScroll)
            {
                return;
            }

            //If beyond the highest point in the chain when activating, take a step forward in the chain immediately.
            //if (nRecentCard > faceUpcardsPlayed - 1)
            //{
            //    nNextOldestCard = faceUpcardsPlayed - 7;
            //    nRecentCard = faceUpcardsPlayed - 1;
            //}

            if (nNextOldestCard < 0)
            {
                nNextOldestCard = 0;
                nRecentCard = 6;
            }
            else if (sPriorFlip == 2)
            {
                nNextOldestCard++;
                nRecentCard++;
            }

            sPriorFlip = 1;

            //Cards will Lerp Upwards to show newer cards in the Chain.
            SetupLerping(true);
            bManualScroll = true;
        }
    }

    ///*********************************************************************///
    /// Function: CardScrollDown                                            ///
    ///                                                                     ///
    /// Description: Scrolls the card chain downwards until it reaches the  ///
    ///             top of the chain.                                       ///
    ///                                                                     ///
    ///     Date Created: 2/07/22                                           ///
    ///     Date Updated: 2/07/22                                           ///
    ///                                                                     ///
    ///     Author: Jordan R. Douglas                                       ///
    ///*********************************************************************///
    public void CardScrollDown()
    {
        //If we are beyond the very bottom of the chain when activating, take a step back in the chain immediately
        if (!isLerping && nNextOldestCard >= 0)
        {
            if (nRecentCard > faceUpcardsPlayed)
            {
                nNextOldestCard = faceUpcardsPlayed - 6;
                nRecentCard = faceUpcardsPlayed;
            }
            else if (sPriorFlip == 1)
            {
                nNextOldestCard--;
                nRecentCard--;
            }

            sPriorFlip = 2;

            //Cards will Lerp Downwards to show older cards in the Chain.
            SetupLerping(false);
            bManualScroll = true;
        }
    }

    ///*********************************************************************///
    /// Function: CardScroll                                                ///
    ///                                                                     ///
    /// Description: Used to determine how scrolling through the chain will ///
    ///             work. It has two core parts, one that determines going  ///
    ///             up, the other determines going down.                    ///
    ///                                                                     ///
    ///     Date Created: 2/04/22                                           ///
    ///     Date Updated: 2/06/22                                           ///
    ///                                                                     ///
    ///     Author: Jordan R. Douglas                                       ///
    ///*********************************************************************///
    private void CardScroll()
    {
        //Chain cannot be scrolled upwards any farther once the oldest card (first in chain) is reached.
        if (Input.GetKeyDown(KeyCode.L) || Input.mouseScrollDelta.y > 0.1f)
        {
            CardScrollUp();
        }

        //Chain cannot be scrolled downwards any farther once the newest card (most recently played) is reached.
        else if (Input.GetKeyDown(KeyCode.Period) || Input.mouseScrollDelta.y <= -0.1f)
        {
            CardScrollDown();
        }
    }

    private void FindVariables()
    {
        rect = GetComponent<RectTransform>();
        if (rect is null)
        {
            Debug.Log("Missing rect transform in " + name);
        }

        if (cardsInChain.Count <= 0)
        {
            targetLerpingPosition = rect.localPosition;
            orininalPosition = rect.localPosition;
        }

        cardsInChain = new List<Image>();
        foreach (Transform child in CardImages.transform)
        {
            cardsInChain.Add(child.GetComponent<Image>());
            //child.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        TestLerp();
        LerpUI(bGoingUp);
        timePassedSinceLerping += Time.deltaTime * sIncreasePace;
        if (faceUpcardsPlayed > 5)
        {
            CardScroll();
        }
    }



    #region MECHANICS

    //return false if cannot play the card.
    //either because not implemented, or max cards reached.
    //return true if can play
    public bool PlayCardUI(Card card, bool isPlayer = true, bool isPlayedFaceUp = true)
    {
        if (bManualScroll)
        {
            bManualScroll = false;
        }

        //checking validity
        if (!isPlayedFaceUp)
        {
            //handle playing trap card here
            //NOTE: playing trap card is handled in TrapCardManager.cs, this is no longer required.
            Debug.Log("playing card face down is not handled yet.");
            return false;
        }
        faceUpcardsPlayed++;
        sPriorFlip = 0;

        //all is valid.

        //improvement: if player starts the turn, the first collumn is placed right side.

        InitiateCardImage(card);


        //lerping or not?
        if (faceUpcardsPlayed > MaxWithoutLerping)
        {
            // If LERPing from here, is the chain currently at the most recent point prior to the newest card?
            if (nRecentCard != faceUpcardsPlayed - 1)
            {
                //If not, run a loop to reach the most recently played card prior to the newest one.
                if (nNextOldestCard < 0)
                {
                    nNextOldestCard = 0;
                    nRecentCard = 6;
                }
                ReturnToRecent();
            }
            SetupLerping(true);
        }

        nNextOldestCard = faceUpcardsPlayed - 6;
        nRecentCard = faceUpcardsPlayed;

        return true;
    }


    //CardChain.cs call this to reset the UI when new chain starts
    public void ResetChainUI()
    {
        for (int i = 1; i < cardsInChain.Count; i++)
        {
            cardsInChain[i].sprite = null;
            Destroy(cardsInChain[i].gameObject);
            cardsInChain.RemoveAt(i);
            i--;
        }
        ResetLerpingChain();
        faceUpcardsPlayed = 0;
        cardsInChain[0].sprite = null;
        if (!bDEBUGENABLE)
            cardsInChain[0].color = new Color(1, 1, 1, 0.0f);
        else
            cardsInChain[0].color = new Color(1, 1, 1, 1.0f);
        fSwitchCardPosX = -4.9f;

        bChainManualReset = false;
    }

    #endregion

    #region LERP_STUFF

    private void TestLerp()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            bChainManualReset = true;
            ResetLerpingChain();
        }
    }

    //other class calls this function to reset the chain to origial position
    //reset all other variables too
    private void ResetLerpingChain()
    {
        timePassedSinceLerping = 0f;
        rect.localPosition = orininalPosition;
        targetLerpingPosition = rect.localPosition;
        isLerping = false;
        for (int i = 0; i < cardsInChain.Count; i++)
        {
            cardsInChain[i].color = new Color(1, 1, 1, 1.0f);
        }
        bManualScroll = false;
        nNextOldestCard = faceUpcardsPlayed - 6;
        nRecentCard = faceUpcardsPlayed - 1;
    }

    //other class calls this function to start lerping to a new position.
    private void SetupLerping(bool bIsGoingUp)
    {
        if (bIsGoingUp)
        {
            targetLerpingPosition += eachLerpSize * Vector3.up;
            startLerpingPosition = rect.localPosition;
            isLerping = true;   //lerp starts
            timePassedSinceLerping = 0f;

            bGoingUp = true;
        }
        else if (!bIsGoingUp)
        {
            targetLerpingPosition -= eachLerpSize * Vector3.up;
            startLerpingPosition = rect.localPosition;
            isLerping = true;   //lerp starts
            timePassedSinceLerping = 0f;

            bGoingUp = false;
        }
    }

    ///*********************************************************************///
    /// Function: ManualLERPScroll                                          ///
    ///                                                                     ///
    /// Description: Determines if we incremate/decrease the currently      ///
    ///             tracked card #s ONLY when Manually Scrolling.           ///
    ///             Otherwise, it is to be handled elsewhere in the card    ///
    ///             playing process.                                        ///
    ///                                                                     ///
    ///     Date Created: 2/06/22                                           ///
    ///     Date Updated: 2/06/22                                           ///
    ///                                                                     ///
    ///     Author: Jordan R. Douglas                                       ///
    ///*********************************************************************///
    private void ManualLERPScroll(bool bIsGoingUp)
    {
        if (bManualScroll)
        {
            if (bIsGoingUp)
            {
                nNextOldestCard++;
                nRecentCard++;
            }
            else
            {
                nNextOldestCard--;
                nRecentCard--;
            }
        }
    }

    //lerp from start to target position in 1 second
    private void LerpUI(bool bIsGoingUp)
    {
        if (!isLerping)
        {
            return;
        }
        if (timePassedSinceLerping > lerpLength && (fAlphaIncrease >= 1.0f || fAlphaReduction <= 0.0f))//lerp ends
        {
            fAlphaReduction = 1.0f;
            fAlphaIncrease = 0.0f;

            ManualLERPScroll(bIsGoingUp);

            isLerping = false;
            return;
        }

        if (faceUpcardsPlayed > 5)
        {
            if (bIsGoingUp)
            {
                //Reveal the next lowest card of the Chain.
                RevealCardImage(nRecentCard);

                //Hide the current oldest card present in the chain.
                HideCardImage(nNextOldestCard);
            }
            else
            {
                //Reveal the next old card in the Chain.
                RevealCardImage(nNextOldestCard);

                //Hide the lowest card of the Chain.
                HideCardImage(nRecentCard);
            }
        }
        float fractionOfJourney = (timePassedSinceLerping / lerpLength) * sIncreasePace;

        rect.localPosition = Vector3.Lerp(startLerpingPosition, targetLerpingPosition, fractionOfJourney);
    }

    #endregion
}
