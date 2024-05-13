using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaBossStart : MonoBehaviour
{
    public GameObject EnemySpawner;
    public GameObject Boss;

    private void Update()
    {
        if(EnemySpawner == null)
        {
            Boss.SetActive(true);
            Destroy(gameObject);
        }
    }
}
