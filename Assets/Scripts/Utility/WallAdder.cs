using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAdder : MonoBehaviour
{
    public GameObject Wall;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Wall.SetActive(true);
            Destroy(gameObject);
        }
    }
}
