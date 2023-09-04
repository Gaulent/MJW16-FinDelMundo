using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Action onGameOver;
    
    bool gameStatus = false;
    float dopamina = 100;
    [SerializeField] private float dopamineDepleteRatio = 2f;
    [SerializeField] private float dopamineIncreaseRatio = 2f;
    PlayerController playerController;

// DEPRECTED: Difficulty By Time
//    DateTime currentTime;
    //SpawnManager spawnManager;
    [SerializeField] private float maxGameSpeed = 10f;
    private float gameSpeed = 0f;
    //SpawnManager backgroundSpawnManager;
    //private SpawnManager powerUpSpawnManager;
    private bool canLowerHand = true;
    [SerializeField] private Sprite[] damageSprites;
    private int hitPoints = 0;
    private SpriteRenderer damageSpriteRenderer;
    private Image dopamineSpriteRenderer;
    [SerializeField] private Sprite[] dopamineSprites;
    private Slider dopamineGauge;
    private Image dopamineBarGauge;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private Text ScoreText;
    [SerializeField] private List<int> EnemiesByWaves;

    //[SerializeField] private Dictionary<EDifficultyWaves,int> EnemiesByWaves;
    // Failed...s
    //[SerializeField] public Dictionary<EDifficultyWaves,List<GameObject>> WaveList;
    [SerializeField] List<GameObject> Wave1List;
    [SerializeField] List<GameObject> Wave2List;
    [SerializeField] List<GameObject> Wave3List;
    
    EDifficultyWaves currentDifficultyWave;

    public void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        //spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        //backgroundSpawnManager = GameObject.Find("BackgroundSpawnManager").GetComponent<SpawnManager>();
        //powerUpSpawnManager = GameObject.Find("PowerUpSpawnManager").GetComponent<SpawnManager>();
        dopamineSpriteRenderer = GameObject.Find("Dopamine Background").GetComponent<Image>();
        damageSpriteRenderer = GameObject.FindWithTag("DamageSprite").GetComponent<SpriteRenderer>();
        dopamineGauge = GameObject.FindWithTag("DopamineGauge").GetComponent<Slider>();
        dopamineBarGauge = dopamineGauge.GetComponentInChildren<Image>();

        playerController.OnGameOverSignal.AddListener(this.GameOver);
        //spawnManager.OnWaveEndedSignal.AddListener(WaveEnded);
        
        StartGame();
    }
/*
    private void WaveEnded()
    {
        currentDifficultyWave++;
        if ((int)currentDifficultyWave >= Enum.GetNames(typeof(EDifficultyWaves)).Length)
        {
            /// TODO Make call
        }

        spawnManager.SetEnemiesWaves(
            GetEnemiesObjectList(currentDifficultyWave), 
            EnemiesByWaves[(int)currentDifficultyWave]
            );

    }*/

    public List<GameObject> GetEnemiesObjectList(EDifficultyWaves difficultyWaves)
    {
        switch(difficultyWaves)
        {
            case EDifficultyWaves.Wave1: return Wave1List;
            case EDifficultyWaves.Wave2: return Wave2List;
            case EDifficultyWaves.Wave3: return Wave3List;
        }
        return new List<GameObject>();
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
        else{
            dopamineBarGauge.color = Color.red;
            dopamineSpriteRenderer.sprite = dopamineSprites[1];
            }
    }

/*
    private void getInternalTime()
    {
        return DateTime.Now - currentTime;
    }

*/
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
        //currentTime = System.DateTime.Now;
        InternalGameStatus(true);
        gameSpeed = maxGameSpeed;
    }

    public delegate void OnButtonClick();
    public static event OnButtonClick onButtonClick;
    public void RaiseOnButtonClick() {
        if (onButtonClick != null) {
            onButtonClick();
        }
    }    
    
    public void GameOver()
    {
        InternalGameStatus(false);
        gameSpeed = 0;
        playerController.Disable();
        FindObjectOfType<VideoPlayerRandomizer>().StopVideo();
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
    /*
        GameObject.Find("Player").GetComponent<IPlayerController>().EnableMovement(status);
        GameObject.Find("SpawnManager").GetComponent<ISpawnManager>().SetSpawnStatus(status);
    */

      //playerController.EnableMovement(status);
        //spawnManager.SetSpawnStatus(status);
        //backgroundSpawnManager.SetSpawnStatus(status);
        // Should We comment this?
        //powerUpSpawnManager.SetSpawnStatus(status);
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
            Debug.Log("TE MORISTE! GAME OVER");
            GameOver();
        }

    }

    // Testing Time Lines
    public void EndOfLevel()
    {
        InternalGameStatus(false);
        gameSpeed = 0;
        playerController.Disable();
    }

}
