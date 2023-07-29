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
    private bool canLowerHand = true;
    [SerializeField] private Sprite[] damageSprites;
    private int hitPoints = 0;
    private SpriteRenderer damageSpriteRenderer;
    private Slider dopamineGauge;
    private Image dopamineBarGauge;

    public void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<IPlayerController>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<ISpawnManager>();
        damageSpriteRenderer = GameObject.FindWithTag("DamageSprite").GetComponent<SpriteRenderer>();
        dopamineGauge = GameObject.FindWithTag("DopamineGauge").GetComponent<Slider>();
        dopamineBarGauge = dopamineGauge.GetComponentInChildren<Image>();
        
        playerController.OnGameOverSignal.AddListener(this.GameOver);
        StartGame();
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
        dopamineGauge.value = dopamina / 100f;
        if (canLowerHand)
            dopamineBarGauge.color = Color.magenta;
        else
            dopamineBarGauge.color = Color.red;
    }

    private void HandleDopamine()
    {
        if(playerController.GetIsPhoneDown())
            dopamina -= dopamineDepleteRatio * Time.deltaTime;
        else
        {
            dopamina += dopamineIncreaseRatio * Time.deltaTime;            
        }

        if (dopamina < 0)
        {
            dopamina = 0;
            canLowerHand = false;
            //dopamineStatusDepleting = false;
        }
        if (dopamina > 100)
        {
            dopamina = 100;
            canLowerHand = true;
            //dopamineStatusDepleting = true;
        }
    }

    public bool CanLowerHand()
    {
        return canLowerHand;
    }

    public void StartGame()
    {
        dopamina = 100;
        InternalGameStatus(true);
    }

    public void GameOver()
    {
        InternalGameStatus(false);
        gameSpeed = 0;

    }

    private void InternalGameStatus(bool status)
    {
        gameStatus = status;
    /*
        GameObject.Find("Player").GetComponent<IPlayerController>().EnableMovement(status);
        GameObject.Find("SpawnManager").GetComponent<ISpawnManager>().SetSpawnStatus(status);
    */

      //playerController.EnableMovement(status);
        spawnManager.SetSpawnStatus(status);
    }

    public bool GetGameStatus()
    {
        return gameStatus;
    }
    
    public void GetDamage()
    {
        hitPoints++;
        damageSpriteRenderer.sprite = damageSprites[hitPoints];
        if (hitPoints >= damageSprites.Length-1)
        {
            Debug.Log("TE MORISTE");
            GameOver();
        }

    }

}
