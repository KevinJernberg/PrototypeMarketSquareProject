using Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTest : MonoBehaviour
{
    
    private GameObject _mainCamera;
    
    public GameObject CinemachineCameraTarget;
    
    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    
    public float CameraAngleOverride = 0.0f;
    
    private PlayerInputActions playerInputActions;

    private InputAction look;
    
    private void Awake()
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }
    
    
    private void Start()
    {
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
        
        playerInputActions = GetComponent<PlayerInputActions>();
    }
    
    private void FixedUpdate()
    {
        Look();
    }

    private void Look()
    {
        // if there is an input and camera position is not fixed
        if (playerInputActions.look.sqrMagnitude >= 0.01f)
        {

            _cinemachineTargetYaw += playerInputActions.look.x * 2;
            _cinemachineTargetPitch += playerInputActions.look.y * 2;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, -30, 70);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }
    
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }


//     [SerializeField] private float _mouseSensitivity = 3.0f;
//
//     private float _rotationX;
//     private float _rotationY;
//     
//     // cinemachine
//     private float _cinemachineTargetYaw;
//     private float _cinemachineTargetPitch;
//
//     [SerializeField] private Transform _target;
//
//     [SerializeField] private float _distanceFromTarget = 3.0f;
//     
//     void Update()
//     {
//         //Jag vet inte vad jag gör om du undrar, bara ignorera det här så länge  
//         // JAG KOMMER GRÅTA
//         
//         //No problemo, jag fixar detta snarast. Ingen oro. --- Kevin
//         
//         float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
//         float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;
//
//         _rotationX += mouseX;
//         _rotationY += mouseY;
//         _rotationX = Mathf.Clamp(_rotationX, -40, 40);
//
//         Vector3 nextRotation = new Vector3(_rotationX, _rotationY, 0);
//         
//         transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
//         
//         transform.localEulerAngles = new Vector3(0, _rotationY, 0);
//     }

}
