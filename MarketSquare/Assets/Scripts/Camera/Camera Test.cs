using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 3.0f;

    private float _rotationX;
    private float _rotationY;

    [SerializeField] private Transform _target;

    [SerializeField] private float _distanceFromTarget = 3.0f;
    
    void Update()
    {
        //Jag vet inte vad jag gör om du undrar, bara ignorera det här så länge
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _rotationX += mouseX;
        _rotationY += mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -40, 40);

        Vector3 nextRotation = new Vector3(_rotationX, _rotationY, 0);
        
        transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        
        transform.localEulerAngles = new Vector3(0, _rotationY, 0);
    }
}
