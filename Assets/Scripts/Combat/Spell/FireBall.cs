using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject FireWall;

    private void Update()
    {
        transform.position = transform.position + transform.up * 10.0f * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && collision.isTrigger)
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(25.0f);
            Instantiate(FireWall, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
