using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogue : MonoBehaviour
{
    public GameObject Boss;
    public string[] Dialogue;
    private bool triggeredBefore = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !triggeredBefore)
        {
            DialogueBox.Instance.StartDialogue(Dialogue, Boss.GetComponent<SpriteRenderer>().sprite);
            triggeredBefore = true;
        }
    }
}
