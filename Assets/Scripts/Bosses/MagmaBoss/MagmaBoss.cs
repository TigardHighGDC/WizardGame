using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaBoss : MonoBehaviour
{
    public GameObject FireIndicator;
    public GameObject EnemyFireWall;
    public string[] Dialogue;

    public GameObject Laser;
    public GameObject FireBall;

    private bool canAttack = true;
    private bool beginDialogue = true;
    private bool initiated = false;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Disappear");
        Laser.SetActive(false);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!initiated)
        {
            initiated = true;
            anim.Play("Jump");
            return;
        }

        if (DialogueBox.Instance.IsTalking)
        {
            return;
        }

        if (beginDialogue)
        {
            DialogueBox.Instance.StartDialogue(Dialogue, GetComponent<SpriteRenderer>().sprite);
            beginDialogue = false;
            return;
        }

        if (canAttack)
        {
            StartCoroutine(Attack());
            StartCoroutine(LaserAttack());
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        anim.Play("Clap");
        yield return new WaitForSeconds(35f / 60f);
        for (int x = -15; x < 20; x += 10)
        {
            for (int y = -10; y < 15; y += 10)
            {
                Vector3 spawnPosition = new Vector3(x + Random.Range(-1.5f, 1.5f), y + Random.Range(-3f, 3f), 0);
                Instantiate(FireIndicator, spawnPosition, Quaternion.identity);
            }
        }
        yield return new WaitForSeconds(3.0f);

        anim.Play("None");
        canAttack = true;
    }

    IEnumerator LaserAttack()
    {
        yield return new WaitForSeconds(1.0f + (35f / 60f));
        Laser.SetActive(true);
        LineRenderer line = Laser.GetComponent<LineRenderer>();
        float timer = 0.0f;
        while (timer < 2.0f)
        {
            line.SetPositions(new Vector3[] { Player.Instance.gameObject.transform.position, transform.position });
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Vector3 direction = (Player.Instance.transform.position - transform.position).normalized;
        GameObject bullet =
            Instantiate(FireBall, transform.position, Quaternion.FromToRotation(transform.up, direction));
        bullet.GetComponent<Rigidbody2D>().velocity = direction * 15.0f;
        Laser.SetActive(false);
    }
}
