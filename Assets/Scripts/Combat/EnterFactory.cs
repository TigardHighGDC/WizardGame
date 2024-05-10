using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterFactory : MonoBehaviour
{
    [HideInInspector]
    public bool canEnter = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && canEnter)
        {
            SceneManager.LoadScene("AirFactory");
        }
    }
}
