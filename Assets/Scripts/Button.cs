using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public string nextScene;
    void Start()
    {
        PlayerPrefs.SetInt("MapID", 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    private void OnMouseDown()
    {
        
        ChangeScene();
    }
}
