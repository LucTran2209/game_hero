using Assets.Scripts.CharacterMain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerCondition : MonoBehaviour
{
    [SerializeField] GameObject panelWinner;
    [SerializeField] AudioSource audioWinner;

    public void Winner1(int mapwinner)
    {       
        audioWinner.Play();
        Time.timeScale = 0f;
        switch (mapwinner)
        {
            case 5: 
                if(PlayerPrefs.GetInt(Key.Boss) == 0)
                {
                    PlayerPrefs.SetInt(Key.Boss, 1);

                }
                break;
            case 2:
                if (PlayerPrefs.GetInt(Key.NguoiLun) == 0)
                {
                    PlayerPrefs.SetInt(Key.NguoiLun, 1);

                    PlayerPrefs.SetFloat(Key.Jump, PlayerPrefs.GetFloat(Key.Jump) + 2);
                    PlayerPrefs.SetFloat(Key.Speed, PlayerPrefs.GetFloat(Key.Speed) + 2);

                }
                break;
            case 3:
                if (PlayerPrefs.GetInt(Key.Goblin) == 0)
                {
                    PlayerPrefs.SetInt(Key.Goblin, 1);

                    PlayerPrefs.SetInt(Key.Skill4, 1);
                }
                break;
            case 4:
                if (PlayerPrefs.GetInt(Key.Elf) == 0)
                {
                    PlayerPrefs.SetInt(Key.Elf, 1);

                    PlayerPrefs.SetFloat(Key.AtkPoint, PlayerPrefs.GetFloat(Key.AtkPoint) * 2);
                    PlayerPrefs.SetFloat(Key.PlayerHealth, PlayerPrefs.GetFloat(Key.PlayerHealth) * 2);
                }
                break;
            default:
                break;
        }
        panelWinner.SetActive(true);
    }

/*    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Winner1(2);
        }
    }*/
}
