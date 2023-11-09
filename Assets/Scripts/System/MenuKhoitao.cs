using Assets.Scripts.CharacterMain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuKhoitao : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("TotalMap", 0);
        
        PlayerPrefs.SetInt(Key.Skill4,0);
        PlayerPrefs.SetFloat(Key.PlayerHealth, 5000f);
        PlayerPrefs.SetFloat(Key.AtkPoint, 200f);
        PlayerPrefs.SetFloat(Key.Speed, 5.4f);
        PlayerPrefs.SetFloat(Key.Jump, 8f);
        PlayerPrefs.SetInt(Key.NguoiLun,0);
        PlayerPrefs.SetInt(Key.Goblin,0);
        PlayerPrefs.SetInt(Key.Elf,0);
        PlayerPrefs.SetInt(Key.Boss,0);
    }
}
