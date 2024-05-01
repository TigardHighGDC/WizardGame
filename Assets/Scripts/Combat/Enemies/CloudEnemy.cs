using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEnemy : BasicEnemy
{
    public GameObject lightningBolt;

    private float minAttack = 1.0f;
    private float maxAttack = 2.0f;
    private float attackTimer = 1.0f;

    private void Update()
    {
        attackTimer -= Time.deltaTime;
        Attack();
    }

    private void FixedUpdate()
    {
        AccelerationLimit();
        if (chasePlayer && !stunned)
        {
            rb.AddForce(((Player.Instance.transform.position - transform.position).normalized) * RunState(), ForceMode2D.Impulse);
        }
    }

    private int RunState()
    {
        if (Vector3.Distance(Player.Instance.transform.position, transform.position) < 4)
        {
            return -1;
        }
        else if (Vector3.Distance(Player.Instance.transform.position, transform.position) > 7)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private void Attack()
    {
        if (attackTimer <= 0)
        {
            attackTimer = Random.Range(minAttack, maxAttack);
            Vector3 direction = (Player.Instance.transform.position - transform.position).normalized;
            GameObject lightning = Instantiate(lightningBolt, transform.position, Quaternion.FromToRotation(transform.up, direction));
            lightning.GetComponent<Rigidbody2D>().velocity = direction * 5.0f;
        }
    }
}
