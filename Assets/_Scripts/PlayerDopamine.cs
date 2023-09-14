using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDopamine : MonoBehaviour
{
    public float Dopamine { get; private set; }
    [SerializeField] private float dopamineDepleteRatio = 60f;
    [SerializeField] private float dopamineIncreaseRatio = 20f;
    PlayerController playerController;
    private Animator handAnimator;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Dopamine = 100;
        playerController = GetComponent<PlayerController>();
        handAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!handAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hand Idle") &&
           !handAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hand Rising"))
            Dopamine -= dopamineDepleteRatio * Time.deltaTime;
        else
            Dopamine += dopamineIncreaseRatio * Time.deltaTime;            

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
