using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLowerHand : MonoBehaviour
{
    private Animator handAnimator;
    private bool isPhoneDown = false;
    public bool canLowerHand = true;



    // Start is called before the first frame update
    void Start()
    {
        handAnimator = GetComponentInChildren<Animator>();

    }



    // Update is called once per frame
    void Update()
    {


    }


}
