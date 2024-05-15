using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherTalking : MonoBehaviour
{
    public GameObject Brother;
    public string[] Dialogue;
    private bool triggeredBefore = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !triggeredBefore)
        {
            DialogueBox.Instance.StartDialogue(Dialogue, Brother.GetComponent<SpriteRenderer>().sprite);
            triggeredBefore = true;
        }
    }
}