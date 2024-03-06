using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputPriority : MonoBehaviour
{
    private bool joystickStart = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
            // When a touch has first been detected, change the message and record the starting position
            case TouchPhase.Began:

                if (DialogueBox.Instance.IsTalking)
                {
                    DialogueBox.Instance.NextParagraph();
                    break;
                }
                // Adds camera position to the touch position
                Collider2D[] colliders =
                    Physics2D.OverlapPointAll(AdjustPointToScreen(5, touch.position) + transform.position);
                Dictionary<string, GameObject> tag = new Dictionary<string, GameObject>();
                foreach (Collider2D collider in colliders)
                {
                    tag.Add(collider.gameObject.tag, collider.gameObject);
                }
                // priority interactions
                if (tag.ContainsKey("Item"))
                {
                    break;
                }
                else if (tag.ContainsKey("NPC"))
                {
                    DialogueStorage speech = tag["NPC"].GetComponent<DialogueStorage>();
                    DialogueBox.Instance.StartDialogue(speech.Dialogue, speech.Sprite);
                }
                else if (tag.ContainsKey("Quit"))
                {
                    Debug.Log("Quit");
                    Application.Quit();
                }
                // if nothing clicked move joystick to touch position
                else
                {
                    joystickStart = true;
                    MovementJoystick.Instance.Show();
                    Vector3 position = AdjustPointToScreen(5, touch.position);
                    MovementJoystick.Instance.SetJoystick(position);
                    MovementJoystick.Instance.SetJoystickCenterPoint(position);
                }
                break;
            case TouchPhase.Moved:
                if (joystickStart)
                {
                    MovementJoystick.Instance.SetJoystickCenterPoint(AdjustPointToScreen(5, touch.position));
                }
                break;

            // Determine if the touch is a moving touch
            case TouchPhase.Ended:
                if (joystickStart)
                {
                    MovementJoystick.Instance.SetJoystickCenterPoint(
                        MovementJoystick.Instance.joystick.transform.localPosition);
                    MovementJoystick.Instance.Hide();
                    joystickStart = false;
                }
                break;
            }
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
