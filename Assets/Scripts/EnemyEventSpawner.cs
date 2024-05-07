using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventSpawner : MonoBehaviour
{
    public GameObject[] possibleEnemies = {};
    public GameObject Wall;
    public float interval = 2.0f;
    public float spawnWidth = 14.0f;
    public float spawnHeight = 14.0f;
    public int enemyCount = 5;

    private bool canRemove = false;

    private bool initiated = false;

    private void Update()
    {
        if (!initiated && Vector3.Distance(Player.Instance.transform.position, transform.position) < 12.0f)
        {
            StartCoroutine(SpawnEnemies());
            initiated = true;
            Wall.SetActive(true);
        }
        if (canRemove && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            PlayerHealth.Instance.Heal(1000f);
            Destroy(gameObject);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-spawnWidth, spawnWidth),
                                                                     Random.Range(-spawnHeight, spawnHeight), 0);
            while (Vector3.Distance(spawnPosition, Player.Instance.transform.position) < 4.0f)
            {
                spawnPosition = transform.position + new Vector3(Random.Range(-spawnWidth, spawnWidth),
                                                                 Random.Range(-spawnHeight, spawnHeight), 0);
            }
            Instantiate(possibleEnemies[Random.Range(0, possibleEnemies.Length)], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }
        canRemove = true;
    }
}
