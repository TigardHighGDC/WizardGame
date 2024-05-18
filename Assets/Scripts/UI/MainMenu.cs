using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("Scene", "TutorialScene"));
    }

    public void OnLoadSceneButton()
    {
        SceneManager.LoadScene("LoadSceneMenu");
    }

    public void OnSettingsButton()
    {
        SceneManager.LoadScene("Settings");
    }

    public void OnMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnCreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ResetButton()
    {
        PlayerPrefs.DeleteAll();
    }
}
