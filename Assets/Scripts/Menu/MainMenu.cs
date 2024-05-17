using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("AirIslandTileMap");
    }

    public void OnLoadSceneButton()
    {
        SceneManager.LoadScene("LoadSceneMenu");
    }

    public void OnCreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }
}
