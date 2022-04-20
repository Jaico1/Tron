using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinMessage : MonoBehaviour
{
    public TextMeshProUGUI winMessage;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("win") == 0)
        {
            winMessage.text = "PLAYER 1 WINS!";
        }else if (PlayerPrefs.GetInt("win") == 1)
        {
            winMessage.text = "PLAYER 2 WINS!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
