using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseGuard : MonoBehaviour
{
    public GameObject civilians;
    private bool isTriggered = false;
    public GameObject[] possibleEnemies = {};
    public float interval = 0.5f;
    public float spawnWidth = 14.0f;
    public float spawnHeight = 14.0f;
    public int enemyCount = 7;
    public EnemyHealth enemyHealth;
    public EnterFactory enterFactory;

    private void Update()
    {
        if (!isTriggered && enemyHealth.burning > 0f)
        {
            enterFactory.canEnter = true;
            isTriggered = true;
            civilians.SetActive(false);
            StartCoroutine(SpawnEnemies());
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
    }
}
