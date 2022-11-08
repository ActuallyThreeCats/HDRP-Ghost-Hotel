using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class InputManager : MonoBehaviour
{

    [SerializeField] Movement movement;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] ThirdPersonCam thirdPersonCam;

    [SerializeField] public CinemachineVirtualCamera firstPerson;
    [SerializeField] public CinemachineFreeLook thirdPerson;
    public AxisState axisState;


    PlayerControls controls;
    PlayerControls.DefaultActions actions;

    InteractSystem interactSystem;

    Vector2 horizontalInput;
    public Vector2 mouseInput;

    float thirdPersonX;
    bool isMouse;

    private void Awake()
    {
        interactSystem = gameObject.GetComponent<InteractSystem>();
        controls = new PlayerControls();
        actions = controls.Default;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //actions.Move.performed += Move_performed;
        actions.Jump.performed += _ => movement.OnJumpPressed();
        actions.Move.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        actions.Move.canceled += ctx => horizontalInput = Vector2.zero;
        actions.MouseX.performed += MouseX_performed;
        //actions.MouseX.canceled += ctx => isMouse = false;
        actions.MouseY.performed += MouseY_performed;
        //actions.MouseY.performed += ctx => isMouse = false;

        actions.SwitchCamera.performed += SwitchCamera_performed;

        actions.Interact.performed += Interact_performed;
    }

    private void MouseY_performed(InputAction.CallbackContext obj)
    {
        if (obj.control.device.displayName == "Mouse")
        {
            isMouse = true;

        }
        else
        {
            isMouse = false;
        }
        mouseInput.y = obj.ReadValue<float>();
    }

    private void MouseX_performed(InputAction.CallbackContext obj)
    {
        if(obj.control.device.displayName == "Mouse")
        {
            isMouse = true;

        }
        else
        {
            isMouse = false;
        }
        mouseInput.x = obj.ReadValue<float>();
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        interactSystem.Interact();
    }

    private void SwitchCamera_performed(InputAction.CallbackContext obj)
    {
        if(mouseInput == Vector2.zero)
        {
            if (firstPerson.Priority > thirdPerson.Priority)
            {
                thirdPerson.Priority = 2;
                firstPerson.Priority = 1;

                thirdPerson.m_RecenterToTargetHeading.m_enabled = false;



            }
            else if (thirdPerson.Priority > firstPerson.Priority)
            {
                thirdPerson.Priority = 1;
                firstPerson.Priority = 2;
                thirdPerson.m_RecenterToTargetHeading.m_enabled = true;


            }


        }
    }



    private void OnEnable()
    {
        thirdPerson.m_StandbyUpdate = CinemachineVirtualCameraBase.StandbyUpdateMode.Always;

        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (firstPerson.Priority > thirdPerson.Priority)
        {


            thirdPerson.m_RecenterToTargetHeading.m_enabled = true;

        }
        else if (thirdPerson.Priority > firstPerson.Priority)
        {

            thirdPerson.m_RecenterToTargetHeading.m_enabled = false;


        }
    }

    // Update is called once per frame
    void Update()
    {

        if (thirdPerson.Priority < firstPerson.Priority)
        {
            mouseLook.ReceiveInput(mouseInput, isMouse);
            
        }else if(thirdPerson.Priority > firstPerson.Priority)
        {

            thirdPersonCam.ReceiveInput(mouseInput, isMouse);
        }
        movement.ReceiveInput(horizontalInput);
    }
}
