using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectColor : MonoBehaviour
{
    public BoxCollider2D col;
    public Color color;
    public bool playerOne;
    public bool playerTwo;
    public int livesOne = 3;
    public int livesTwo = 3;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("livesOne", livesOne);
        PlayerPrefs.SetInt("livesTwo", livesTwo);

        if (playerOne)
        {
            PlayerPrefs.SetFloat("redTwo", 0);
            PlayerPrefs.SetFloat("greenTwo", 255);
            PlayerPrefs.SetFloat("blueTwo", 255);
        } 
        else if (playerTwo)
        {
            PlayerPrefs.SetFloat("redOne", 255);
            PlayerPrefs.SetFloat("greenOne", 0);
            PlayerPrefs.SetFloat("blueOne", 255);
        }
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (playerOne)
        {
            PlayerPrefs.SetFloat("redOne", color.r);
            PlayerPrefs.SetFloat("greenOne", color.g);
            PlayerPrefs.SetFloat("blueOne", color.b);
        }
        else if (playerTwo)
        {
            PlayerPrefs.SetFloat("redTwo", color.r);
            PlayerPrefs.SetFloat("greenTwo", color.g);
            PlayerPrefs.SetFloat("blueTwo", color.b);
        }
    }
}
