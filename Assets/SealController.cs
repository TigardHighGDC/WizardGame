using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealController : MonoBehaviour
{
    public GameObject Seal;
    public GameObject Shadow;
    public GameObject SealMelee;
    public GameObject jellyFish;

    [HideInInspector]
    public bool canDive = true;
    [HideInInspector]
    public bool firstDive = true;
    
    [HideInInspector]
    public bool start = false;

    private bool jumped = false;
    private int jumpCount = 0;
    private bool startChasing = false;

    private void Update()
    {
        if (!start)
        {
            return;
        }

        if (!canDive && startChasing && !jumped)
        {
            SealMelee.SetActive(true);
            SealMelee.transform.position = Seal.transform.position;
            Seal.SetActive(false);
            startChasing = false;
        }

        if (jumpCount >= 5)
        {
            canDive = false;
            startChasing = true;
            jumpCount = 0;
        }
        if (!jumped && canDive)
        {
            if (firstDive)
            {
                Seal.SetActive(true);
                Seal.transform.position = SealMelee.transform.position;
                SealMelee.SetActive(false);
            }
            StartCoroutine(Dive());
            if (jumpCount % 2 == 0)
            {
                Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-10f, 10f),
                                                                     Random.Range(-10f, 10f), 0);
                Instantiate(jellyFish, spawnPosition, Quaternion.identity);
            }

            jumpCount++;
        }
    }

    private IEnumerator Dive()
    {
        Rigidbody2D rb = Seal.GetComponent<Rigidbody2D>();
        jumped = true;
        Seal.GetComponent<Animator>().Play("SealUp");

        yield return new WaitForSeconds(0.5f);
        rb.velocity = transform.up * 30.0f;

        yield return new WaitForSeconds(0.5f);
        Seal.GetComponent<Animator>().Play("Disappear");
        rb.velocity = new Vector2(0, 0);
        GameObject shadow = Instantiate(Shadow, Player.Instance.transform.position, Quaternion.identity);
        Seal.transform.position = shadow.transform.position;
        Seal.transform.position = new Vector3(Seal.transform.position.x, Seal.transform.position.y + 11.0f, 0.0f);

        yield return new WaitForSeconds(2.0f);
        Seal.GetComponent<Animator>().Play("SealDown");
        rb.velocity = -transform.up * 20.0f;

        yield return new WaitForSeconds(0.5f);
        rb.velocity = new Vector2(0, 0);
        
        yield return new WaitForSeconds(1.5f);
        Seal.GetComponent<Animator>().Play("None");

        jumped = false;
    }
}
