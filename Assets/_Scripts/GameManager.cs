using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Game { get; private set; }

    private void Awake()
    {
        if (Game != null && Game != this) Destroy(this);
        else Game = this;
    }
    
    public Action onGameOver;

    [SerializeField] private float maxGameSpeed = 12f;

    public float GameSpeed { get; private set; }

    
    public void Start()
    {

        GameSpeed = maxGameSpeed;
    }

   
    public void GameOver()
    {
        GameSpeed = 0;
        onGameOver.Invoke(); // <--
    }
}
