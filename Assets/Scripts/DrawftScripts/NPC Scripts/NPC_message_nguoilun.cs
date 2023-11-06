using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_message_nguoilun : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Dialog_message;
    [SerializeField] private GameObject Message;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Dialog_message.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Dialog_message.SetActive(false);
        }
    }
}
