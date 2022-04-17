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
            Debug.Log("You Win!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    //Testing purposes only
    void KillEnemy()
    {
        if (Input.GetKeyDown(KeyCode.P) && enableKill == true)
        {
            healthRef.health = 0;
            Debug.LogWarning("Player Died: Scene reload!");
        }
    }

    bool i = false;
    void CheckForEnableKill()
    {

        if (enableKill == true && i == false)
        {
            Debug.LogWarning("Press 'p' to test next SceneLoad.");
            i = true;
        }
    }
}
