using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour, IPlayerController
{
    //private bool movement; // Replaced by enableing and disabling scripts
    [SerializeField] private float rangeMovement = 3f;
    [SerializeField] private float speed = 1f;
    private bool performJump = false;
    private Rigidbody myRB;
    [SerializeField] private float jumpForce = 10f;
    private Animator handAnimator;

    /*public void EnableMovement(bool movement)
    {
        this.movement = movement;
    }*/

    protected UnityEvent GameOverSignal = new UnityEvent();

    public UnityEvent OnGameOverSignal { get { return GameOverSignal; } }

    // Start is called before the first frame update
    void Start()
    {
        //EnableMovement(true);
        myRB = GetComponent<Rigidbody>();
        handAnimator = GetComponentInChildren<Animator>();
    }

    void HandlePLayerMovement(float moveAmount)
    {
        float currentPosition = transform.position.x;
        currentPosition += moveAmount * speed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(currentPosition, -rangeMovement, rangeMovement), transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        HandlePLayerMovement(Input.GetAxisRaw("Horizontal"));
        if (Input.GetButtonDown("Jump") && !GetIsJumping())
            performJump = true;
        if (Input.GetButton("Fire1"))
            handAnimator.SetBool("HandDown",true);
        else
            handAnimator.SetBool("HandDown",false);
    }

    private void FixedUpdate()
    {
        if (performJump)
        {
            myRB.velocity = new Vector3(0, jumpForce, 0);
            performJump = false;
        }
    }

    public bool GetIsJumping()
    {
        return Mathf.Abs(myRB.velocity.y) > Mathf.Epsilon; // Mejorable TODO
        
    }

    void OnCollisionEnter(Collision collision)
    {
        //GameOverSignal.Invoke(); <--- TODO
    }
}
