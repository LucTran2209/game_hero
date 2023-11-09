using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogFinal : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] private int itemCountRequired = 8;

    [Header("Dialog")]
    public GameObject dialogPannel;
    public TextMeshProUGUI message;

    [Header("Conversation")]
    public string[] conversation;
    private bool isTalking = false;
    private int messageIndex = 0;

    private void Start()
    {
        dialogPannel.SetActive(false);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Khi người chơi vào khoảng cách gần với vật thể, cho phép hiển thị đoạn hội thoại
            dialogPannel.SetActive(true);

            // lay ra so luong item da nhat duoc
            int itemCount = PlayerPrefs.GetInt("QuantityItem");

            if (itemCount < itemCountRequired)
            {
                message.text = "Someone took away my beloved equipment. Please help me!!";
            }
            else
            {
                //message.text = "Cam on cau da giup ta. Cau co nhan thay suc manh cua minh da thay doi khong!";
                //messageIndex = 0;
                //message.text = conversation[messageIndex];
                //isTalking = true;
                //StartCoroutine(LoadTotalMapAfterDelay(3f));
                DialogControl.instance.StartDialog();
            }
        }
    }

    private void Update()
    {
        // neu da tim du item
        if (isTalking)
        {
            // neu bam enter
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (messageIndex < conversation.Length - 1)
                {
                    // chuyen sang cau tiep theo
                    messageIndex++;
                    message.text = conversation[messageIndex];
                }
                else
                {
                    // dung hoi thoai
                    isTalking = false;
                    message.text = "Cho mot chut de chuyen map!";
                    StartCoroutine(LoadTotalMapAfterDelay(3f));
                }
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
