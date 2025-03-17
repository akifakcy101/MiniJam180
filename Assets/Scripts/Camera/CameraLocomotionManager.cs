using Unity.VisualScripting;
using UnityEngine;


//VERSION 1.0 
//YAVUZ SELÝM KÝBÝROÐLU

public class CameraLocomotionManager : MonoBehaviour
{
    [Header("Camera Transform Values")]
    [SerializeField] private Transform cameraPivotTransform;


    [Header("Camera Movement Locomotion Variables")]
    [SerializeField] private float cameraMovementSpeedWithKeyboard;
    [SerializeField] private float cameraMovementSpeedWithMouse;
    private float currentMovementSpeedType;
    [SerializeField] private float cameraSmoothDampValue;
    private Vector3 smoothDampVelocity;

    [Header("Camera Rotation Locomotion Variables")]
    [SerializeField] private float cameraRotationSpeedWithKeyboard;
    [SerializeField] private float cameraRotationSpeedWithMouse;
    private float currentRotationSpeedType;
    [SerializeField] private float mouseRotationMinClamp;
    [SerializeField] private float mouseRotationMaxClamp;

    [Header("Camera Zoom Locomotion Values")]
    public float maxZoomValue;
    public float minZoomValue;
    public float zoomSpeed;
    private float zoomAmount = 5f;


    private void Awake()
    {
 
    }

    private void Start()
    {
        SetTheMovementVelocityType();
        SetTheRotationVelocityType();
    }

    //HANDLE ALL CAMERA FUNCTIONALITY LIKE POSITION,ROTATION ECT.
    private void HandleAllCameraActions()
    {
        ChangeCameraPositionAccordingToInput();
        HandleAllRotationActions();
        HandleAllZoomActions();
    }

    //HANDLING THE CAMERA POSITION ACCORDING TO INPUT
    private void ChangeCameraPositionAccordingToInput()
    {
        //GET THE DESIRED POSITION THEN SET IT TO CAMERAS PIVOT TRANSFORM POSITIONS
        //CAN REQUIRE TIME.DELTATIME
        //SmoothDampli yap
        cameraPivotTransform.position += cameraPivotTransform.right.normalized * PlayerInputManager.instance.cameraHorizontalInputValue * currentMovementSpeedType * Time.deltaTime;
        cameraPivotTransform.position += cameraPivotTransform.forward.normalized * PlayerInputManager.instance.cameraVerticalInputValue * currentMovementSpeedType * Time.deltaTime;
    }

    //SELECT THE MOVEMENT VELOCITY TYPE ACCORDING TO MOUSE DRAG OR KEYBOARD
    private void SetTheMovementVelocityType()
    {
        PlayerInputManager.instance.playerInput.Player.LeftMouse.performed += i => currentMovementSpeedType = cameraMovementSpeedWithMouse;
        PlayerInputManager.instance.playerInput.Player.LeftMouse.canceled += i => currentMovementSpeedType = cameraMovementSpeedWithKeyboard;
    }

    //HANDLING THE CAMERA ROTATION ACCORDING TO INPUT
    private void ChangeCameraRotationAccordingToInput()
    {
        cameraPivotTransform.Rotate(Vector3.up, PlayerInputManager.instance.cameraRotationInputValues * -currentRotationSpeedType);
    }

    //CLAMP THE VALUE WHICH COMES FROM INPUT MANAGER
    private void ClampTheMouseRotationVectorValue()
    {
        PlayerInputManager.instance.cameraRotationInputValues = Mathf.Clamp(PlayerInputManager.instance.cameraRotationInputValues, mouseRotationMinClamp, mouseRotationMaxClamp);
    }

    //THIS WILL HANDLE ALL ROTATION WORKS
    private void HandleAllRotationActions()
    {
        ClampTheMouseRotationVectorValue();
        ChangeCameraRotationAccordingToInput();
    }

    //SELECT THE ROTATION VELOCITY TYPE ACCORDING TO MOUSE OR KEYBOARD
    private void SetTheRotationVelocityType()
    {
        PlayerInputManager.instance.playerInput.Player.RightMouse.performed += i => currentRotationSpeedType = cameraRotationSpeedWithMouse;
        PlayerInputManager.instance.playerInput.Player.RightMouse.canceled += i => currentRotationSpeedType = cameraRotationSpeedWithKeyboard;
    }


    //HANDLE ALL CAMERA ZOOM ACTIONS
    private void HandleAllZoomActions()
    {
        ClampTheZoomValues();
        HandleCameraZoom();
    }
    

    //CLAMP THE CAMERA ZOOM VALUE
    private void ClampTheZoomValues()
    {
        zoomAmount = Mathf.Clamp(zoomAmount, minZoomValue, maxZoomValue);
    }

    //HANDLE CAMERA ZOOM ACTION
    private void HandleCameraZoom()
    {
        zoomAmount += PlayerInputManager.instance.cameraZoomInputValue * zoomSpeed;
        transform.localPosition = new Vector3(0, 0, zoomAmount);
    }


    //CAMERA ACTIONS WORKS IN THE LATE UPDATE
    private void LateUpdate()
    {
        HandleAllCameraActions();
        
    }
}
