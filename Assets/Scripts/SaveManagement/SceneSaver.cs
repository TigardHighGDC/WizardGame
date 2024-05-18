using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSaver : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetString("Scene", SceneManager.GetActiveScene().name);
    }
}
