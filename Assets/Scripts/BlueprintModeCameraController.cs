using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintModeCameraController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float fastSpeed;
    [SerializeField] private float movementTime;
    [SerializeField] private InputManager inputManager;
    bool isMoving;

    public Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        
    }



    // Update is called once per frame
    void Update()
    {
        if(inputManager.controls.Default.Sprint.IsInProgress())
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

        if(inputManager.controls.Default.Move.ReadValue<Vector2>().x != 0)
        {
            Debug.Log(inputManager.controls.Default.Move.ReadValue<Vector2>().x);
            newPosition += (transform.right * (inputManager.controls.Default.Move.ReadValue<Vector2>().x * movementSpeed));
        }
        if (inputManager.controls.Default.Move.ReadValue<Vector2>().y != 0)
        {
            Debug.Log(inputManager.controls.Default.Move.ReadValue<Vector2>().y);

            newPosition += (transform.forward * (inputManager.controls.Default.Move.ReadValue<Vector2>().y * movementSpeed));
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
    }

    
}
