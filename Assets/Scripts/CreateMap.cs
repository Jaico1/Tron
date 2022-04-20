using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public GameObject arena1;
    public GameObject arena2;
    public GameObject arena3;
    private int mapID;
    void Start()
    {
        mapID = PlayerPrefs.GetInt("MapID");

        if (mapID == 0)
        {
            PlayerPrefs.SetInt("MapID", 1);
            mapID = PlayerPrefs.GetInt("MapID");
        }
        if ( mapID==1 )
        {
            arena1.SetActive(true);
            arena2.SetActive(false);
            arena3.SetActive(false);
        } 
        else if(mapID==2)
        {
            arena2.SetActive(true);
            arena1.SetActive(false);
            arena3.SetActive(false);
        }
        else if (mapID == 3)
        {
            arena3.SetActive(true);
            arena1.SetActive(false);
            arena2.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
