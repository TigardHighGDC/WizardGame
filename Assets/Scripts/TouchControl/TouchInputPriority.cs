using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputPriority : MonoBehaviour
{
    public bool FightMode = false;
    public LineDrawer lineDrawer;
    public static TouchInputPriority Instance;

    private bool joystickStart = false;
    private float inverse;
    // Stores the touch id of the touch that is currently being tracked
    private int leftTouchId = -1;
    private int leftTouchIndex = -1;
    private bool leftFindTouch = true;

    private int rightTouchId = -1;
    private int rightTouchIndex = -1;
    private bool rightFindTouch = true;

    private void Start()
    {
        Instance = this;
        inverse = PlayerPrefs.GetFloat("SketchPosition", 1);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (leftFindTouch)
            {
                leftTouchIndex = -1;
                for (int i = 0; i < Input.touchCount; i++)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began &&
                        (AdjustPointToScreen(8, Input.GetTouch(i).position).x * inverse < 0.0f || !FightMode))
                    {
                        leftTouchId = Input.GetTouch(i).fingerId;
                        leftFindTouch = false;
                        break;
                    }
                }
            }

            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).fingerId == leftTouchId)
                {
                    leftTouchIndex = i;
                    break;
                }
            }

            if (rightFindTouch)
            {
                rightTouchIndex = -1;
                for (int i = 0; i < Input.touchCount; i++)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began &&
                        (AdjustPointToScreen(8, Input.GetTouch(i).position).x * inverse > 0.0f || !FightMode))
                    {
                        rightTouchId = Input.GetTouch(i).fingerId;
                        rightFindTouch = false;
                        break;
                    }
                }
            }

            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).fingerId == rightTouchId)
                {
                    rightTouchIndex = i;
                    break;
                }
            }

            if (leftTouchIndex != -1 && !leftFindTouch)
            {
                Touch leftTouch;
                try
                {
                    leftTouch = Input.GetTouch(leftTouchIndex);
                }
                catch
                {
                    leftFindTouch = true;
                    return;
                }
                // Handle finger movements based on TouchPhase
                switch (leftTouch.phase)
                {
                // When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:

                    if (DialogueBox.Instance.IsTalking)
                    {
                        DialogueBox.Instance.NextParagraph();
                        leftFindTouch = true;
                        break;
                    }

                    // Adds camera position to the touch position
                    Vector3 adjustedTouch = AdjustPointToScreen(8, leftTouch.position);
                    Collider2D[] colliders = Physics2D.OverlapPointAll(adjustedTouch + transform.position);
                    Dictionary<string, GameObject> tag = new Dictionary<string, GameObject>();
                    foreach (Collider2D collider in colliders)
                    {
                        if (!tag.ContainsKey(collider.tag))
                        {
                            tag.Add(collider.gameObject.tag, collider.gameObject);
                        }
                    }

                    if (tag.ContainsKey("NPC"))
                    {
                        DialogueStorage speech = tag["NPC"].GetComponent<DialogueStorage>();
                        DialogueBox.Instance.StartDialogue(speech.Dialogue, speech.Sprite);
                        Reset();
                    }
                    else
                    {
                        MovementJoystickStart(adjustedTouch);
                    }
                    break;
                case TouchPhase.Moved:
                    if (DialogueBox.Instance.IsTalking)
                    {
                        leftFindTouch = true;
                        break;
                    }
                    MovementJoystickMove(leftTouch);
                    break;

                case TouchPhase.Ended:
                    MovementJoystickEnd();
                    leftFindTouch = true;
                    break;

                case TouchPhase.Canceled:
                    MovementJoystickEnd();
                    leftFindTouch = true;
                    break;
                }
            }
            else
            {
                leftFindTouch = true;
                MovementJoystickEnd();
            }

            if (rightTouchIndex != -1 && !rightFindTouch)
            {
                Touch rightTouch;
                try
                {
                    rightTouch = Input.GetTouch(rightTouchIndex);
                }
                catch
                {
                    rightFindTouch = true;
                    return;
                }
                if (lineDrawer.spellStorage != "" && !DialogueBox.Instance.IsTalking)
                {
                    rightFindTouch = lineDrawer.SpellCast(rightTouch);
                    return;
                }
                // Handle finger movements based on TouchPhase
                switch (rightTouch.phase)
                {
                // When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    if (DialogueBox.Instance.IsTalking)
                    {
                        DialogueBox.Instance.NextParagraph();
                        rightFindTouch = true;
                        break;
                    }

                    // Adds camera position to the touch position
                    Vector3 adjustedTouch = AdjustPointToScreen(8, rightTouch.position);
                    Collider2D[] colliders = Physics2D.OverlapPointAll(adjustedTouch + transform.position);
                    Dictionary<string, GameObject> tag = new Dictionary<string, GameObject>();
                    foreach (Collider2D collider in colliders)
                    {
                        if (!tag.ContainsKey(collider.tag))
                        {
                            tag.Add(collider.gameObject.tag, collider.gameObject);
                        }
                    }

                    if (tag.ContainsKey("NPC"))
                    {
                        DialogueStorage speech = tag["NPC"].GetComponent<DialogueStorage>();
                        DialogueBox.Instance.StartDialogue(speech.Dialogue, speech.Sprite);
                        Reset();
                    }
                    else
                    {
                        lineDrawer.LineBegin(rightTouch);
                    }
                    break;

                case TouchPhase.Moved:
                    lineDrawer.LineMove(rightTouch);
                    break;

                case TouchPhase.Ended:
                    lineDrawer.LineEnd();
                    rightFindTouch = true;
                    break;

                case TouchPhase.Canceled:
                    lineDrawer.LineCancel();
                    rightFindTouch = true;
                    break;
                }
            }
            else
            {
                rightFindTouch = true;
                lineDrawer.LineCancel();
            }
        }
    }

    public void Reset()
    {
        lineDrawer.LineCancel();
        MovementJoystickEnd();
        rightFindTouch = true;
        leftFindTouch = true;
        lineDrawer.SpellCancel();
    }

    private void MovementJoystickStart(Vector3 position)
    {
        joystickStart = true;
        MovementJoystick.Instance.Show();
        MovementJoystick.Instance.SetJoystick(position);
        MovementJoystick.Instance.SetJoystickCenterPoint(position);
    }

    private void MovementJoystickMove(Touch touch)
    {
        if (joystickStart)
        {
            MovementJoystick.Instance.SetJoystickCenterPoint(AdjustPointToScreen(8, touch.position));
        }
    }

    private void MovementJoystickEnd()
    {
        if (joystickStart)
        {
            MovementJoystick.Instance.SetJoystickCenterPoint(
                MovementJoystick.Instance.joystick.transform.localPosition);
            MovementJoystick.Instance.Hide();
            joystickStart = false;
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
