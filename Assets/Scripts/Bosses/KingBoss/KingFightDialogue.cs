using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingFightDialogue : MonoBehaviour
{
    public GameObject Boss;
    public Sprite King;
    public GameObject Wall;
    public string[] Dialogue;
    private bool triggeredBefore = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !triggeredBefore)
        {
            DialogueBox.Instance.StartDialogue(Dialogue, King);
            triggeredBefore = true;
        }
    }

    private void Update()
    {
        if (triggeredBefore && !DialogueBox.Instance.IsTalking)
        {
            Wall.SetActive(true);
            Boss.SetActive(true);
            Boss.GetComponent<KingFight>().startFight = true;
            Destroy(gameObject);
        }
    }
}
