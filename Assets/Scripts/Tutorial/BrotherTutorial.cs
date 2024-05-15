using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherTutorial : MonoBehaviour
{
    public GameObject Brother;
    int dialogueIndex = 0;
    public BrotherHealth brotherHealth;
    
    [Header("Signs")]
    public GameObject airPrefab;
    public GameObject earthPrefab;
    public GameObject firePrefab;
    public GameObject waterPrefab;
    public GameObject shieldPrefab;

    private void Update()
    {
        if (brotherHealth.hitBy == EnemyHealth.ElementType.Air && dialogueIndex == 0)
        {
            earthPrefab.SetActive(true);
            airPrefab.SetActive(false);
            DialogueBox.Instance.StartDialogue(new string[] {"Next try casting earth."}, Brother.GetComponent<SpriteRenderer>().sprite);
            dialogueIndex = 1;
        }

        else if (brotherHealth.hitBy == EnemyHealth.ElementType.Earth && dialogueIndex == 1)
        {
            waterPrefab.SetActive(true);
            earthPrefab.SetActive(false);
            DialogueBox.Instance.StartDialogue(new string[] {"Next try casting water."}, Brother.GetComponent<SpriteRenderer>().sprite);
            dialogueIndex = 2;
        }

        else if (brotherHealth.hitBy == EnemyHealth.ElementType.Water && dialogueIndex == 2)
        {
            firePrefab.SetActive(true);
            waterPrefab.SetActive(false);
            DialogueBox.Instance.StartDialogue(new string[] {"Next try casting fire."}, Brother.GetComponent<SpriteRenderer>().sprite);
            dialogueIndex = 3;
        }

        else if (brotherHealth.hitBy == EnemyHealth.ElementType.Fire && dialogueIndex == 3)
        {
            shieldPrefab.SetActive(true);
            firePrefab.SetActive(false);
            DialogueBox.Instance.StartDialogue(new string[] {"Finally try casting the shield spell. Keep walking down the path and you should be able to the castle."}, Brother.GetComponent<SpriteRenderer>().sprite);
            dialogueIndex = 4;
        }
    }
}