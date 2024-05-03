using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    private void Update()
    {
        transform.position = transform.position + transform.up * 10.0f * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && collision.isTrigger)
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(25.0f);
            collision.GetComponent<BasicEnemy>().StartStun();
            Destroy(gameObject);
        }
    }
}
