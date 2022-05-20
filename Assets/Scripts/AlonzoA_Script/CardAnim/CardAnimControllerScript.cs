using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimControllerScript : MonoBehaviour
{
    [SerializeField]
    Animator cardController;
    [SerializeField]
    GameObject[] cards;

    private void Awake()
    {
        cardController = this.GetComponent<Animator>();
    }

    // This is used to reshuffle the entire hand
    public void CardsOff()
    {
        for(int i = 0; i < cards.Length; i++)
        {
            cards[i].SetActive(true);
        }
    }

    // This is used to draw all the cards when the hand is ready to reset. 
    public void DrawCards(string boolName)
    {
        cardController.SetBool(boolName, true);
    }

    public void ResetCards(string boolName)
    {
        cardController.SetBool(boolName, false);
    }

    // This is used for turning off individual cards.
    public void HideCard(int i)
    {
        cards[i].SetActive(false);
    }

    public void ShowCard(int i)
    {
        cards[i].SetActive(true);
    }
}
