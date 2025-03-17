using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Singleton")]
    public static PlayerInputManager instance;

    [Header("Player Input")]
    [HideInInspector] public PlayerInput playerInput;



    [Header("Camera Position Input Values")]
    private Vector2 cameraInputValuesVector;
    [HideInInspector] public float cameraVerticalInputValue;
    [HideInInspector] public float cameraHorizontalInputValue;
    private bool isMouseDrag;

    [Header("Camera Rotation Input Values")]
    [HideInInspector] public float cameraRotationInputValues;
    private bool isMouseRotation = false;

    [Header("Camera Zoom Input Values")]
    [HideInInspector] public float cameraZoomInputValue;

    private void Awake()
    {
        //MADE A SINGLETON
        if (instance == null) 
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    private void OnEnable()
    {
        if(playerInput == null)
        {
            //IF THERE IS NO PLAYER INPUT THEN CREATE
            playerInput = new PlayerInput();
            playerInput.Enable();

            //TAKE THE MOVEMENT INPUTS THEN SET IT TO cameraInputValuesVector VARIABLE
            playerInput.Player.CameraMovementWithKeyboard.performed += i => { if (!isMouseDrag) cameraInputValuesVector = i.ReadValue<Vector2>(); };

            //TAKE THE ROTATION INPUTS THEN SET IT TO cameraRotationInputValues VARIABLE
            playerInput.Player.CameraRotationWithKeyboard.performed += i => { if (!isMouseRotation) cameraRotationInputValues = i.ReadValue<float>(); };

            //SET THE INPUT VALUES TO MOUSE DRAG WHEN isMouseDrag IS TRUE 
            playerInput.Player.CameraLocomotionWithMouse.performed += i => { if(isMouseDrag) { cameraInputValuesVector = -i.ReadValue<Vector2>().normalized; } if (isMouseRotation) { cameraRotationInputValues = i.ReadValue<Vector2>().x; }; };


            //TAKE THE ZOOM INPUT FROM MOUSE SCROLL
            playerInput.Player.MouseScroll.performed += i => cameraZoomInputValue = i.ReadValue<float>();


            //SET THE isMouseDrag BOOL
            playerInput.Player.LeftMouse.performed += i => isMouseDrag = true;
            playerInput.Player.LeftMouse.canceled += i => { cameraInputValuesVector = Vector3.zero; isMouseDrag = false; };

            //SET THE isMouseRotation BOOL
            playerInput.Player.RightMouse.performed += i => isMouseRotation = true;
            playerInput.Player.RightMouse.canceled += i => { cameraRotationInputValues = 0f; isMouseRotation = false; };
            
        }
    }


    //CALL ALL CAMERA INPUTS HERE
    private void SetAllCameraInputValues()
    {
        SetTheVerticalCameraInput();
        SetTheHorizontalCameraInput();
    }

    //SET THE Y AXIS OF THE VECTOR TO PUBLIC VARIABLE
    private void SetTheVerticalCameraInput()
    {
        cameraVerticalInputValue = cameraInputValuesVector.y;
    }

    //SET THE X AXIS OF THE VECTOR TO PUBLIC VARIABLE
    private void SetTheHorizontalCameraInput()
    {
        cameraHorizontalInputValue = cameraInputValuesVector.x;
    }


    private void Update()
    {
        SetAllCameraInputValues();
    }
}
