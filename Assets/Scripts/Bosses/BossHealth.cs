using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    public GameObject Crystal;

    public override void TakeDamage(float damage, ElementType spellElement)
    {
        currentHealth -= damage * ElementTypeMultiplier(spellElement, element);
        if (currentHealth <= 0.0f)
        {
            Instantiate(Crystal, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (!isFlashing)
        {
            StartCoroutine(FlashOnHit());
        }
    }
}
