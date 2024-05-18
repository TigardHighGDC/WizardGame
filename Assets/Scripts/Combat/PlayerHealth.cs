using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    [HideInInspector]
    public float MaxHealth;
    public Slider Slider;
    public GameObject Shield;

    private float invincability = 0.0f;

    private float currentHealth;

    private void Start()
    {
        MaxHealth = PlayerPrefs.GetFloat("Health", 100.0f);
        Instance = this;
        currentHealth = MaxHealth;
    }

    private void Update()
    {
        Shield.SetActive(false);
        if (invincability > 0.0f)
        {
            Shield.SetActive(true);
            invincability -= Time.deltaTime;
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }
        Slider.value = currentHealth / MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (invincability > 0.0f)
        {
            damage /= 2.0f;
        }
        currentHealth -= damage;
        if (currentHealth <= 0.0f)
        {
            Slider.value = 0.0f;
            SceneManager.LoadScene("Died");
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
