using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.tag == "Enemy")
        {
            collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(15f * Time.deltaTime);
        }
    }
}
