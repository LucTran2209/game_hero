using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogFinal : MonoBehaviour
{
    public GameObject dialogPannel;
    public TextMeshProUGUI message;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Khi người chơi vào khoảng cách gần với vật thể, cho phép hiển thị đoạn hội thoại
            dialogPannel.SetActive(true);  
            if (PlayerPrefs.GetInt("QuantityItem")>1 && PlayerPrefs.GetInt("QuantityItem") < 150) {
                message.text = $"So luong vat pham : {PlayerPrefs.GetInt("QuantityItem")}/150";

            }
            else
            {
                message.text = $"Vay la ban da nhan du vat pham roi !";
                StartCoroutine(LoadTotalMapAfterDelay(3f));
            }
        }
    }


    IEnumerator LoadTotalMapAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(1); 
    }
    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogPannel.SetActive(false);
        }
    }

}
