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

    private float _rotationVelocity;
    public float RotationSmoothTime = 0.12f;
    private float rotationSpeed = 1;

    public Transform cam;
    
    private PlayerInputActions playerInputActions;

    private Rigidbody rigidBody;

    private Vector3 movement;
    private bool jump;


    private void Awake()
    {
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        
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
        Vector3 inputDirection = new Vector3(movement.x, 0.0f, movement.z).normalized;
        
        rigidBody.rotation = transform.rotation;

        if (movement != Vector3.zero)
        {
            float angle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                        cam.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref _rotationVelocity,
                RotationSmoothTime);
            
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            
            Vector3 inputDir = transform.forward * inputDirection.x + transform.right * inputDirection.z;

            transform.forward = Vector3.Slerp(transform.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            
            rigidBody.add
        }
        
    }
    
    
    //Jump
    private void DoJump(InputAction.CallbackContext obj)
    {
        // if (playerInputActions.jump && jumpTimer <= 0 && rigidBody)
        // {
        //     jumpTimer = 0.1f;
        // }
    }
}
