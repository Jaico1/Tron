using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMap : MonoBehaviour
{
    public int mapid;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("MapID", 1);

    }

    
    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("MapID"));
    }
    private void OnMouseDown()
    {
        PlayerPrefs.SetInt("MapID", mapid);
    }
}
