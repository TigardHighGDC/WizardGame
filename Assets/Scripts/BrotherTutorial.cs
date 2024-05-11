using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherTutorial : MonoBehaviour
{
    public GameObject Brother;
    int dialogueIndex = 0;

// need to fix the recognition for this
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.ToString() ==  "Lightning(Clone)" && dialogueIndex == 0)
        {
            DialogueBox.Instance.StartDialogue(new string[] {"Next try casting earth"}, Brother.GetComponent<SpriteRenderer>().sprite);
            dialogueIndex = 1;
        }

        else if (collision.gameObject.ToString() ==  "Lightning(Clone)" && dialogueIndex == 1)
        {
            //change to earth spell
            DialogueBox.Instance.StartDialogue(new string[] {"Next try casting earth"}, Brother.GetComponent<SpriteRenderer>().sprite);
            dialogueIndex = 2;
        }

        else if (collision.gameObject.ToString() ==  "Fireball(Clone)" && dialogueIndex == 2)
        {
            DialogueBox.Instance.StartDialogue(new string[] {"Next try casting earth"}, Brother.GetComponent<SpriteRenderer>().sprite);
            dialogueIndex = 3;
        }

        else if (collision.gameObject.ToString() ==  "Spell-Wave(Clone)" && dialogueIndex == 3)
        {
            DialogueBox.Instance.StartDialogue(new string[] {"Next try casting earth"}, Brother.GetComponent<SpriteRenderer>().sprite);
            dialogueIndex = 4;
        }

        else if (collision.gameObject.ToString() ==  "Shield(Clone)" && dialogueIndex == 4)
        {
            //change to shield spell
            DialogueBox.Instance.StartDialogue(new string[] {"Next try casting earth"}, Brother.GetComponent<SpriteRenderer>().sprite);
            dialogueIndex = 0;
        }
    }
}