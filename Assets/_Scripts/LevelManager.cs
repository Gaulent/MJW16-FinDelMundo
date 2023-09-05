using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }
    
    public void ReLoadGame()
    {
        SceneManager.LoadScene(0);
    }
}
