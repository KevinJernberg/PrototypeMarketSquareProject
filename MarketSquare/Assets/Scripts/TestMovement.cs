using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private PlayerInput playerControls;
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerControls = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    
    // Update is called once per frame
    void Update()
    {
        TimersCountdown();
        
        GroundedCheck();
        
        Movement();
        Jump();
    }

    private void TimersCountdown()
    {
        if (jumpTimer > 0f)
        {
            jumpTimer -= Time.deltaTime;
        }
    }
    
    //Movement input
    private void Movement()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            transform.Translate(Vector3.forward *(moveSpeed * Time.deltaTime * Input.GetAxis("Vertical")));
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
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
    private void Jump()
    {
        if (jumpTimer <= 0 && Input.GetButton("Jump") && grounded)
        {
            jumpTimer = 0.1f;
            _rigidbody.AddForce(Vector3.up * jumpForce);
        }
    }
}
