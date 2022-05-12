using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenseBarShaker : MonoBehaviour
{
    public bool startBeat;
    public bool startShake;
    [SerializeField] int timer;
    [SerializeField] int timerBeat;
    [SerializeField] int timerShake;

    [SerializeField] float xPosition;

    // Start is called before the first frame update
    void Start()
    {
        xPosition = gameObject.GetComponent<Transform>().localPosition.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startBeat && timerShake <= 0)
        {
            timer++;
            timerBeat = timer;
            if (timer >= 5 && timer <= 9)
            {
                gameObject.GetComponent<Transform>().transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                gameObject.GetComponent<Image>().color = new Color(0.1660199f, 0.3161518f, 0.84873455f, 0.9245283f);
            }
            else if (timer >= 10 && timer <= 14)
            {
                gameObject.GetComponent<Transform>().transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            }
            else if (timer >= 15 && timer <= 19)
            {
                gameObject.GetComponent<Transform>().transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            else if (timer >= 20 && timer <= 24)
            {
                gameObject.GetComponent<Transform>().transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            }
            else if (timer >= 25)
            {
                gameObject.GetComponent<Transform>().transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
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
                gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(xPosition, .0f, 0.0f);
                gameObject.GetComponent<Image>().color = new Color(0.1694998f, 0.1797441f, 0.4018868f, 1f);
            }
            else if (timer >= 10 && timer <= 14)
            {
                gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(xPosition + 5, 0.0f, 0.0f);
            }
            else if (timer >= 15 && timer <= 19)
            {
                gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(xPosition + 10, 0.0f, 0.0f);
            }
            else if (timer >= 20 && timer <= 24)
            {
                gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(xPosition + 5, 0.0f, 0.0f);
            }
            else if (timer >= 25 && timer <= 29)
            {
                gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(xPosition, 0.0f, 0.0f);
            }
            else if (timer >= 30 && timer <= 34)
            {
                gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(xPosition - 5, 0.0f, 0.0f);
            }
            else if (timer >= 34 && timer <= 39)
            {
                gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(xPosition - 10, 0.0f, 0.0f);
            }
            else if (timer >= 40 && timer <= 44)
            {
                gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(xPosition - 5, 0.0f, 0.0f);
            }
            else if (timer >= 45)
            {
                gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(xPosition, 0f, 0.0f);
                timer = 0;
                timerShake = 0;
                startShake = false;
            }
        }

        if (startBeat == false && startShake == false)
        {
            gameObject.GetComponent<Transform>().transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            //gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(-50.0f, -50.0f, 0.0f);
            timer = 0;
            timerBeat = 0;
            timerShake = 0;
        }
    }
}
