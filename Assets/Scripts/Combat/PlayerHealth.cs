using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    public float MaxHealth = 100.0f;
    public Slider Slider;
    public Image Shield;

    private float invincability = 0.0f;

    private float currentHealth;

    private void Start()
    {
        Instance = this;
        currentHealth = MaxHealth;
    }

    private void Update()
    {
        Shield.enabled = false;
        if (invincability > 0.0f)
        {
            Shield.enabled = true;
            invincability -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        if (invincability > 0.0f)
        {
            return;
        }
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

    public void SetInvincability(float time)
    {
        invincability = time;
    }
}
