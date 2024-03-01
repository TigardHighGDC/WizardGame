using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickOnObject : MonoBehaviour
{
    // Maybe use TextMeshProUGUI instead of Text in public Text dialogueText;
    // Maybe add using TMPro;

    // Public
    public GameObject dialogPanel;
    public Text dialogueText;
    public string[] dialogue;
    public float wordSpeed;
    // Private
    private int index;

    public void StartDialogue()
    {
        if (dialogPanel.activeInHierarchy)
        {
            zeroText();
        }
        else
        {
            dialogPanel.SetActive(true);
            StartCoroutine(Typing());
        }
    }
    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialogPanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }
}