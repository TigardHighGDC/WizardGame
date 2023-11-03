using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public JoyStick JoyStick;
    public float PlayerSpeed;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (JoyStick.JoystickVec.y != 0)
        {
            rb.velocity = new Vector2(JoyStick.JoystickVec.x * PlayerSpeed, JoyStick.JoystickVec.y * PlayerSpeed);
        }

        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
