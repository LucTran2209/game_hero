using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;

    // Player click button new play
    public void NewPlay()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    // Player click button new play
    public void TotalMap()
    {
        SceneManager.LoadScene(1);
    }


    //  Player click button Quit
    public void Quit()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }
  
    // Active panel confirm Quit
    public void SetActivePanel()
    {
        optionPanel.SetActive(true);
        Time.timeScale = 0;
    }

    // DeActive panel confirm Quit
    public void DeActivePanel()
    {
        optionPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    //Out game
    public void ExitGame()
    {
        Application.Quit();   
    }

    // Load lại scene
    public void LoadMapAgain()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(5);
    }

    // Load lại scene nguoi lun
    public void LoadMapNguoiLun()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(3);
    }

    // Load lại scene boss
    public void LoadMapBoss()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(8);
    }
}
