using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LoadScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void ExiGame()
    {
        Application.Quit();
    }
}
