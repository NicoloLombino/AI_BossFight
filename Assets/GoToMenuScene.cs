using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenuScene : MonoBehaviour
{
    void Update()
    {
        if(Time.timeSinceLevelLoad >= 8)
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
