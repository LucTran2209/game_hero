using Assets.Scripts.CharacterMain;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunction : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;

    // Player click button new play
    public void NewPlay()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);

    }

    //  Player click button Quit
    public void Quit()
    {
        Application.Quit();
    }

    // Player click choose wolf map
    public void LoadWolfMap()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(2);
    }
    
    // Player click choose dragon map
    public void LoadDragonMap()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(5);
    }

    // Player click choose Elf map
    public void LoadElfMap()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(4);
    }

    // Player click choose angel map
    public void LoadAngelMap()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(4);
    }

    // Player click choose dwarf map
    public void LoadDwarfMap()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(2);
    }

    // Player click choose goblin map
    public void LoadGoblinMap()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(3);
    }

    // Active panel confirm Quit
    public void SetActivePanel()
    {
        Time.timeScale = 1.0f;
        optionPanel.SetActive(true);
    }

    // DeActive panel confirm Quit
    public void DeActivePanel()
    {
        Time.timeScale = 1.0f;
        optionPanel.SetActive(false);
    }
}
