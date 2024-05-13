using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    private float timer = 4.0f;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if ((collider.tag == "Enemy" || collider.tag == "Interactable") && collider.isTrigger)
        {
            collider.gameObject.GetComponent<EnemyHealth>().burning = 0.5f;
        }
    }
}
