using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultyText : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Update()
    {
        text.text = "Current Health: " + PlayerPrefs.GetFloat("Health", 100.0f);
    }
}
