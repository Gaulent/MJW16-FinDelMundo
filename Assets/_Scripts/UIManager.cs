using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private Image dopamineSpriteRenderer;
    [SerializeField] private Sprite dopamineNormal;
    [SerializeField] private Sprite dopamineLow;
    private Slider dopamineGauge;
    private Image dopamineBarGauge;    
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private Text ScoreText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Game.onGameOver += OnGameOver;
        
        dopamineSpriteRenderer = GameObject.Find("Dopamine Background").GetComponent<Image>();
        dopamineGauge = GameObject.FindWithTag("DopamineGauge").GetComponent<Slider>();
        dopamineBarGauge = dopamineGauge.GetComponentInChildren<Image>();    
    }

    // Update is called once per frame
    void Update()
    {
        dopamineGauge.value = GameManager.Game.Dopamine / 100f;
        if (GameManager.Game.CanLowerHand)
        {
            dopamineBarGauge.color = Color.magenta;
            dopamineSpriteRenderer.sprite = dopamineNormal;
        }
        else
        {
            dopamineBarGauge.color = Color.red;
            dopamineSpriteRenderer.sprite = dopamineLow;
        }        
    }

    private void OnGameOver()
    {
        gameOverCanvas.SetActive(true);
        ScoreText.text = "HAS SOBREVIVIDO\n" + Time.timeSinceLevelLoad.ToString("F2") + " SEGUNDOS";
        
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(es.firstSelectedGameObject);
    }
}
