using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealLandDamage : MonoBehaviour
{
    public GameObject damageLand;
    public float timer = 2.0f;

    private void Update()
    {
        if (timer < 0f)
        {
            damageLand.SetActive(true);
        }
        else if (timer < -1.0f)
        {
            Destroy(gameObject);
        }
    }
}
