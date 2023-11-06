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
		dialog.Add("Chao dung si loai nguoi, chac chan ban den tu 1 tuong lai dang day dau don voi nguy co bi huy diet nen moi quay lai qua khu the nay !!!");
		dialog.Add("Day la the gioi ma loai nguoi cua ban chung song voi rat nhieu loai va toc dac biet Goblin, Tinh Linh, Nguoi Lun, Nguoi Soi, Thien Than, Long Tc.");
		dialog.Add("Hay co gang thong linh cac chung toc de co the doan ket vuot qua tham hoa o tuong lai cua ban.");
		dialog.Add("Moi chung toc deu co nang luc va nhung bau vat doc dao nhung khong phai toc nao cung de dang quy phuc dau (co khi phai dung vu luc do)");
		dialog.Add("Toi tin voi y chi va nang luc cua ban thi moi thu deu co the !!!");
		dialog.Add("!!! LEN DUONG !!!");
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
