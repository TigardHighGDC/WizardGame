using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovementJoystick : MonoBehaviour
{
    public static MovementJoystick Instance;
    public GameObject joystick;
    public GameObject joystickCenter;
    public float maxRadius;



    void Start()
    {
        Instance = this;
    }

    public void SetJoystick(Vector3 setPoint)
    {
        joystick.transform.position = setPoint;
    }

    public void SetJoystickCenterPoint(Vector3 setPoint)
    {
        if (maxRadius > Vector3.Distance(setPoint, joystick.transform.position))
        {
            joystickCenter.transform.position = setPoint;
        }
    }

}
