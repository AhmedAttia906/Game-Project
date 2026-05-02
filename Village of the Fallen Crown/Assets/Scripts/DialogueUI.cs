using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    [Header("Panel")]
    public GameObject panel;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI dialogueText;

    [Header("Buttons")]
    public Button acceptButton;
    public Button declineButton;
    public TextMeshProUGUI acceptButtonText;
    public TextMeshProUGUI declineButtonText;

    private Action onAccept;
    private Action onDecline;

    void Awake()
    {
        Instance = this;
        if (panel != null) panel.SetActive(false);
    }

    public void Show(string npcName, string text, string acceptLabel, string declineLabel, Action onAcc, Action onDec)
    {
        npcNameText.text = npcName;
        dialogueText.text = text;
        if (acceptButtonText != null) acceptButtonText.text = acceptLabel;
        if (declineButtonText != null) declineButtonText.text = declineLabel;

        onAccept = onAcc;
        onDecline = onDec;

        panel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnAcceptClicked()
    {
        Hide();
        onAccept?.Invoke();
    }

    public void OnDeclineClicked()
    {
        Hide();
        onDecline?.Invoke();
    }

    void Hide()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
