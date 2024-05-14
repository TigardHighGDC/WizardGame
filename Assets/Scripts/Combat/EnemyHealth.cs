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

    private bool isFlashing = false;

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
        if (!isFlashing)
        {
            StartCoroutine(FlashOnHit());
        }
    }

    private IEnumerator FlashOnHit()
    {
        isFlashing = true;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        for (int i = 5; i > 0; i--)
        {
            sprite.color = new Color(1f, 1f, 1f, 0.5f + (i * 0.1f));
            for (int child = 0; child < transform.childCount; child++)
            {
                gameObject.transform.GetChild(child).GetComponent<SpriteRenderer>().color =
                    new Color(1f, 1f, 1f, 0.5f + (i * 0.1f));
            }
            yield return new WaitForSeconds(0.02f);
        }

        for (int i = 0; i <= 5; i++)
        {
            sprite.color = new Color(1f, 1f, 1f, 0.5f + (i * 0.1f));
            for (int child = 0; child < transform.childCount; child++)
            {
                gameObject.transform.GetChild(child).GetComponent<SpriteRenderer>().color =
                    new Color(1f, 1f, 1f, 0.5f + (i * 0.1f));
            }
            yield return new WaitForSeconds(0.02f);
        }

        isFlashing = false;
    }
}
