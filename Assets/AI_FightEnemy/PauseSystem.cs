using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            PauseManager();
        }
    }

    public void PauseManager()
    {
        pausePanel.SetActive(!pausePanel.activeInHierarchy);

        if(pausePanel.activeInHierarchy)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.visible = false;
        }
    }
}
