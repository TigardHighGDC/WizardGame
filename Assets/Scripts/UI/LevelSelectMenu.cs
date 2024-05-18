using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{
    public void OnBackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnAirIslandButton()
    {
        SceneManager.LoadScene("AirIslandTileMap");
    }

    public void OnAirIslandBossButton()
    {
        SceneManager.LoadScene("AirFactory");
    }

    public void OnFireIslandButton()
    {
        SceneManager.LoadScene("FireIslandTileMap");
    }

    public void OnFireIslandBossButton()
    {
        SceneManager.LoadScene("FireColosseum");
    }

    public void OnWaterIslandBossButton()
    {
        SceneManager.LoadScene("SealFight");
    }

    public void OnMainIslandFightButton()
    {
        SceneManager.LoadScene("MainMenuFight");
    }

    public void OnMainIslandButton()
    {
        SceneManager.LoadScene("MainIsland");
    }

    public void OnFinalBossIslandButton()
    {
        SceneManager.LoadScene("Castle");
    }
}
