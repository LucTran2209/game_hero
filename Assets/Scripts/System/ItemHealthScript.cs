using Assets.Scripts.CharacterMain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealthScript : MonoBehaviour
{
    // Audio
    [SerializeField] AudioSource audioGetItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (PlayerPrefs.GetFloat(Key.PlayerCurrentHealth) < PlayerPrefs.GetFloat(Key.PlayerMaxHealth))
            {
                PlayerPrefs.SetFloat(Key.PlayerCurrentHealth, PlayerPrefs.GetFloat(Key.PlayerCurrentHealth) + 50f);

            } else
            {
                PlayerPrefs.SetFloat(Key.PlayerCurrentHealth, PlayerPrefs.GetFloat(Key.PlayerCurrentHealth) + 0f);
            }
            
            gameObject.SetActive(false);
        }

    }

}
