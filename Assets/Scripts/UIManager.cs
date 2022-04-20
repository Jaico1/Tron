using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI ScoreOne;
    public TextMeshProUGUI ScoreTwo;
    public TextMeshProUGUI Countdown;
    public GameObject shootp1;
    public GameObject shootp2;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateScores(int one, int two)
    {
        ScoreOne.text = "" + one;
        ScoreTwo.text = "" + two;
    }

    public void UpdateCountdown(int countdown)
    {
        Countdown.text = "" + countdown;
    }

    public void UpdateShoot(bool shoot, int player)
    {
        if (player == 0)
        {
            if (shoot)
            {
                shootp1.SetActive(false);
            }
            else
            {
                shootp1.SetActive(true);
            }
        }
        else if(player == 1)
        {
            if (shoot)
            {
                shootp2.SetActive(false);
            }
            else
            {
                shootp2.SetActive(true);
            }
        }
    }
}
