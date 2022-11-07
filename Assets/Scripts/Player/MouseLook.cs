using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensX = 16f;
    [SerializeField] float sensY = 0.5f;
    [SerializeField] float mouseSensX;
    [SerializeField] float mouseSensY;
    InputManager input;
    float mouseX, mouseY;

    [SerializeField] Transform playerCamera;
    [SerializeField] float xClamp = 80f;
    float xRotation = 0f;

    private void Awake()
    {
        input = gameObject.GetComponent<InputManager>();
    }
    private void Update()
    {
        if (input.firstPerson.Priority > input.thirdPerson.Priority)
        {
            transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
            Vector3 targetRotation = transform.eulerAngles;
            targetRotation.x = xRotation;

            playerCamera.eulerAngles = targetRotation;


        }
    }

    public void ReceiveInput(Vector2 mouseInput, bool isMouse)
    {
        if (isMouse)
        {
            mouseX = mouseInput.x * mouseSensX;
            mouseY = mouseInput.y * mouseSensY;
        }
        else
        {
            mouseX = mouseInput.x * sensX;
            mouseY = mouseInput.y * sensY;

        }

    }
}
