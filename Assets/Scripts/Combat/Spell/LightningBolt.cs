using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    private void Start()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.up * 10.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && collision.isTrigger)
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(35.0f);
            if (collision.TryGetComponent<BasicEnemy>(out BasicEnemy enemy))
            {
                enemy.StartStun();
            }
            Destroy(gameObject);
        }
        else if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
