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
    }
}
