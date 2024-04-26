using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    private float canAttack = 1.0f;
    private float attackCooldown = 0.5f;

    void Update()
    {
        if (attackCooldown >= 0.0f)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && attackCooldown <= 0.0f)
        {
            attackCooldown = canAttack;
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(10.0f);
        }
    }
}
