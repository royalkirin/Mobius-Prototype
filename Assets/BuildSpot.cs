using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSpot : MonoBehaviour
{
    private JG_PlayerMovement player;
    public GameObject building;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<JG_PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && player.GetCurrentSegment() == this.gameObject)
        {
            //Debug.Log("Input handling works");
            building.SetActive(true);
        }
    }
}
