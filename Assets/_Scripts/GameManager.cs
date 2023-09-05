using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    
    bool gameStatus = false;
    float dopamina = 100;
    [SerializeField] private float dopamineDepleteRatio = 2f;
    [SerializeField] private float dopamineIncreaseRatio = 2f;
    PlayerController playerController;
    [SerializeField] private float maxGameSpeed = 10f;
    private float gameSpeed = 0f;
    private bool canLowerHand = true;
    private Image dopamineSpriteRenderer;
    [SerializeField] private Sprite[] dopamineSprites;
    private Slider dopamineGauge;
    private Image dopamineBarGauge;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private Text ScoreText;


    public void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        dopamineSpriteRenderer = GameObject.Find("Dopamine Background").GetComponent<Image>();
        dopamineGauge = GameObject.FindWithTag("DopamineGauge").GetComponent<Slider>();
        dopamineBarGauge = dopamineGauge.GetComponentInChildren<Image>();
        
        StartGame();
    }

    public float GetGameSpeed()
    {
        return gameSpeed;
    }

    public void Update()
    {
        HandleDopamine();
        UpdateUI();
    }

    private void UpdateUI()
    {
        dopamineGauge.value = dopamina / 100f;
        if (canLowerHand)
        {
            dopamineBarGauge.color = Color.magenta;
            dopamineSpriteRenderer.sprite = dopamineSprites[0];
        }
        else
        {
            dopamineBarGauge.color = Color.red;
            dopamineSpriteRenderer.sprite = dopamineSprites[1];
        }
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
        }
        if (dopamina > 100)
        {
            dopamina = 100;
            canLowerHand = true;
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
        gameSpeed = maxGameSpeed;
    }
    
    public void GameOver()
    {
        InternalGameStatus(false);
        gameSpeed = 0;
        playerController.Disable();
        gameOverCanvas.SetActive(true);

        ScoreText.text = "HAS SOBREVIVIDO\n" + Time.timeSinceLevelLoad.ToString("F2") + " SEGUNDOS";
        
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(es.firstSelectedGameObject);
        
        onGameOver.Invoke(); // <--
    }

    private void InternalGameStatus(bool status)
    {
        gameStatus = status;
    }

    public bool GetGameStatus()
    {
        return gameStatus;
    }
}
