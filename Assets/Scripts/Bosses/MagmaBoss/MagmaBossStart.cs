using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaBossStart : MonoBehaviour
{
    public GameObject EnemySpawner;
    public GameObject Boss;
    public GameObject BossHealthBar;

    private void Update()
    {
        if (EnemySpawner == null)
        {
            Boss.SetActive(true);
            BossHealthBar.SetActive(true);
            Destroy(gameObject);
        }
    }
}
