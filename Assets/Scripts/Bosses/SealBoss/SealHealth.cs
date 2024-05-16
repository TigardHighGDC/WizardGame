using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealHealth : EnemyHealth
{
    public SealController controller;

    public float HealthLossTrigger = 100.0f;
    private float healthLoss;

    private void Start()
    {
        currentHealth = MaxHealth;
        healthLoss = currentHealth - HealthLossTrigger;
    }

    public override void TakeDamage(float damage, ElementType spellElement)
    {
        currentHealth -= damage * ElementTypeMultiplier(spellElement, element);
        if (currentHealth <= healthLoss)
        {
            healthLoss = currentHealth - HealthLossTrigger;
            controller.firstDive = true;
            controller.canDive = true;
        }
        if (currentHealth <= 0.0f)
        {
            Destroy(gameObject);
        }
        if (!isFlashing)
        {
            StartCoroutine(FlashOnHit());
        }
    }
}
