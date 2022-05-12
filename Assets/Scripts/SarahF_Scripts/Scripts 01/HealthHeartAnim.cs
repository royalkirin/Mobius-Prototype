using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHeartAnim : MonoBehaviour
{

    [SerializeField] GameObject phantomHeart;

    public bool startBeat;
    public bool startShake;
    [SerializeField] int timer;
    [SerializeField] int timerBeat;
    [SerializeField] int timerShake;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startBeat && timerShake <= 0)
        {
            timer++;
            timerBeat = timer;
            if (timer >= 5 && timer <= 9)
            {
                phantomHeart.GetComponent<Transform>().transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            else if (timer >= 10 && timer <= 14)
            {
                phantomHeart.GetComponent<Transform>().transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            }
            else if (timer >= 15 && timer <= 19)
            {
                phantomHeart.GetComponent<Transform>().transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            else if (timer >= 20 && timer <= 24)
            {
                phantomHeart.GetComponent<Transform>().transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            }
            else if (timer >= 25)
            {
                phantomHeart.GetComponent<Transform>().transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                timer = 0;
                timerBeat = 0;
                startBeat = false;
            }
        }

        if (startShake && timerBeat <= 0)
        {
            timer++;
            timerShake = timer;
            if (timer >= 5 && timer <= 9)
            {
                phantomHeart.GetComponent<Transform>().transform.localPosition = new Vector3(-50.0f, -50.0f, 0.0f);
                phantomHeart.GetComponent<Image>().color = new Color(.7f, .7f, .7f, .5f);
            }
            else if (timer >= 10 && timer <= 14)
            {
                phantomHeart.GetComponent<Transform>().transform.localPosition = new Vector3(-55.0f, -50.0f, 0.0f);
            }
            else if (timer >= 15 && timer <= 19)
            {
                phantomHeart.GetComponent<Transform>().transform.localPosition = new Vector3(-60.0f, -50.0f, 0.0f);
            }
            else if (timer >= 20 && timer <= 24)
            {
                phantomHeart.GetComponent<Transform>().transform.localPosition = new Vector3(-55.0f, -50.0f, 0.0f);
            }
            else if (timer >= 25 && timer <= 29)
            {
                phantomHeart.GetComponent<Transform>().transform.localPosition = new Vector3(-50.0f, -50.0f, 0.0f);
            }
            else if (timer >= 30 && timer <= 34)
            {
                phantomHeart.GetComponent<Transform>().transform.localPosition = new Vector3(-45.0f, -50.0f, 0.0f);
            }
            else if (timer >= 34 && timer <= 39)
            {
                phantomHeart.GetComponent<Transform>().transform.localPosition = new Vector3(-40.0f, -50.0f, 0.0f);
            }
            else if (timer >= 40 && timer <= 44)
            {
                phantomHeart.GetComponent<Transform>().transform.localPosition = new Vector3(-45.0f, -50.0f, 0.0f);
            }
            else if (timer >= 45)
            {
                phantomHeart.GetComponent<Transform>().transform.localPosition = new Vector3(-50.0f, -50.0f, 0.0f);
                phantomHeart.GetComponent<Image>().color = new Color(1f, 1f, 1f, .5f);
                timer = 0;
                timerShake = 0;
                startShake = false;
            }
        }

        if (startBeat == false && startShake == false)
        {
            phantomHeart.GetComponent<Transform>().transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            phantomHeart.GetComponent<Transform>().transform.localPosition = new Vector3(-50.0f, -50.0f, 0.0f);
            phantomHeart.GetComponent<Image>().color = new Color(1f, 1f, 1f, .5f);
            timer = 0;
            timerBeat = 0;
            timerShake = 0;
        }
    }
}
