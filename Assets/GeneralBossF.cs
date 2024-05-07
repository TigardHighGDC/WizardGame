using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBossF : BasicEnemy
{
    public bool startFight = false;
    public Animator anim;
    public GameObject bullet;
    public GameObject guard;


    private bool swapScatter = false;
    private int maxShots = 20;
    private int numShots = 10;
    private bool canAttack = true;
    
    private void Update()
    {
        if (!startFight)
        {
            return;
        }

        if (canAttack && numShots > 0)
        {
            StartCoroutine(SpawnBullets());
            numShots--;
        }
        else if (canAttack)
        {
            StartCoroutine(SpawnEnemies());
            numShots = maxShots;
        }
    }

    private void FixedUpdate()
    {
        if (GetComponent<EnemyHealth>().currentHealth > 400f)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        AccelerationLimit();
        if (chasePlayer && !stunned)
        {
            rb.AddForce(((Player.Instance.transform.position - transform.position).normalized), ForceMode2D.Impulse);
        }
    }
    
    

    private void ScatterShot(int numShots, bool reverse = false)
    {
        float addedAngle = 0.0f;
        if (reverse)
        {
            addedAngle = ((Mathf.PI * 2) / numShots) / 2;
        }
        for (int i = 0; i < numShots; i++)
        {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            float x = Mathf.Cos((2 * Mathf.PI * i) / numShots + addedAngle);
            float y = Mathf.Sin((2 * Mathf.PI * i) / numShots + addedAngle);
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector3(x, y, 0) * 3.0f;
        }
    }

    private IEnumerator SpawnBullets()
    {
        canAttack = false;
        swapScatter = !swapScatter;
        anim.Play("Slam");
        yield return new WaitForSeconds(41f / 60f);
        ScatterShot(10, swapScatter);
        yield return new WaitForSeconds(19f / 60f);
        canAttack = true;
        anim.Play("None");
    }

    private IEnumerator SpawnEnemies()
    {
        canAttack = false;
        anim.Play("BlockSummon");
        Vector3 randomPos;
        yield return new WaitForSeconds(1f);
        randomPos = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
        Instantiate(guard, transform.position + randomPos, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        randomPos = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
        Instantiate(guard, transform.position + randomPos, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        canAttack = true;
        anim.Play("None");
    }
}