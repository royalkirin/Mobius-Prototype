using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public enum CardType
    {
        None = 0,
        Attack,
        Defense,
        Support
    }

    [SerializeField] GameObject AttackPrefab, DefensePrefab, SupportPrefab;

    List<GameObject> CurrentHand;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHand = new List<GameObject>();
        NewHand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewHand()
    {
        ClearHand();

        for(int i = 0; i < transform.childCount; i++)
        {
            switch(Mathf.CeilToInt(Random.Range(0.00001f, 3.0f)))
            {
                case 0:
                    Debug.Log("Jacob has commited an oof");
                    break;
                case 1:
                    CurrentHand.Add(GameObject.Instantiate(AttackPrefab, transform.GetChild(i)));
                    break;
                case 2:
                    CurrentHand.Add(GameObject.Instantiate(DefensePrefab, transform.GetChild(i)));
                    break;
                case 3:
                    CurrentHand.Add(GameObject.Instantiate(SupportPrefab, transform.GetChild(i)));
                    break;
            }
        }
    }

    public void ClearHand()
    {
        if(CurrentHand.Count > 0)
        {
            for(int i = CurrentHand.Count - 1; i >= 0; i--)
            {
                GameObject.Destroy(CurrentHand[i]);
                CurrentHand.RemoveAt(i);
            }
        }
    }
}
