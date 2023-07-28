using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour, IPlayerController
{
    private bool movement;
    public void EnableMovement(bool movement)
    {
        this.movement = movement;
    }

    protected UnityEvent GameOverSignal = new UnityEvent();

    public UnityEvent OnGameOverSignal { get { return GameOverSignal; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        GameOverSignal.Invoke();
    }
}
