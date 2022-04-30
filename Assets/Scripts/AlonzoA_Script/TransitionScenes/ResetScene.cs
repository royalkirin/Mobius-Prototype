using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    [SerializeField] Health healthRef;
    TutorialMusic tutMusic = null;
    LevelOneMusic loMusic = null;
    LevelTwoMusic ltMusic = null;

    [SerializeField] bool enableKill = false;
    [SerializeField] MusicBools[] bools;

    // Start is called before the first frame update
    void Start()
    {
        healthRef = this.GetComponent<Health>();

        if (bools[0].isTrue == true)
        {
            tutMusic = FindObjectOfType<TutorialMusic>();
        }
        else if (bools[1].isTrue == true)
        {
            loMusic = FindObjectOfType<LevelOneMusic>();
        }
        else if (bools[2].isTrue == true)
        {
            ltMusic = FindObjectOfType<LevelTwoMusic>();
        }
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
            MusicStop();
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

    //--------------------------------------
    /// This portion controls audio.
    /// 

    private void MusicStop()
    {
        if (bools[0].isTrue == true)
        {
            tutMusic.Stop();
        }
        else if (bools[1].isTrue == true)
        {
            loMusic.Stop();
        }
        else if (bools[2].isTrue == true)
        {
            ltMusic.Stop();
        }
    }
}
