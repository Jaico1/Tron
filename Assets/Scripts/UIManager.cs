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
}
