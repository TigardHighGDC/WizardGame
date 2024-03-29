using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth = 100.0f;
    public Slider Slider;

    private float currentHealth;

    private void Start()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0.0f)
        {
            Slider.value = 0.0f;
            Destroy(gameObject);
        }
        else
        {
            Slider.value = currentHealth / MaxHealth;
        }
    }
}
