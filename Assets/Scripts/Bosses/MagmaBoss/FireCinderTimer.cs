using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCinderTimer : MonoBehaviour
{
    public GameObject FireWall;

    private void Start()
    {
        StartCoroutine(CreateFireWall());
    }

    IEnumerator CreateFireWall()
    {
        yield return new WaitForSeconds(1.5f);
        FireWall.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
