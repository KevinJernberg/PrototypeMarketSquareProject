using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    [SerializeField] 
    private float moveSpeed;
    
    [SerializeField] 
    private float rotateSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
}
