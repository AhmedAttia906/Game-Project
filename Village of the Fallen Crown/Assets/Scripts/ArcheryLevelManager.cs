using UnityEngine;
using TMPro;

public class ArcheryLevelManager : MonoBehaviour
{
    public TextMeshProUGUI startMessageText;
    public TextMeshProUGUI targetCounterText;
    public GameObject rewardChest;
    public AudioClip chestSound;
    private AudioSource audioSource;

    private int targetsHit = 0;
    private int totalTargets = 3;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        targetsHit = 0;

        if (rewardChest != null)
            rewardChest.SetActive(false);

        UpdateCounter();

        if (startMessageText != null)
        {
            startMessageText.gameObject.SetActive(true);
            Invoke(nameof(HideStartMessage), 3f);
        }
    }

    void HideStartMessage()
    {
        startMessageText.gameObject.SetActive(false);
    }

    public void TargetHit()
    {
        targetsHit++;
        UpdateCounter();

        if (targetsHit >= totalTargets)
        {
            CompleteChallenge();
        }
    }

    void UpdateCounter()
    {
        targetCounterText.text = "Targets: " + targetsHit + "/" + totalTargets;
    }

    void CompleteChallenge()
    {
        if (rewardChest != null)
            rewardChest.SetActive(true);

        if (audioSource != null && chestSound != null)
            audioSource.PlayOneShot(chestSound);

        if (startMessageText != null)
        {
            startMessageText.gameObject.SetActive(true);
            startMessageText.text = "Challenge Complete! Claim your chest.";
        }
    }
}