using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDopamine : MonoBehaviour
{
    public float Dopamine { get; private set; }
    [SerializeField] private float dopamineDepleteRatio = 60f;
    [SerializeField] private float dopamineIncreaseRatio = 20f;
    PlayerController playerController;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Dopamine = 100;
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.IsPhoneDown)
            Dopamine -= dopamineDepleteRatio * Time.deltaTime;
        else
        {
            Dopamine += dopamineIncreaseRatio * Time.deltaTime;            
        }

        if (Dopamine < 0)
        {
            Dopamine = 0;
            playerController.CanLowerHand = false;
        }
        if (Dopamine > 100)
        {
            Dopamine = 100;
            playerController.CanLowerHand = true;
        }        
    }
}
