using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class TestMovement : MonoBehaviour
{
    //Movement
    [SerializeField] 
    private float moveSpeed;
    [SerializeField] 
    private float rotateSpeed;
    
    //Jumping
    [SerializeField] 
    private float jumpForce;
    private float jumpTimer;
    
    private bool grounded;

    private Rigidbody _rigidbody;

    public Transform cam;
    
    private PlayerInputActions playerInputActions;
    
    private CharacterController controller;

    private Vector3 movement;
    private bool jump;


    private void Awake()
    {
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        playerInputActions = GetComponent<PlayerInputActions>();
    }

    
    // Update is called once per frame
    void Update()
    {
        TimersCountdown();
        
        ReadInputs();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void TimersCountdown()
    {
        if (jumpTimer > 0f)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    private void ReadInputs()
    {
        jump = playerInputActions.jump;
        movement = new Vector3(playerInputActions.move.x, 0, playerInputActions.move.y);
    }

    private void Movement()
    {
        Vector3 inputDirection = new Vector3(movement.x, 0.0f, movement.z);
        float angle = cam.rotation.eulerAngles.y;
        Debug.Log(inputDirection);
        if (inputDirection.x != 0f && inputDirection.z != 0)
        {
            angle = 90f + -90f * inputDirection.z;
            float angleReverser = 1f;
            if (inputDirection.z == -1f)
            {
                angleReverser = -1f;
            }
            angle += 45 * angleReverser * inputDirection.x;
        }
        
        
        if (movement != Vector3.zero)
        {
            _rigidbody.Move(transform.position + inputDirection * moveSpeed, q);
        }
        
    }
    
    
    //Jump
    private void DoJump(InputAction.CallbackContext obj)
    {
        if (playerInputActions.jump && jumpTimer <= 0 && controller.isGrounded)
        {
            jumpTimer = 0.1f;
            _rigidbody.AddForce(Vector3.up * jumpForce);
        }
    }
}
