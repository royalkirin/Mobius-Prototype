using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimControllerScript : MonoBehaviour
{
    [SerializeField]
    Animator cardController;
    [SerializeField]
    GameObject[] cards;

    private void Start()
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
    public void DrawCards()
    {
        cardController.SetBool("ToggleCardDraw", true);
    }

    public void ResetCards()
    {
        cardController.SetBool("ToggleCardDraw", false);
    }

    // This is used for turning off individual cards.
    public void HideCard(int i)
    {
        cards[i].SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            HideCard(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            CardsOff();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            DrawCards();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            ResetCards();
        }
    }
}
