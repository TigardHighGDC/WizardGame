using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject FireWall;
    public GameObject SpawnPoint;

    private void Start()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.up * 10.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Enemy" || collision.tag == "Interactable") && collision.isTrigger)
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(25.0f);
            Instantiate(FireWall, SpawnPoint.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collision.tag == "Wall")
        {
            Instantiate(FireWall, SpawnPoint.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
