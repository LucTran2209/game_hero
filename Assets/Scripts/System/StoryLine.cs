using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public GameObject panelLamMo;
    public TextMeshProUGUI txt;
    public Button skipButton;
    private int currentDialogIndex = 0;
    private string currentDialog = "";
    private float letterDelay = 0.05f;
    private float letterTimer = 0f;
    private bool isDisplayingText = false;

    private List<string> dialog = new List<string>();

    void Start()
    {
		dialog.Add("Chào dũng sĩ loài người, anh quay lại quá khứ thế này chắc hẳn ở thế giới hiện tại đang hỗn loạn và bị hủy hoại lắm !!!");
		dialog.Add("Anh đang ở thời kì mà loài người sống hòa bình cùng tất cả chủng tộc khác. CÓ cả người lùn, người sói, thiên thần, long tôc,...");
		dialog.Add("Nếu anh có thể thống lĩnh chủng tộc và vượt qua các thử thách thì có thể đi lấy hạt giống thần để cứu lấy thế giới của anh");
		dialog.Add("Muốn chiến đấu với tên quái vật cuối cùng anh cần phải được ban tất cả sức mạnh của các chủng tộc trên thế giới này.");
        dialog.Add("Có những chủng tộc chỉ quy phục dưới những ai mạnh hơn họ thôi nên là hãy cố gắng nhớ.");
        dialog.Add("Tôi tin với năng lực và ý chí của anh không gì là không thể !!!");
		dialog.Add("!!! LÊN ĐƯỜNG !!!");
		ShowNextDialog();
        skipButton.onClick.AddListener(SkipDialog);
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
                Close();
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
    void Close() {
        gameObject.SetActive(false);
        panelLamMo.SetActive(false);
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

    void SkipDialog()
    {
        // Bỏ qua tất cả đoạn hội thoại còn lại và ẩn panel
        
        Close();
    }
}
