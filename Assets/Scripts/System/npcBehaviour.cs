using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcBehaviour : MonoBehaviour
{
    public GameObject panel;
    public Button btn_close;


    // Start is called before the first frame update
    void Start()
    {
        btn_close.onClick.AddListener(HandlerButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandlerButton()
    {
        
        panel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            panel.SetActive(true);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        panel.SetActive(false);
    }
}
