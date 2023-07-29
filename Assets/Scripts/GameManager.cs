using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IGameManager
{
    bool gameStatus = false;
    private bool dopamineStatusDepleting = true;
    float dopamina = 100;
    [SerializeField] private float dopamineDepleteRatio = 2f;
    [SerializeField] private float dopamineIncreaseRatio = 2f;
    IPlayerController playerController;
    ISpawnManager spawnManager;
    [SerializeField] private float gameSpeed = 10f;
    [SerializeField] private Text dopamineField;

    public void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<IPlayerController>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<ISpawnManager>();

        playerController.OnGameOverSignal.AddListener(this.GameOver);
    }

    public float GetGameSpeed()
    {
        return gameSpeed;
    }

    public void Update()
    {
        /*
        if (dopamina < 0)
        {
            GameOver();
        }
        */

        HandleDopamine();
        UpdateUI();

    }

    private void UpdateUI()
    {   
        dopamineField.text = dopamina.ToString();
    }

    private void HandleDopamine()
    {
        if(dopamineStatusDepleting)
            dopamina -= dopamineDepleteRatio * Time.deltaTime;
        else
        {
            dopamina += dopamineIncreaseRatio * Time.deltaTime;            
        }

        if (dopamina < 0)
        {
            dopamina = 0;
            dopamineStatusDepleting = false;
        }
        if (dopamina > 100)
        {
            dopamina = 100;
            dopamineStatusDepleting = true;
        }
    }
    
    

    public void StartGame()
    {
        dopamina = 100;
        InternalGameStatus(true);
    }

    public void GameOver()
    {
        InternalGameStatus(false);
    }

    private void InternalGameStatus(bool status)
    {
        gameStatus = status;
    /*
        GameObject.Find("Player").GetComponent<IPlayerController>().EnableMovement(status);
        GameObject.Find("SpawnManager").GetComponent<ISpawnManager>().SetSpawnStatus(status);
    */
      //playerController.EnableMovement(status);
      spawnManager.SetSpawnStatus(true);

    }

    public bool GetGameStatus()
    {
        return gameStatus;
    }

}
