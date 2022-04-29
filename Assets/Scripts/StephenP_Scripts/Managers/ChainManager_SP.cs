using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainManager_SP : MonoBehaviour
{
    public static ChainManager_SP Instance;
    
    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
