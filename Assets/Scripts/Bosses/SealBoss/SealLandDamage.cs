using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealLandDamage : MonoBehaviour
{
    public GameObject damageLand;
    private float timer = 2.0f;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            damageLand.SetActive(true);
            damageLand.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.1f);
        }
        if (timer < -1.25f)
        {
            Destroy(gameObject);
        }
    }
}
