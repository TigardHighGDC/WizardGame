using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth = 100.0f;
    public float invincabilityReduction = 0.25f;
    public Slider Slider;
    public GameObject FireParticles;
    public GameObject LightningParticles;
    public GameObject WaterParticles;
    public float fireTimer = 0.0f;
    public float lightningTimer = 0.0f;
    public float waterTimer = 0.0f;

    [HideInInspector]
    public bool isInvicable;

    private float currentHealth;

    private void Start()
    {
        currentHealth = MaxHealth;
    }

    private void Update()
    {
        FireParticles.SetActive(fireTimer > 0.0f);
        LightningParticles.SetActive(lightningTimer > 0.0f);
        WaterParticles.SetActive(waterTimer > 0.0f);

        if (fireTimer > 0.0f)
        {
            TakeDamage(5.0f * Time.deltaTime);
            fireTimer -= Time.deltaTime;
        }

        if (lightningTimer > 0.0f)
        {
            lightningTimer -= Time.deltaTime;
        }

        if (waterTimer > 0.0f)
        {
            waterTimer -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isInvicable)
        {
            damage = damage * invincabilityReduction;
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

    public void ApplyFire()
    {
        fireTimer = 10.0f;
        TakeDamage(5.0f);
    }

    public void ApplyLightning()
    {
        lightningTimer = 0.5f;
        TakeDamage(15.0f);
    }

    public void ApplyWater()
    {
        waterTimer = 3.5f;
        TakeDamage(10.0f);
    }
}
