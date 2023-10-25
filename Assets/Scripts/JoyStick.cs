using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour
{
    public GameObject Joystick;
    public GameObject JoystickBG;
    public Vector2 JoystickVec;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    void Start()
    {
        joystickOriginalPos = JoystickBG.transform.position;
        joystickRadius = JoystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

    public void PointerDown()
    {
        Joystick.transform.position = Input.mousePosition;
        JoystickBG.transform.position = Input.mousePosition;
        joystickTouchPos = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        JoystickVec = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        if (joystickDist < joystickRadius)
        {
            Joystick.transform.position = joystickTouchPos + JoystickVec * joystickDist;
        }

        else
        {
            Joystick.transform.position = joystickTouchPos + JoystickVec * joystickRadius;
        }
    }

    public void PointerUp()
    {
        JoystickVec = Vector2.zero;
        Joystick.transform.position = joystickOriginalPos;
        JoystickBG.transform.position = joystickOriginalPos;
    }
}
