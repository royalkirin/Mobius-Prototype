using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField ] Health healthRef;
    [SerializeField] bool enableKill = false;

    // Start is called before the first frame update
    void Start()
    {
        healthRef = this.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        LoadNextScene();
        if(enableKill == true)
        {
            KillEnemy();
            CheckForEnableKill();
        }
    }

    void LoadNextScene()
    {
        if(healthRef.health <= 0)
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    //Testing purposes only
    void KillEnemy()
    { 
        if(Input.GetKeyDown(KeyCode.K) && enableKill == true)
        {
            healthRef.health = 0;
            Debug.LogWarning("Next Scene Loaded");
        }
    }

    bool i = false;
    void CheckForEnableKill()
    {

        if (enableKill == true && i == false)
        {
            Debug.LogWarning("Press 'k' to test next SceneLoad.");
            i = true;
        }
    }
}
