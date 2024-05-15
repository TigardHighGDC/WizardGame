using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishedTalking : MonoBehaviour
{
    public string Location = "";
    public bool isActivated = false;
    private bool startedTalking = false;

    private void Update()
    {
        if (isActivated && DialogueBox.Instance.IsTalking)
        {
            startedTalking = true;
        }
        if (isActivated && startedTalking && !DialogueBox.Instance.IsTalking)
        {
            SceneManager.LoadScene(Location);
        }
    }
}
