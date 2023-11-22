using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnObject : MonoBehaviour
{
    // Sources : https://docs.unity3d.com/ScriptReference/TouchPhase.Began.html |
    // https://www.reddit.com/r/Unity2D/comments/hmgghi/how_to_detect_when_mouse_is_over_game_object/
    void OnMouseOver()
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
                Debug.Log("Click");
                break;

            // Determine if the touch is a moving touch
            case TouchPhase.Ended:
                // Report that the touch has ended when it ends
                Debug.Log("Opposite of Click");
                break;
            }
        }
    }
}
