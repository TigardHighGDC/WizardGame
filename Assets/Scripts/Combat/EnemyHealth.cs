using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth = 100.0f;

    public ElementType element = ElementType.None;

    [HideInInspector]
    public float burning = 0.0f;

    [HideInInspector]
    public float currentHealth;

    protected bool isFlashing = false;

    private void Start()
    {
        currentHealth = MaxHealth;
    }
    private void Update()
    {
        if (burning > 0.0f)
        {
            burning -= Time.deltaTime;
            TakeDamage(10f * Time.deltaTime, ElementType.Fire);
        }
    }
    public virtual void TakeDamage(float damage, ElementType spellElement)
    {
        currentHealth -= damage * ElementTypeMultiplier(spellElement, element);
        if (currentHealth <= 0.0f)
        {
            Destroy(gameObject);
        }
        if (!isFlashing)
        {
            StartCoroutine(FlashOnHit());
        }
    }

    protected IEnumerator FlashOnHit()
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

    protected float ElementTypeMultiplier(ElementType spellElement, ElementType enemyElement)
    {
        switch (spellElement)
        {
        case ElementType.Air:
            switch (enemyElement)
            {
            case ElementType.Fire:
                return 0.75f;
            case ElementType.Earth:
                return 1.25f;
            default:
                return 1.0f;
            }
        case ElementType.Fire:
            switch (enemyElement)
            {
            case ElementType.Water:
                return 0.75f;
            case ElementType.Air:
                return 1.25f;
            default:
                return 1.0f;
            }
        case ElementType.Water:
            switch (enemyElement)
            {
            case ElementType.Earth:
                return 0.75f;
            case ElementType.Fire:
                return 1.25f;
            default:
                return 1.0f;
            }
        case ElementType.Earth:
            switch (enemyElement)
            {
            case ElementType.Air:
                return 0.75f;
            case ElementType.Water:
                return 1.25f;
            default:
                return 1.0f;
            }
        default:
            return 1.0f;
        }
    }

    public enum ElementType
    {
        Air,
        Fire,
        Water,
        Earth,
        None
    }
}
