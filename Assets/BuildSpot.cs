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
        if(Input.GetKeyDown(KeyCode.P) && player.GetCurrentSegment() == this.gameObject && !building.activeInHierarchy)
        {
            //Debug.Log("Input handling works");
            
            GetComponent<ParticleSystem>().Play();

            Invoke("Build", 2.0f);
        }
    }

    private void Build()
    {
        building.SetActive(true);
    }
}
