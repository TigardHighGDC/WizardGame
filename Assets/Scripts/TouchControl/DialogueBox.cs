using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueBox : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textDisplay;
    public Image CharacterSprite;
    [HideInInspector]
    public string[] Dialogue;
    [HideInInspector]
    public bool IsTalking = false;
    public static DialogueBox Instance;

    private int currentParagraph = 0;

    private void Start()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void StartDialogue(string[] dialogue, Sprite sprite)
    {
        IsTalking = true;
        Dialogue = dialogue;
        CharacterSprite.sprite = sprite;
        gameObject.SetActive(true);
        NextParagraph();
        TouchInputPriority.Instance.Reset();
    }

    public void NextParagraph()
    {
        if (currentParagraph < Dialogue.Length)
        {
            textDisplay.text = Dialogue[currentParagraph];
            currentParagraph++;
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        currentParagraph = 0;
        IsTalking = false;
        gameObject.SetActive(false);
    }
}