using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Countdown : MonoBehaviour
{
    float currentTime;
    public float startingTime = 10f;

    private bool start = false;

    [SerializeField] Text countdownText;
    void Start()
    {
        currentTime = startingTime;
    }
    void Update()
    {
        if (currentTime <= 0)
        {
            currentTime = 0;
            if (start == false)
            {
                GetComponent<Movement>().enabled = true;
                start = true;
                countdownText.text = "";
            }
            
        }
        else
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");
        }
    }
}
