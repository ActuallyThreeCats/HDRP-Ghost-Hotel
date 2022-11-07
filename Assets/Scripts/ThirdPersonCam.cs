using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class ThirdPersonCam : MonoBehaviour
{
    private GameObject orientation;
    private float mouseX;


    private void Update()
    {
        GetComponent<CinemachineFreeLook>().m_XAxis.Value = GetComponent<CinemachineFreeLook>().m_XAxis.Value += mouseX;
    }

    public void ReceiveInput(Vector2 vector2)
    {
        mouseX = vector2.x;
    }
}
