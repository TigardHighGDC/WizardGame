using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth = 100.0f;

    [HideInInspector]
    public float burning = 0.0f;

    [HideInInspector]
    public float currentHealth;

    private void Start()
    {
        currentHealth = MaxHealth;
    }
    private void Update()
    {
        if (burning > 0.0f)
        {
            burning -= Time.deltaTime;
            TakeDamage(10f * Time.deltaTime);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
