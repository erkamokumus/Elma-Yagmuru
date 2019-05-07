using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }

    public void Leveller(int levelid)
    {
        SceneManager.LoadScene(levelid);
    }

    public void TekrarOyna()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
