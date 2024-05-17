using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider Slider;
    public Gradient Gradient;
    public Image Fill;
    public EnemyHealth enemyHealth;

    private void Start()
    {
        SetMaxHealth(enemyHealth.MaxHealth);
        SetHealth(enemyHealth.currentHealth);
    }

    private void Update()
    {
        SetHealth(enemyHealth.currentHealth);
    }

    // Set the number that it considered the max.
    public void SetMaxHealth(float maxHealth)
    {
        Slider.maxValue = maxHealth;

        Fill.color = Gradient.Evaluate(1f);
    }

    // When you take damage fill will shrink.
    public void SetHealth(float currentHealth)
    {
        Slider.value = currentHealth;

        Fill.color = Gradient.Evaluate(Slider.normalizedValue);
    }
}
