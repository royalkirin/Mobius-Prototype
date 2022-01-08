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
    float eachLerpSize = 30f / 7; //bottom Y = -30, top Y = 0, lerp from card 4th -> 10th = 7 times
    Vector3 targetLerpingPosition;
    Vector3 startLerpingPosition;
    Vector3 orininalPosition; //when chain finishes, reset back to original position, reset all values.
    bool isLerping = false;//when lerping, switch this
    int maximumLerpTime = 7;
    int currentLerpTime = 0;
    float fSwitchCardPosX = -5.0f;

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

    public void InitiateCardImage(Card card)
    {
        Image uNewCard = Instantiate<Image>(uCardTemplate);
        uNewCard.transform.SetParent(CardImages.transform, false);
        uNewCard.transform.position = cardsInChain[faceUpcardsPlayed - 1].transform.position - new Vector3(fSwitchCardPosX, 0.0f, 8.0f);
        fSwitchCardPosX = fSwitchCardPosX * -1;
        FindVariables();
        cardsInChain[faceUpcardsPlayed - 1].sprite = card.GetFrontImage();
        cardsInChain[faceUpcardsPlayed - 1].gameObject.SetActive(true);
        //cardsInChain[faceUpcardsPlayed - 1].gameObject.transform.position = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private void FindVariables()
    {
        rect = GetComponent<RectTransform>();
        if (rect is null)
        {
            Debug.Log("Missing rect transform in " + name);
        }
        targetLerpingPosition = rect.localPosition;
        orininalPosition = rect.localPosition;


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
        ResetLerpingChain();
        faceUpcardsPlayed = 0;

        for (int i = 1; i < cardsInChain.Count; i++)
        {
            cardsInChain[i].sprite = null;
            Destroy(cardsInChain[i].gameObject);
            cardsInChain.RemoveAt(i);
            i--;
        }
        cardsInChain[0].sprite = null;
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
        currentLerpTime = 0;
    }

    //other class calls this function to start lerping to a new position.
    private void SetupLerping()
    {
        if (currentLerpTime >= maximumLerpTime)//already lerp to maximum position
        {
            return;
        }
        targetLerpingPosition += eachLerpSize * Vector3.up;
        startLerpingPosition = rect.localPosition;
        isLerping = true;   //lerp starts
        timePassedSinceLerping = 0f;
        currentLerpTime++;
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
