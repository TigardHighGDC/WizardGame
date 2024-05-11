using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpell : MonoBehaviour
{
    public GameObject Brother;
    int dialogueIndex = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.ToString());
        if (collision.gameObject.ToString() == "Lightning" && dialogueIndex == 0)
        {
            DialogueBox.Instance.StartDialogue("Next try casting earth", Brother.GetComponent<SpriteRenderer>().sprite);
            dialogueIndex = 1;
        }

        else if (collision.gameObject.ToString() == "Earth" && dialogueIndex == 1)
        {
            DialogueBox.Instance.StartDialogue(Dialogue, Brother.GetComponent<SpriteRenderer>().sprite);
            dialogueIndex = 2;
        }

        else if (collision.gameObject.ToString() == "Fire" && dialogueIndex == 2)
        {
            DialogueBox.Instance.StartDialogue(Dialogue, Brother.GetComponent<SpriteRenderer>().sprite);
            dialogueIndex = 3;
        }

        else if (collision.gameObject.ToString() == "Water" && dialogueIndex == 3)
        {
            DialogueBox.Instance.StartDialogue(Dialogue, Brother.GetComponent<SpriteRenderer>().sprite);
            dialogueIndex = 4;
        }

        else if (collision.gameObject.ToString() == "Shield" && dialogueIndex == 4)
        {
            DialogueBox.Instance.StartDialogue(Dialogue, Brother.GetComponent<SpriteRenderer>().sprite);
            dialogueIndex = 0;
        }
    }
}
