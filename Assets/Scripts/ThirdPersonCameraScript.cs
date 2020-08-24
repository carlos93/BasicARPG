using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class ThirdPersonCameraScript : MonoBehaviour
{
    private CinemachineFreeLook cam;

    void Start()
    {
        cam = GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {
        cam.m_YAxis.m_InputAxisName = IsMousePressed() ? "Mouse Y" : "";
        cam.m_XAxis.m_InputAxisName = IsMousePressed() ? "Mouse X" : "";
    }

    bool IsMousePressed()
    {
        return Input.GetMouseButton(0) || Input.GetMouseButton(1);
    }
}