using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    [SerializeField] Health healthRef;
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
        if (enableKill == true)
        {
            KillEnemy();
            CheckForEnableKill();
        }
    }

    void LoadNextScene()
    {
        if (healthRef.health <= 0)
        {
            //Debug.Log("You Lose!");
            SceneManager.LoadScene(4);
        }
    }

    //Testing purposes only
    void KillEnemy()
    {
        if (Input.GetKeyDown(KeyCode.P) && enableKill == true)
        {
            healthRef.health = 0;
            Debug.LogWarning("Player Died: You Lose Scene loaded!");
        }
    }

    bool i = false;
    void CheckForEnableKill()
    {

        if (enableKill == true && i == false)
        {
            Debug.LogWarning("Press 'p' to kill player.");
            i = true;
        }
    }
}
