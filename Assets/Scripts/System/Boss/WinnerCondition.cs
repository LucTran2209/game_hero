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
        panelWinner.SetActive(true);
        Time.timeScale = 0f;
        PlayerPrefs.SetInt(Key.Skill4, 1);
        if (true)
        {
            PlayerPrefs.SetInt("map", 1);
        }
        
    }
}
