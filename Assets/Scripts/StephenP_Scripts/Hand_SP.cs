using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Hand_SP : MonoBehaviour
{
    public List<GameObject> Cards { get; private set; }
    public int MaxCards = 5;

    [SerializeField] List<Transform> cardPositionsLimited;

    private void Awake()
    {
        Cards = new List<GameObject>();
    }

    public void AddCard(GameObject cardPrefab)
    {
        Cards.Add(cardPrefab);
    }
    
    public void RemoveCard(Card_SP card)
    { 
        Cards.Remove(card.gameObject);
    }

    public void DrawCards()
    {
        for(int i = 0; i < Cards.Count; i++)
        {
            GameObject.Instantiate(Cards[i], cardPositionsLimited[i].position, quaternion.identity);
        }
    }
}
