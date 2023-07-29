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
    private ISoundManager soundManager;
    private Rigidbody myRB;
    [SerializeField] private float jumpForce = 10f;
    private Animator handAnimator;
    private bool isPhoneDown = false;
    private IGameManager myGM;

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
        soundManager = GameObject.Find("SoundManager").GetComponent<ISoundManager>();
        myRB = GetComponent<Rigidbody>();
        handAnimator = GetComponentInChildren<Animator>();
        myGM = FindObjectOfType<GameManager>();
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
            soundManager.PlaySFX(ESFXType.Jump);
        }
        
        isPhoneDown = myGM.CanLowerHand() && Input.GetButton("Fire1");
        
        if(isPhoneDown) 
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
        soundManager.PlaySFX(ESFXType.BrokenGlass);
        //GameOverSignal.Invoke(); <--- TODO
    }
    
    public bool GetIsPhoneDown()
    {
        return isPhoneDown;
    }
}
