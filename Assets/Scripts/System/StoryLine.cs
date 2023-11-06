using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public GameObject panelLamMo;
    public TextMeshProUGUI txt;
    private int currentDialogIndex = 0;
    private string currentDialog = "";
    private float letterDelay = 0.1f;
    private float letterTimer = 0f;
    private bool isDisplayingText = false;

    private List<string> dialog = new List<string>();

    void Start()
    {
        dialog.Add("Chào dũng sĩ loài người, chắc hẳn bạn đến từ 1 tương lai đang đầy đau đớn với nguy cơ bị hủy diệt nên mới quay lại quá khứ thế này !!!");
        dialog.Add("Đây là thế giới mà loài người của bạn chung sống với rất nhiều loài và tộc đặc biệt Goblin, Tinh Linh, Người Lùn, Người Sói, Thiên Thần, Long Tộc.");
        dialog.Add("Hãy cố gắng thống lĩnh các chủng tộc để có thể đoàn kết vượt qua thảm họa ở tương lai của bạn.");
        dialog.Add("Mỗi chủng tộc đều có năng lực và những báu vật độc đáo nhưng không phải tộc nào cũng dễ dàng quy phục đâu (có khi phải dùng vũ lực đó)");
        dialog.Add("Tôi tin với ý chí và năng lực của bạn thì mọi thứ đều có thể !!!");
        dialog.Add("!!! LÊN ĐƯỜNG !!!");


        ShowNextDialog();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isDisplayingText)
            {
                // If the text is still displaying, finish displaying it instantly
                letterTimer = 0f;
            }
            else if (currentDialogIndex < dialog.Count - 1)
            {
                // Show the next dialog when Space is pressed
                currentDialogIndex++;
                ShowNextDialog();
            }
            else
            {
                // Hide the panel when all dialog is displayed
                gameObject.SetActive(false);
                panelLamMo.SetActive(false);
            }
        }
    }

    void ShowNextDialog()
    {
        currentDialog = dialog[currentDialogIndex];
        txt.text = "";
        letterTimer = 0f;
        isDisplayingText = true;
    }

    void FixedUpdate()
    {
        if (isDisplayingText)
        {
            letterTimer += Time.fixedDeltaTime;
            if (letterTimer >= letterDelay)
            {
                letterTimer = 0f;
                txt.text += currentDialog[txt.text.Length];
                if (txt.text.Length >= currentDialog.Length)
                {
                    isDisplayingText = false;
                }
            }
        }
    }
}
