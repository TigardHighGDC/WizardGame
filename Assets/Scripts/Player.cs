using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MovementJoystick movementJoystick;
    public float playerSpeed;
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (movementJoystick.joystickVec.y != 0)
        {
            rb.velocity =
                new Vector2(movementJoystick.joystickVec.x * playerSpeed, movementJoystick.joystickVec.y * playerSpeed);
        }

        else
        {
            rb.velocity = Vector2.zero;
        }

        PlayerMovementAnimation(rb.velocity.x, rb.velocity.y);
    }

    private void PlayerMovementAnimation(float x, float y)
    {
        anim.enabled = true;
        // Up, Down
        if (Mathf.Abs(y) > Mathf.Abs(x))
        {
            if (rb.velocity.y > 0)
            {
                anim.Play("Forward");
            }
            else
            {
                anim.Play("Backward");
            }
        }
        // Left, Right
        else if (Mathf.Abs(y) < Mathf.Abs(x))
        {
            if (rb.velocity.x > 0)
            {
                anim.Play("Right");
            }
            else
            {
                anim.Play("Left");
            }
        }
        else
        {
            anim.enabled = false;
        }
    }
}
