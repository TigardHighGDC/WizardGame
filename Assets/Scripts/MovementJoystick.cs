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
        if (maxRadius < Vector3.Distance(setPoint, joystick.transform.position))
        {
            Vector3 offset = setPoint - joystick.transform.position;
            offset.Normalize();
            offset.x *= maxRadius;
            offset.y *= maxRadius;
            joystickCenter.transform.position = this.transform.position + offset;
        }
        else
        {
            joystickCenter.transform.position = setPoint;
        }
    }

    public Vector3 GetVelocity(float maxSpeed)
    {
        Vector3 offset = joystickCenter.transform.position - joystick.transform.position;
        offset.Normalize();
        float speed =
            maxSpeed * (Vector3.Distance(joystick.transform.position, joystickCenter.transform.position) / maxRadius);
        return offset * speed;
    }

    public void Show()
    {
        joystick.GetComponent<SpriteRenderer>().enabled = true;
        joystickCenter.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Hide()
    {
        joystick.GetComponent<SpriteRenderer>().enabled = false;
        joystickCenter.GetComponent<SpriteRenderer>().enabled = false;
    }
}
