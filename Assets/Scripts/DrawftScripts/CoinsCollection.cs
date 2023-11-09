using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCollection : MonoBehaviour
{
    private int coins = 0;
    [SerializeField] private Text coinsText;

    // Start is called before the first frame update


    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            coins++;
            coinsText.text = "Coins: "+ coins.ToString();
            gameObject.SetActive(false);
        }
    }
}
