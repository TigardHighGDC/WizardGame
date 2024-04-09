using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public Animator anim;

    [Header("Quick Attack")]
    public float attackTimer = 1.5f;
    public float quickAttackDamage = 15.0f;
    public List<string> quickAttackAnims = new List<string>();

    [Header("Heavy Attack")]
    public float heavyAttackTimer = 2.0f;
    public float heavyAttackDamage = 30.0f;
    public List<string> heavyAttackAnims = new List<string>();

    [Header("Feint")]
    public float feintTimer = 2.0f;
    public List<string> feintAnims = new List<string>();

    [Header("Block")]
    public float blockTimer = 2.0f;
    public List<string> blockAnims = new List<string>();

    private float attackSpeed = 1.0f;
    private bool attackPhase = false;
    private float waitTimer = 2.0f;
    private float attacking = 0.0f;
    private int attackType;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        // Time Effects
        if (enemyHealth.waterTimer > 0.0f)
        {
            attackSpeed = 0.5f;
        }
        else
        {
            attackSpeed = 1.0f;
        }

        if (enemyHealth.lightningTimer > 0.0f)
        {
            attackSpeed = 0.0f;
        }

        anim.speed = attackSpeed;

        if (waitTimer > 0.0f)
        {
            waitTimer -= Time.deltaTime * attackSpeed;
        }

        if (attacking > 0.0f)
        {
            attacking -= Time.deltaTime * attackSpeed;
        }

        if (waitTimer <= 0.0f && !attackPhase)
        {
            attackPhase = true;
            attackType = attackChoice();
            Debug.Log(attackType);
            switch (attackType)
            {
            case 0:
                anim.Play(Choice(quickAttackAnims));
                attacking = attackTimer;
                waitTimer = Random.Range(1.0f, 2.0f) + attackTimer;
                break;
            case 1:
                anim.Play(Choice(heavyAttackAnims));
                attacking = heavyAttackTimer;
                waitTimer = Random.Range(1.0f, 2.0f) + heavyAttackTimer;
                break;
            case 2:
                anim.Play(Choice(feintAnims));
                attacking = feintTimer;
                waitTimer = Random.Range(0.1f, 0.4f) + feintTimer;
                break;
            case 3:
                anim.Play(Choice(blockAnims));
                attacking = blockTimer;
                enemyHealth.isInvicable = true;
                waitTimer = Random.Range(0.5f, 1.5f) + blockTimer;
                break;
            }
        }

        if (attackPhase == true && attacking <= 0.0f)
        {
            switch (attackType)
            {
            case 0:
                PlayerHealth.Instance.TakeDamage(quickAttackDamage);
                break;
            case 1:
                PlayerHealth.Instance.TakeDamage(quickAttackDamage);
                break;
            case 3:
                enemyHealth.isInvicable = false;
                break;
            }
            attackPhase = false;
        }
    }

    private int attackChoice()
    {
        int type = 0;
        bool invalidAttack = true;
        while (invalidAttack)
        {
            type = Random.Range(0, 4);
            switch (type)
            {
            case 0:
                if (attackTimer > 0.0f)
                {
                    invalidAttack = false;
                }
                break;
            case 1:
                if (heavyAttackTimer > 0.0f)
                {
                    invalidAttack = false;
                }
                break;
            case 2:
                if (feintTimer > 0.0f)
                {
                    invalidAttack = false;
                }
                break;
            case 3:
                if (blockTimer > 0.0f)
                {
                    invalidAttack = false;
                }
                break;
            }
        }
        return type;
    }

    private string Choice(List<string> names)
    {
        return names[Random.Range(0, names.Count)];
    }
}
