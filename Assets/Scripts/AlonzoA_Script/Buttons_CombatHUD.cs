using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Still need to place in functions for the skills. 
public class Buttons_CombatHUD : MonoBehaviour
{
    //finds the reference to the gameobject that contains the items menu.
    [Header("References")]
    [SerializeField]
    private GameObject _items;

    private void Start()
    {
        //makes sure the items menu is hidden.
        _items.SetActive(false); 
    }

    public void ItemsMenuLoad()
    {
        //shows the items menu.
        _items.SetActive(true);
        Debug.Log("Loaded items menu!");
    }

    public void Back()
    {
        //hides the items menu again.
        _items.SetActive(false);
    }

    public void Run()
    {
        //will load the scene with the player base/home when it is added to the build order. 
        //SceneManager.LoadScene("PlayerHome"); //This code will be used to load the scene when it is put into the build order. 
        Debug.LogWarning("Load scene: House/Player Home??");
    }
}
