using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController
{
    private bool movement;
    [SerializeField] private float rangeMovement = 3f;
    [SerializeField] private float speed = 1f;
    private bool performJump = false;
    private Rigidbody myRB;
    [SerializeField] private float jumpForce = 10f;

    public void EnableMovement(bool movement)
    {
        this.movement = movement;
    }

    // Start is called before the first frame update
    void Start()
    {
        EnableMovement(true);
        myRB = GetComponent<Rigidbody>();
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
        return Mathf.Abs(myRB.velocity.y) > Mathf.Epsilon;
    }
}
