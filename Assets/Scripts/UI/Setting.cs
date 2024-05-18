using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public void OnRightButton()
    {
        PlayerPrefs.SetFloat("SketchPosition", 1);
    }

    public void OnLeftButton()
    {
        PlayerPrefs.SetFloat("SketchPosition", -1);
    }

    public void OnEasyButton()
    {
        PlayerPrefs.SetFloat("Health", 150f);
    }

    public void OnNormalButton()
    {
        PlayerPrefs.SetFloat("Health", 100f);
    }

    public void OnHardButton()
    {
        PlayerPrefs.SetFloat("Health", 50f);
    }

    public void OnBackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
