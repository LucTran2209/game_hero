using Assets.Scripts.CharacterMain;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealthScript : MonoBehaviour
{
    // Audio
    [SerializeField] AudioSource audioGetItem;
    [SerializeField] float healthItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (PlayerPrefs.GetFloat(Key.PlayerCurrentHealth) < PlayerPrefs.GetFloat(Key.PlayerMaxHealth))
            {
                PlayerPrefs.SetFloat(Key.PlayerCurrentHealth, Mathf.Clamp(PlayerPrefs.GetFloat(Key.PlayerCurrentHealth) + healthItem, PlayerPrefs.GetFloat(Key.PlayerCurrentHealth), PlayerPrefs.GetFloat(Key.PlayerMaxHealth)));

            }
            gameObject.SetActive(false);
        }

    }

}
