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
    
    private GameObject mainCamera;
    
    private PlayerInputActions playerInputActions;
    
    private CharacterController controller;

    private InputAction movement;
    
    private float speed;
    public float speedChangeRate = 10.0f;
    public float RotationSmoothTime = 0.12f;
    private float _targetRotation = 0.0f;
    
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;
    
    
    private void Awake()
    {
        // get a reference to our main camera
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        
        controller = GetComponent<CharacterController>();
        playerInputActions = GetComponent<PlayerInputActions>();
    }

    
    // Update is called once per frame
    void Update()
    {
        TimersCountdown();
        
        GroundedCheck();
        
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
    
    //Movement input
    // ReSharper disable Unity.PerformanceAnalysis
    private void Movement()
    {
        // set target speed based on move speed, sprint speed and if sprint is pressed
        float targetSpeed = moveSpeed;

        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0
        if (playerInputActions.move == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = playerInputActions.analogMovement ? playerInputActions.move.magnitude : 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * speedChangeRate);

            // round speed to 3 decimal places
            speed = Mathf.Round(speed * 1000f) / 1000f;
        }
        else
        {
            speed = targetSpeed;
        }

        // normalise input direction
        Vector3 inputDirection = new Vector3(playerInputActions.move.x, 0.0f, playerInputActions.move.y).normalized;
        if (playerInputActions.move != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }


        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        // move the player
        controller.Move(targetDirection.normalized * (speed * Time.deltaTime) +
                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        
        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (!grounded && _verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += -9.82f * Time.deltaTime;
        }
    }
    
    //Ground Check
    private void GroundedCheck()
    {
        //Raycast to se if anything is directly below you
        if (Physics.Raycast(transform.position, Vector3.down, 1.02f))
        {
            grounded = true;
        }
        else //Nothing directly below you
        {
            grounded = false;
        }
    }
    
    //Jump
    private void DoJump(InputAction.CallbackContext obj)
    {
        if (jumpTimer <= 0 && grounded)
        {
            jumpTimer = 0.1f;
            _rigidbody.AddForce(Vector3.up * jumpForce);
        }
    }
}
