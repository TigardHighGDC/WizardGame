using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : MonoBehaviour
{
    private float duration = 3.0f;

    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (duration <= 2.7f)
        {
            return;
        }

        if (collision.tag == "Enemy" && collision.isTrigger)
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(20.0f, EnemyHealth.ElementType.Earth);
            if (collision.TryGetComponent<BasicEnemy>(out BasicEnemy enemy))
            {
                enemy.StartStun(2.0f);
            }
            Knockback(collision.gameObject, Player.Instance);
        }
    }

    private void Knockback(GameObject enemy, GameObject player)
    {
        Vector2 direction = enemy.transform.position - player.transform.position;
        enemy.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 20.0f, ForceMode2D.Impulse);
    }
}
