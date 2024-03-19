using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public Animator anim;

    private float attackSpeed  = 1.0f;
    private float attackTimer = 1.5f;
    private bool attackPhase = false;
    private float attacking = 0.0f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (enemyHealth.waterTimer > 0.0f)
        {
            attackSpeed = 0.5f;
        }
        else
        {
            attackSpeed = 1.0f;
        }

        if (enemyHealth.lightningTimer > 0.0f)
        {
            attackSpeed = 0.0f;
        }

        anim.speed = attackSpeed;

        if (attackTimer > 0.0f)
        {
            attackTimer -= Time.deltaTime;
        }

        if (attackTimer <= 0.0f && !attackPhase)
        {
            attacking = 1.0f;
            attackPhase = true;
            anim.Play("Swing");
        }

        if (attacking > 0.0f)
        {
            attacking -= Time.deltaTime * attackSpeed;
        }
        else if (attackTimer <= 0.0f && attackPhase)
        {
            attackPhase = false;
            anim.Play("None");
            attackTimer = Random.Range(1.25f, 3.5f);
            PlayerHealth.Instance.TakeDamage(20.0f);
        }
    }
}
