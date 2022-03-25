using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverOverMe : MonoBehaviour
{

    [SerializeField] GameObject myPartner;
    [SerializeField] GameObject myPartnerText;

    [SerializeField] string[] infoText;
    [SerializeField] int textUsed;

    [SerializeField] GameObject levelSelect;

    // Start is called before the first frame update
    void Start()
    {
        if (textUsed > infoText.Length - 1)
        {
            Debug.LogWarning("Selected textUsed is too high.");
        }
        else if (textUsed < 0)
        {
            Debug.LogWarning("Selected textUsed is too low.");
        }
        else
        {
            myPartnerText.GetComponent<Text>().text = infoText[textUsed];
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (levelSelect.active == true)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnMouseOver()
    {   
        myPartner.SetActive(true);
    }

    private void OnMouseExit()
    {
        myPartner.SetActive(false);
    }
}
