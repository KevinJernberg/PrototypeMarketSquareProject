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
    private int cayoteTime;

    private float _rotationVelocity;
    public float RotationSmoothTime = 0.12f;
    private float rotationSpeed = 1;

    public Transform cam;
    
    private PlayerInputActions playerInputActions;

    private Rigidbody rigidBody;

    private CharacterController controller;

    public float gravity = -15.0f;

    private Vector3 movement;
    private bool jump;
    private float _verticalVelocity;


    private void Awake()
    {
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        controller = GetComponent<CharacterController>();
        
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
        grounded = controller.isGrounded;
        Movement();
        ApplyGravity();
        Jump();
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

        if (movement != Vector3.zero)
        {
            float angle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                        cam.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref _rotationVelocity,
                RotationSmoothTime);
            
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

            Vector3 moveDirection = Quaternion.Euler(0, rotation, 0) * Vector3.forward;
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
        
        controller.Move(new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (grounded && _verticalVelocity < 0)
        {
            _verticalVelocity = 0;
        }
        _verticalVelocity += gravity * Time.deltaTime;
    }

    //Jump
    private void Jump()
    {
        if (jump && grounded)
        {
            playerInputActions.jump = false;
            Debug.Log("Jump");
            _verticalVelocity = Mathf.Sqrt(jumpForce * gravity * -1);
        }
        else
        {
            playerInputActions.jump = false;
        }
    }
}
