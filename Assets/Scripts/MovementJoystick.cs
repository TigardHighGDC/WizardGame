using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovementJoystick : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec;

    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    void Start()
    {
        // Joystick starting position
        joystickOriginalPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

    void Update() // Collider2D other
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
            // When a touch has first been detected, change the message and record the starting position
            case TouchPhase.Began:
                // Record initial touch position.
                Collider2D[] colliders = Physics2D.OverlapPointAll(AdjustPointToScreen(5, touch.position));
                HashSet<string> tag = new HashSet<string>();
                foreach (Collider2D collider in colliders)
                {
                    tag.Add(collider.gameObject.tag);
                }
                // priority interactions
                if (tag.Contains("Item"))
                {
                    Debug.Log("Item");
                }
                else if (tag.Contains("NPC"))
                {
                    Debug.Log("NPC");
                }
                else if (tag.Contains("Quit"))
                {
                    Debug.Log("Quit");
                    Application.Quit();
                }
                // if nothing clicked move joystick to touch position
                else
                {
                    joystick.transform.position = touch.position;
                    joystickBG.transform.position = touch.position;
                    joystickTouchPos = touch.position;
                }
                break;

            // Determine if the touch is a moving touch
            case TouchPhase.Ended:
                // Report that the touch has ended when it ends
                joystickVec = Vector2.zero;
                joystick.transform.position = joystickOriginalPos;
                joystickBG.transform.position = joystickOriginalPos;
                break;
            }
        }
    }

    // for the draging
    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        if (joystickDist < joystickRadius)
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickDist;
        }

        else
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;
        }
    }

    private Vector3 AdjustPointToScreen(float cameraHeight, Vector3 position)
    {
        // Center the point to the screen
        position.x = position.x - (Screen.width / 2f);
        position.y = position.y - (Screen.height / 2f);

        // Adjust the point to the camera
        position.x = (position.x / Screen.width) * cameraHeight * (Screen.width / (float)Screen.height) * 2f;
        position.y = (position.y / Screen.height) * cameraHeight * 2f;

        return position;
    }
}
