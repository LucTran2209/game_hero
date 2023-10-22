using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunction : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;

    // Player click button new play
    public void NewPlay()
    {
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
        SceneManager.LoadScene(2);
    }
    
    // Player click choose dragon map
    public void LoadDragonMap()
    {
        SceneManager.LoadScene(7);
    }

    // Player click choose Elf map
    public void LoadElfMap()
    {
        SceneManager.LoadScene(6);
    }

    // Player click choose angel map
    public void LoadAngelMap()
    {
        SceneManager.LoadScene(4);
    }

    // Player click choose dwarf map
    public void LoadDwarfMap()
    {
        SceneManager.LoadScene(3);
    }

    // Player click choose goblin map
    public void LoadGoblinMap()
    {
        SceneManager.LoadScene(5);
    }

    // Active panel confirm Quit
    public void SetActivePanel()
    {
        optionPanel.SetActive(true);
    }

    // DeActive panel confirm Quit
    public void DeActivePanel()
    {
        optionPanel.SetActive(false);
    }
}
