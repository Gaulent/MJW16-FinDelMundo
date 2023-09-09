using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rangeMovement = 3f;
    [SerializeField] private float speed = 1f;
    private bool performJump = false;
    private Rigidbody myRb;
    [SerializeField] private float jumpForce = 5f;
    private Animator handAnimator;
    [SerializeField] private AudioClip jumpAudioClip;
    public bool CanLowerHand { get; set; }
    public bool IsPhoneDown { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        IsPhoneDown = false;
        GameManager.Game.onGameOver += OnGameOver;
        myRb = GetComponent<Rigidbody>();
        handAnimator = GetComponentInChildren<Animator>();
        CanLowerHand = true;
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
        {
            performJump = true;
            AudioSource.PlayClipAtPoint(jumpAudioClip, transform.position); // TODO: Buscar otra forma de hacerlo
        }
        
        IsPhoneDown = CanLowerHand && Input.GetButton("Fire1");
        
        if(IsPhoneDown) 
            handAnimator.SetBool("HandDown",true);
        else
            handAnimator.SetBool("HandDown",false);
    }

    private void FixedUpdate()
    {
        if (performJump)
        {
            myRb.velocity = new Vector3(0, jumpForce, 0);
            performJump = false;
        }
    }

    private bool GetIsJumping()
    {
        return Mathf.Abs(myRb.velocity.y) > 0.1f; // Mejorable TODO
        
    }

    private void OnGameOver()
    {
        enabled = false;
    }
}
