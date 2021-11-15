using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for now, control the CardPlayer component in the player
//CardPlayer provides the execution, we link the Input to those executions here
//in the future, link different player components here.
[RequireComponent(typeof(CardPlayer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] CardPlayer cardPlayer = null;

    private void Start()
    {
        if(cardPlayer is null)
        {
            Debug.Log("Missing CardPlayer in " + name);
        }
    }
}
