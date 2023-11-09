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

    [SerializeField]
    private List<string> dialog = new List<string>();

    void Start()
    {
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
        if(panelLamMo != null)
        {
            panelLamMo.SetActive(false);
        }
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
