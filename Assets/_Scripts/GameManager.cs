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

    
    public float Dopamine { get; private set; }

    [SerializeField] private float dopamineDepleteRatio = 60f;
    [SerializeField] private float dopamineIncreaseRatio = 20f;
    PlayerController playerController;
    [SerializeField] private float maxGameSpeed = 12f;
    public bool CanLowerHand { get; private set; }
    public float GameSpeed { get; private set; }

    
    public void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Dopamine = 100;
        CanLowerHand = true;
        GameSpeed = maxGameSpeed;
    }

    public void Update()
    {
        HandleDopamine();
    }


    private void HandleDopamine()
    {
        if(playerController.GetIsPhoneDown())
            Dopamine -= dopamineDepleteRatio * Time.deltaTime;
        else
        {
            Dopamine += dopamineIncreaseRatio * Time.deltaTime;            
        }

        if (Dopamine < 0)
        {
            Dopamine = 0;
            CanLowerHand = false;
        }
        if (Dopamine > 100)
        {
            Dopamine = 100;
            CanLowerHand = true;
        }
    }
   
    public void GameOver()
    {
        GameSpeed = 0;
        onGameOver.Invoke(); // <--
    }
}
