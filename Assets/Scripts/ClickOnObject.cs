using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickOnObject : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    Debug.Log("Word");
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    Debug.Log("Bye bye");
                    break;
            }
        }
    }
    // void Update()
    // {
    //     if (Input.touchCount > 0)
    //     {
    //         Touch touch = Input.GetTouch(0);

    //         // Handle finger movements based on TouchPhase
    //         switch (touch.phase)
    //         {
    //             //When a touch has first been detected, change the message and record the starting position
    //             case TouchPhase.Began:
    //                 // Record initial touch position.
    //                 Debug.Log("Word");
    //                 break;

    //             //Determine if the touch is a moving touch
    //             case TouchPhase.Ended:
    //                 // Report that the touch has ended when it ends
    //                 Debug.Log("Bye bye");
    //                 break;
    //         }
    //     }
    // }
}