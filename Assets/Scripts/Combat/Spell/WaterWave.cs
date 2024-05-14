using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWave : MonoBehaviour
{
    private float timer = 2.0f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            Destroy(gameObject);
        }
        rb.velocity = transform.up * 5.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && collision.isTrigger)
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(15.0f, EnemyHealth.ElementType.Water);
        }
        else if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
