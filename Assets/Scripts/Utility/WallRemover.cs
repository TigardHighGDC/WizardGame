using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRemover : MonoBehaviour
{
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Destroy(gameObject);
        }
    }
}
