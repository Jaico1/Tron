using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectColor : MonoBehaviour
{
    public BoxCollider2D col;
    public Color color;
    public bool playerOne;
    public bool playerTwo;

    // Start is called before the first frame update
    void Start()
    {
        if (playerOne)
        {
            PlayerPrefs.SetFloat("redOne", 0);
            PlayerPrefs.SetFloat("greenOne", 255);
            PlayerPrefs.SetFloat("blueOne", 255);
        } 
        else if (playerTwo)
        {
            PlayerPrefs.SetFloat("redTwo", 255);
            PlayerPrefs.SetFloat("greenTwo", 0);
            PlayerPrefs.SetFloat("blueTwo", 255);
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
