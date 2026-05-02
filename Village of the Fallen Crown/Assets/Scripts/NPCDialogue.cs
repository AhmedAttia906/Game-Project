using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [Header("Dialogue Content")]
    public string npcName = "Villager";
    [TextArea(3, 6)]
    public string dialogueText;
    public string acceptLabel = "Accept";
    public string declineLabel = "Decline";

    [Header("Panel References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI panelNpcName;
    public TextMeshProUGUI panelDialogueText;
    public Button acceptButton;
    public Button declineButton;

    [Header("Interact Prompt")]
    public TextMeshProUGUI interactPrompt;

    [Header("Events")]
    public UnityEvent onAccepted;
    public UnityEvent onDeclined;

    private bool playerNear = false;
    private bool hasInteracted = false;

    void Start()
    {
        if (interactPrompt != null)
            interactPrompt.gameObject.SetActive(false);
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (playerNear && !hasInteracted && Input.GetKeyDown(KeyCode.E))
            OpenDialogue();
    }

    void OpenDialogue()
    {
        if (interactPrompt != null)
            interactPrompt.gameObject.SetActive(false);

        if (panelNpcName != null) panelNpcName.text = npcName;
        if (panelDialogueText != null) panelDialogueText.text = dialogueText;
        if (acceptButton != null)
        {
            acceptButton.GetComponentInChildren<TextMeshProUGUI>().text = acceptLabel;
            acceptButton.onClick.RemoveAllListeners();
            acceptButton.onClick.AddListener(OnAccept);
        }
        if (declineButton != null)
        {
            declineButton.GetComponentInChildren<TextMeshProUGUI>().text = declineLabel;
            declineButton.onClick.RemoveAllListeners();
            declineButton.onClick.AddListener(OnDecline);
        }

        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnAccept()
    {
        CloseDialogue();
        hasInteracted = true;
        onAccepted.Invoke();
    }

    void OnDecline()
    {
        CloseDialogue();
        onDeclined.Invoke();
    }

    void CloseDialogue()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasInteracted)
        {
            playerNear = true;
            if (interactPrompt != null)
                interactPrompt.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            if (interactPrompt != null)
                interactPrompt.gameObject.SetActive(false);
        }
    }
}
