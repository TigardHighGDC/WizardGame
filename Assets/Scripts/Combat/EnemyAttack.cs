using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Animator anim;

    private float attackTimer = 1.5f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(attackTimer > 0.0f)
        {
            attackTimer -= Time.deltaTime;
        }

        if (attackTimer <= 0.0f)
        {
            attackTimer = Random.Range(1.25f, 3f);
            StartCoroutine(Attack());
        }

    }

    IEnumerator Attack()
    {
        Debug.Log("Attacking");
        anim.Play("Swing");

        yield return new WaitForSeconds(1f);
        anim.Play("None");
        PlayerHealth.Instance.TakeDamage(20f);
    }
}
