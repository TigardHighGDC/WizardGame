using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealDialogue : MonoBehaviour
{
    public GameObject Boss;
    public string[] Dialogue;
    public Sprite BossSprite;
    private bool triggeredBefore = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !triggeredBefore)
        {
            DialogueBox.Instance.StartDialogue(Dialogue, BossSprite);
            triggeredBefore = true;
        }
    }

    private void Update()
    {
        if (triggeredBefore && !DialogueBox.Instance.IsTalking)
        {
            Boss.GetComponent<SealController>().start = true;
            Destroy(gameObject);
        }
    }
}
