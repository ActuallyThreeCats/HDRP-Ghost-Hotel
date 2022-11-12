using Cinemachine;
using UnityEngine;
public class BlueprintModeCameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    

    [SerializeField] private float movementSpeed;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float fastSpeed;
    [SerializeField] private float movementTime;
    [SerializeField] private float rotationAmount;
    [SerializeField] private Vector3 zoomAmount;
    [SerializeField] private InputManager inputManager;
    bool isMoving;
    public bool isCurrentCamera;
    [SerializeField] private Vector3 newPosition;
    [SerializeField] private Quaternion newRotation;
    [SerializeField] private Vector3 newZoom;
    [SerializeField] private Vector3 defaultOffset;
    [SerializeField] private Vector3 zoomClamp;
    public CinemachineVirtualCamera cam;

    public bool isInBlueprintMode;
    public bool isOpeningBlueprintMode;

    private CinemachineTransposer transposer;


    float timeElapsed;
    float delayTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = defaultOffset;

        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = transposer.m_FollowOffset;
        inputManager.controls.Default.EnterBlueprintMode.performed += BlueprintMode_performed;
        inputManager.controls.BlueprintMap.Cancel.started += Cancel_performed;
        inputManager.controls.BlueprintMap.Zoom.performed += Zoom_performed;
    }

    private void OnApplicationQuit()
    {
        transposer.m_FollowOffset = defaultOffset;

    }

    private void Zoom_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(obj.ReadValue<Vector2>().y != 0)
        {
         int y = 0;
            //Debug.Log(obj.ReadValue<Vector2>().y);
            if(obj.ReadValue<Vector2>().y > 0)
            {
                y = 1;
            }
            else if (obj.ReadValue<Vector2>().y < 0)
            {
                y = -1;
            }
            //Debug.Log(y);
            newZoom += (y * zoomAmount);

            if(newZoom.y > zoomClamp.y)
            {
                newZoom.y = zoomClamp.y;
            }
            if(newZoom.y < 0)
            {
                newZoom.y =0;
            }
        }
    }

    private void BlueprintMode_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isOpeningBlueprintMode = true;
        //isInBlueprintMode = true;
        cam.Priority = 3;
        isCurrentCamera = true;
        inputManager.controls.Default.Disable();
        inputManager.controls.BlueprintMap.Enable();
        
    }
    private void Cancel_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!isOpeningBlueprintMode)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            inputManager.controls.BlueprintMap.Disable();
            inputManager.controls.Default.Enable();
            cam.Priority = 0;
            isCurrentCamera = false;

        }

    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= delayTime)
        {
            timeElapsed = 0;
            isOpeningBlueprintMode = false;
        }
        if (cam.Priority == 3)
        {
            if (inputManager.controls.BlueprintMap.ModifySpeed.IsInProgress())
            {
                movementSpeed = fastSpeed;
            }
            else
            {
                movementSpeed = normalSpeed;
            }

            if (inputManager.controls.BlueprintMap.Move.ReadValue<Vector2>().x != 0)
            {
                //Debug.Log(inputManager.controls.BlueprintMap.Move.ReadValue<Vector2>().x);
                newPosition += (transform.right * (inputManager.controls.BlueprintMap.Move.ReadValue<Vector2>().x * movementSpeed));
            }
            if (inputManager.controls.BlueprintMap.Move.ReadValue<Vector2>().y != 0)
            {
                //Debug.Log(inputManager.controls.BlueprintMap.Move.ReadValue<Vector2>().y);

                newPosition += (transform.forward * (inputManager.controls.BlueprintMap.Move.ReadValue<Vector2>().y * movementSpeed));
            }

            if(inputManager.controls.BlueprintMap.RotateCamera.ReadValue<Vector2>().x != 0)
            {
                //Debug.Log(inputManager.controls.BlueprintMap.RotateCamera.ReadValue<Vector2>().x + ", " + inputManager.controls.BlueprintMap.RotateCamera.ReadValue<Vector2>().y);
                newRotation *= Quaternion.Euler(Vector3.up * (inputManager.controls.BlueprintMap.RotateCamera.ReadValue<Vector2>().x * rotationAmount));
            }


            


        }
    }
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);


        transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, newZoom, Time.deltaTime * movementTime);
    }

}
