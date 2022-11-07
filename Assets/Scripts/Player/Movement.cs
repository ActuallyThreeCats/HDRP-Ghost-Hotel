using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] InputManager input;
    [SerializeField] float speed = 11f;
    [SerializeField] float rotSpeed =4f;
    [SerializeField] float jumpHeight = 3.5f;
    bool jump;
    [SerializeField] float gravity = -30f;
    Vector3 verticalVelocity = Vector3.zero;

    [SerializeField] LayerMask groundMask;
    [SerializeField] Transform cameraMain;
    bool isGrounded;

    Vector2 horizontalInput;
    Vector3 horizontalVelocity;
    [SerializeField] private float predictionTime;
    [SerializeField] private int iterationsPerFrame;
    NavMeshBuildSettings buildSettings;

    private void Awake()
    {
        buildSettings = new NavMeshBuildSettings();
        UpdateNavmesh();
    }
    public void ReceiveInput(Vector2 _horizontalInput)
    {

        horizontalInput = _horizontalInput;
    }

    public void UpdateNavmesh()
    {
        NavMesh.avoidancePredictionTime = predictionTime;
        NavMesh.pathfindingIterationsPerFrame = iterationsPerFrame;
        buildSettings.overrideTileSize = true;
        buildSettings.overrideVoxelSize = true;

        buildSettings.tileSize = 1024;
        buildSettings.voxelSize = 0.01f;
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
        if (isGrounded)
        {
            verticalVelocity.y = 0;
        }

        if (input.firstPerson.Priority > input.thirdPerson.Priority)
        {
            horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;


        }else if(input.firstPerson.Priority < input.thirdPerson.Priority)
        {
            horizontalVelocity = new Vector3(horizontalInput.x, 0, horizontalInput.y)* speed;
            horizontalVelocity = cameraMain.forward * horizontalVelocity.z + cameraMain.transform.right * horizontalVelocity.x;
            horizontalVelocity.y = 0f;
            if (horizontalInput != Vector2.zero)
            {
                float targetAngle = Mathf.Atan2(horizontalInput.x, horizontalInput.y) * Mathf.Rad2Deg + cameraMain.eulerAngles.y;
                Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotSpeed);
            }
        }

        controller.Move(horizontalVelocity * Time.deltaTime);
    
        if(jump && isGrounded)
        {
            verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            jump = false;
        }



        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    public void OnJumpPressed()
    {
        jump = true;
    }
}
