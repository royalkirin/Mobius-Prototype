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
    float eachLerpSize = 10f; //bottom Y = -30, top Y = 0, lerp from card 4th -> 10th = 7 times
    Vector3 targetLerpingPosition;
    Vector3 startLerpingPosition;
    Vector3 orininalPosition; //when chain finishes, reset back to original position, reset all values.
    bool isLerping = false;//when lerping, switch this
    //int maximumLerpTime = 7;
    //int currentLerpTime = 0;

    //Variables used for calculating functionality to the chain's flexbility.
    float fSwitchCardPosX = -4.9f;
    float fAlphaReduction = 1.0f;

    //Temporary Bool for Debug Purposes Only
    bool bChainManualReset = false;

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
    }

    ///*********************************************************************///
    /// Function: InitiateCardImage                                         ///
    ///                                                                     ///
    /// Description: Used to take a card template (provided in the prefab   ///
    ///             as Card Template) and duplicates it whenever we add a   ///
    ///             new card to the ongoing chain.                          ///
    ///                                                                     ///
    ///     Date Created: 1/3/21                                            ///
    ///     Date Updated: 1/13/22                                           ///
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
    }

    ///*********************************************************************///
    /// Function: HideCardImage                                             ///
    ///                                                                     ///
    /// Description: Once the card chain reaches a certain card amount,     ///
    ///             this function takes effect to hide the oldest card      ///
    ///             still active in the scene (but it does not remove it    ///
    ///             from play).                                             ///
    ///                                                                     ///
    ///     Date Created: 1/12/21                                           ///
    ///     Date Updated: 1/13/22                                           ///
    ///                                                                     ///
    ///     Author: Jordan R. Douglas                                       ///
    ///*********************************************************************///
    private void HideCardImage(int nCard)
    {
        if (cardsInChain[nCard].GetComponent<Image>().color.a > 0.0f)
        {
            fAlphaReduction -= Time.deltaTime;
            cardsInChain[nCard].GetComponent<Image>().color = new Color(1, 1, 1, fAlphaReduction);
        }
        else if (fAlphaReduction <= 0.0f)
        {
            fAlphaReduction = 1.0f;
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
        foreach(Transform child in CardImages.transform)
        {
            cardsInChain.Add(child.GetComponent<Image>());
            //child.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        TestLerp();
        LerpUI();
        timePassedSinceLerping += Time.deltaTime;
        if (faceUpcardsPlayed > 5 && !bChainManualReset)
        {
            HideCardImage(faceUpcardsPlayed - 6);
        }
    }



    #region MECHANICS

    //return false if cannot play the card.
    //either because not implemented, or max cards reached.
    //return true if can play
    public bool PlayCardUI(Card card, bool isPlayer = true, bool isPlayedFaceUp = true)
    {
        //checking validity
        if (!isPlayedFaceUp)
        {
            //handle playing trap card here
            //NOTE: playing trap card is handled in TrapCardManager.cs, this is no longer required.
            Debug.Log("playing card face down is not handled yet.");
            return false;
        }
        faceUpcardsPlayed++;

        /*if (faceUpcardsPlayed > MAX_CARDS_ALLOWED)
        {
            Debug.Log("Chain is maxed 10 cards!");
            faceUpcardsPlayed--;
            return false;
        }*/

        //all is valid.

        //improvement: if player starts the turn, the first collumn is placed right side.

        InitiateCardImage(card);


        //lerping or not?
        if (faceUpcardsPlayed > MaxWithoutLerping) {
            SetupLerping();
            
        }

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
        cardsInChain[0].color = new Color(1, 1, 1, 1.0f);
        fSwitchCardPosX = -4.9f;

        bChainManualReset = false;
    }

    #endregion

    #region LERP_STUFF

    private void TestLerp()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SetupLerping();
        }
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
        //currentLerpTime = 0;
    }

    //other class calls this function to start lerping to a new position.
    private void SetupLerping()
    {
        /*if (currentLerpTime >= maximumLerpTime)//already lerp to maximum position
        {
            return;
        }*/
        targetLerpingPosition += eachLerpSize * Vector3.up;
        startLerpingPosition = rect.localPosition;
        isLerping = true;   //lerp starts
        timePassedSinceLerping = 0f;

        //currentLerpTime++;
    }

    //lerp from start to target position in 1 second
    private void LerpUI()
    {
        if (!isLerping)
        {
            return;
        }
        if(timePassedSinceLerping > lerpLength)//lerp ends
        {
            isLerping = false;
            return;
        }
        float fractionOfJourney = timePassedSinceLerping / lerpLength;

        rect.localPosition = Vector3.Lerp(startLerpingPosition, targetLerpingPosition, fractionOfJourney);
    }

    #endregion
}
