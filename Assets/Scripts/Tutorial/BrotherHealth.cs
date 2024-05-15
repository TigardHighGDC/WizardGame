using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherHealth : EnemyHealth
{
    public ElementType hitBy;

    public override void TakeDamage(float damage, ElementType spellElement)
    {
        hitBy = spellElement;
        if (!isFlashing)
        {
            StartCoroutine(FlashOnHit());
        }
    }
}
