using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogControl : MonoBehaviour
{
    public static DialogControl instance;

    [Header("Player")]
    public GameObject playerDialog;
    public TextMeshProUGUI playerContent;

    [Header("NPC")]
    public GameObject npcDialog;
    public TextMeshProUGUI npcContent;

    public List<Speech> listSpeech = new List<Speech>();

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerDialog.SetActive(false);
        npcDialog.SetActive(false);
    }

    public bool IsFinishConversation()
    {
        return listSpeech.Count == 0;
    }

    public void StartDialog()
    {
        SomeoneTalk();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SomeoneTalk();
        }
    }

    private void SomeoneTalk()
    {
        if (listSpeech.Count > 0)
        {
            Speech speech = listSpeech[0];
            listSpeech.RemoveAt(0);

            if (speech.role == Role.Player)
                PlayerTalk(speech.message);
            else
                NPCTalk(speech.message);
        }
        else
        {
            playerDialog.SetActive(false);
            npcDialog.SetActive(false);
            StartCoroutine(ChangeNewMap());
        }
    }

    private IEnumerator ChangeNewMap()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }

    private void PlayerTalk(string content)
    {
        playerDialog.SetActive(true);
        npcDialog.SetActive(false);
        playerContent.text = content;
    }

    private void NPCTalk(string content)
    {
        playerDialog.SetActive(false);
        npcDialog.SetActive(true);
        npcContent.text = content;
    }
}

[Serializable]
public struct Speech
{
    public Role role; // 0: player, 1: npc
    public string message;
}

public enum Role
{
    Player,
    NPC
}