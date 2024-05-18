using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingFight : MonoBehaviour
{
    public bool startFight = false;
    public GameObject FireIndicator;
    public GameObject Lightning;
    public GameObject Water;
    public GameObject Shield;
    public GameObject[] Enemies;
    public GameObject HealthBar;

    private KingHealth health;
    private bool fireCanAttack = true;
    private bool lightningCanAttack = true;
    private bool swapScatter = false;
    private bool waterCanAttack = true;
    private bool pauseSpells = false;
    private bool canSpawn = true;

    private void Start()
    {
        Shield.SetActive(false);
        health = GetComponent<KingHealth>();
    }

    void Update()
    {
        if (!startFight)
        {
            Shield.SetActive(false);
            return;
        }
        HealthBar.SetActive(true);

        if (canSpawn)
        {
            StartCoroutine(Spawner());
        }

        if (pauseSpells)
        {
            return;
        }

        if (fireCanAttack)
        {
            StartCoroutine(Attack());
        }

        if (lightningCanAttack)
        {
            StartCoroutine(SpawnBullets());
        }

        if (waterCanAttack)
        {
            StartCoroutine(WaterAttack());
        }
    }

    IEnumerator Attack()
    {
        fireCanAttack = false;
        yield return new WaitForSeconds(35f / 60f);
        for (int x = -15; x < 20; x += 10)
        {
            for (int y = 10; y < 35; y += 10)
            {
                Vector3 spawnPosition = new Vector3(x + Random.Range(-1.5f, 1.5f), y + Random.Range(-3f, 3f), 0);
                Instantiate(FireIndicator, spawnPosition, Quaternion.identity);
            }
        }
        yield return new WaitForSeconds(3.0f);

        fireCanAttack = true;
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
            GameObject newBullet = Instantiate(Lightning, transform.position, Quaternion.identity);
            float x = Mathf.Cos((2 * Mathf.PI * i) / numShots + addedAngle);
            float y = Mathf.Sin((2 * Mathf.PI * i) / numShots + addedAngle);
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector3(x, y, 0) * 3.0f;
        }
    }

    private IEnumerator SpawnBullets()
    {
        lightningCanAttack = false;
        swapScatter = !swapScatter;
        yield return new WaitForSeconds(41f / 60f);
        ScatterShot(8, swapScatter);
        yield return new WaitForSeconds(19f / 60f);
        lightningCanAttack = true;
    }

    private IEnumerator WaterAttack()
    {
        waterCanAttack = false;
        float range = Random.Range(-20f, 20f);
        GameObject spawn = Instantiate(Water, new Vector3(range, 33, 0), Quaternion.identity);
        spawn.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -2.0f, 0);
        yield return new WaitForSeconds(1.5f);
        waterCanAttack = true;
    }

    private IEnumerator Spawner()
    {
        canSpawn = false;
        yield return new WaitForSeconds(25f);
        pauseSpells = true;
        yield return new WaitForSeconds(3.0f);
        health.invicible = true;
        Shield.SetActive(true);
        foreach (GameObject i in Enemies)
        {
            Instantiate(i, transform.position, Quaternion.identity);
        }
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 1);
        pauseSpells = false;
        health.invicible = false;
        Shield.SetActive(false);
        canSpawn = true;
    }
}
