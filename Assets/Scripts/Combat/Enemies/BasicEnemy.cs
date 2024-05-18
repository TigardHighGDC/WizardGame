using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public bool chasePlayer = true;
    public float speed = 1f;
    public float MaxAcceleration = 4f;
    public int directionSwap = 1;

    protected Rigidbody2D rb;
    protected bool stunned = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stunned = false;
    }

    private void FixedUpdate()
    {
        AccelerationLimit();
        if (chasePlayer)
        {
            rb.AddForce(((Player.Instance.transform.position - transform.position).normalized), ForceMode2D.Impulse);
        }
        if (stunned)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Update()
    {
        Vector3 direction = Player.Instance.transform.position - transform.position;
        if (direction.x > 0.0f)
        {
            transform.localScale = new Vector3(1 * directionSwap, 1, 1);
        }
        else if (direction.x < 0.0f)
        {
            transform.localScale = new Vector3(-1 * directionSwap, 1, 1);
        }
    }

    public void StartStun(float duration = 3.0f)
    {
        StartCoroutine(Stun(duration));
    }

    protected IEnumerator Stun(float duration)
    {
        stunned = true;
        yield return new WaitForSeconds(duration);
        stunned = false;
    }

    protected void AccelerationLimit()
    {
        if (rb.velocity.magnitude > MaxAcceleration)
        {
            rb.velocity = rb.velocity.normalized * MaxAcceleration;
        }
    }
}
