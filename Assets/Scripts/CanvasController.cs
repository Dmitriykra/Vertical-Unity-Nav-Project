using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject startPanel;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitToMenu()
    {
        startPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
