using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public void LoadSceneByName(string level)
    {
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    public void OpenPortfolio()
    {
        Application.OpenURL("https://niclombportfolio.altervista.org/");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
