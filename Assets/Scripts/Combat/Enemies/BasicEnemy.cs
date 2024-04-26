using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public bool chasePlayer = true;
    public float speed = 1f;
    public float MaxAcceleration = 4f;

    private Rigidbody2D rb;
    private bool stunned = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        AccelerationLimit();
        if (chasePlayer && !stunned)
        {
            rb.AddForce(((Player.Instance.transform.position - transform.position).normalized), ForceMode2D.Impulse);
        }
    }

    public void StartStun(float duration = 3.0f)
    {
        StartCoroutine(Stun(duration));
    }

    private IEnumerator Stun(float duration)
    {
        stunned = true;
        yield return new WaitForSeconds(duration);
        stunned = false;
    }

    private void AccelerationLimit()
    {
        if (rb.velocity.magnitude > MaxAcceleration)
        {
            rb.velocity = rb.velocity.normalized * MaxAcceleration;
        }
    }
}
