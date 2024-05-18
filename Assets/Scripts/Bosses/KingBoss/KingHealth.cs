using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingHealth : EnemyHealth
{
    public bool invicible = false;
    public GameObject Crystal;
    public override void TakeDamage(float damage, ElementType spellElement)
    {
        if (invicible)
        {
            return;
        }
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
