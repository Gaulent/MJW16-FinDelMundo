using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHit : MonoBehaviour
{
    private PlayerController _myPc;
    
    // Start is called before the first frame update
    void Start()
    {
        _myPc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_myPc.GetIsJumping())
        {
            Debug.Log("TriggerDetected, but jumping");
        }
        else
        {
            Debug.Log("TriggerDetected");
        }
    }
}
