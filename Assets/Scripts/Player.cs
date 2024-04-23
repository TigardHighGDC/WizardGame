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
        rb.velocity = MovementJoystick.Instance.GetVelocity(playerSpeed);
        anim.speed = rb.velocity.magnitude / playerSpeed;
        Vector3 direction = DirectionJoystick.Instance.GetVelocity(1.0f);
        if (direction.x != 0.0f && direction.y != 0.0f)
        {
            PlayerMovementAnimation(direction.x, direction.y);
        }
        else
        {
            PlayerMovementAnimation(rb.velocity.x, rb.velocity.y);
        }
    }

    private void PlayerMovementAnimation(float x, float y)
    {
        // Up, Down
        if (Mathf.Abs(y) > Mathf.Abs(x))
        {
            if (y > 0)
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
            if (x > 0)
            {
                anim.Play("Right");
            }
            else
            {
                anim.Play("Left");
            }
        }
    }
}
