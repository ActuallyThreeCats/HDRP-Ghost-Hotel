using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class ThirdPersonCam : MonoBehaviour
{
    private GameObject orientation;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private MouseLook mouseLook;
    private float mouseX, mouseY;


    private void Start()
    {
        //inputManager = gameObject.GetComponent<InputManager>();
        //mouseLook = gameObject.GetComponent<MouseLook>();
    }
    private void Update()
    {
        GetComponent<CinemachineFreeLook>().m_XAxis.Value = GetComponent<CinemachineFreeLook>().m_XAxis.Value += mouseX;
    }

    public void ReceiveInput(Vector2 vector2, bool isMouse)
    {
        mouseX = vector2.x;
        mouseY = vector2.y;

        if (isMouse)
        {

            if (inputManager.thirdPerson.Priority > inputManager.firstPerson.Priority)
            {
                inputManager.thirdPerson.m_XAxis.m_MaxSpeed = mouseLook.mouseSensX;
                inputManager.thirdPerson.m_YAxis.m_MaxSpeed = mouseLook.mouseSensY*4;
            }

        }
        else
        {

            if (inputManager.thirdPerson.Priority > inputManager.firstPerson.Priority)
            {
                inputManager.thirdPerson.m_XAxis.m_MaxSpeed = mouseLook.controllerSensX;
                inputManager.thirdPerson.m_YAxis.m_MaxSpeed = mouseLook.controllerSensY;
            }

        }
    }
}
