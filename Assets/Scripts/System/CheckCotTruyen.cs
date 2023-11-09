using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCotTruyen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("TotalMap") != 0)
        {
            gameObject.SetActive(false);
           
        }
        PlayerPrefs.SetInt("TotalMap", 1);
    }
}
