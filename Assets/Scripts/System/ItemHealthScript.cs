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
            PlayerPrefs.SetFloat(Key.PlayerCurrentHealth, Mathf.Clamp(PlayerPrefs.GetFloat(Key.PlayerHealth) + 200f, 0, PlayerPrefs.GetFloat(Key.PlayerHealth)));
            gameObject.SetActive(false);
        }

    }

}
