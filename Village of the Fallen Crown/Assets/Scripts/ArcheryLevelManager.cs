using UnityEngine;
using TMPro;

public class ArcheryLevelManager : MonoBehaviour
{
    public TextMeshProUGUI startMessageText;
    public TextMeshProUGUI targetCounterText;
    public GameObject targetBackground;
    public TextMeshProUGUI timerText;
    public GameObject rewardChest;
    public AudioClip chestSound;
    public AudioClip challengeMusic;
    private AudioSource audioSource;

    public float timerDuration = 30f;
    private float timeRemaining;
    private bool timerActive = false;
    private bool challengeStarted = false;

    private int targetsHit = 0;
    private int totalTargets = 3;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        targetsHit = 0;

        if (rewardChest != null)
            rewardChest.SetActive(false);

        // Hide HUD until challenge starts
        if (targetBackground != null)
            targetBackground.SetActive(false);
        if (targetCounterText != null)
            targetCounterText.gameObject.SetActive(false);
        if (timerText != null)
            timerText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!timerActive) return;

        timeRemaining -= Time.deltaTime;

        if (timerText != null)
        {
            int secs = Mathf.CeilToInt(timeRemaining);
            timerText.text = "Time: " + secs;
            timerText.color = secs <= 5 ? new UnityEngine.Color(1f, 0.25f, 0.25f) : new UnityEngine.Color(1f, 0.88f, 0.3f);
        }

        if (timeRemaining <= 0f)
        {
            timerActive = false;
            TimerExpired();
        }
    }

    public void StartChallenge()
    {
        if (challengeStarted) return;
        challengeStarted = true;
        timeRemaining = timerDuration;
        timerActive = true;

        if (targetBackground != null)
            targetBackground.SetActive(true);
        if (targetCounterText != null)
        {
            targetCounterText.gameObject.SetActive(true);
            UpdateCounter();
        }
        if (timerText != null)
            timerText.gameObject.SetActive(true);

        if (challengeMusic != null && audioSource != null)
        {
            audioSource.clip = challengeMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

        if (startMessageText != null)
        {
            startMessageText.gameObject.SetActive(true);
            startMessageText.text = "Destroy all targets to unlock your reward";
            Invoke(nameof(HideStartMessage), 3f);
        }
    }

    void HideStartMessage()
    {
        if (startMessageText != null)
            startMessageText.gameObject.SetActive(false);
    }

    public void TargetHit()
    {
        if (!challengeStarted) return;

        targetsHit++;
        UpdateCounter();

        if (targetsHit >= totalTargets)
            CompleteChallenge();
    }

    void UpdateCounter()
    {
        if (targetCounterText != null)
            targetCounterText.text = "Targets: " + targetsHit + "/" + totalTargets;
    }

    void CompleteChallenge()
    {
        timerActive = false;

        if (audioSource != null) audioSource.Stop();

        if (rewardChest != null)
            rewardChest.SetActive(true);

        if (audioSource != null && chestSound != null)
            audioSource.PlayOneShot(chestSound);

        if (startMessageText != null)
        {
            startMessageText.gameObject.SetActive(true);
            startMessageText.text = "Challenge Complete! Claim your chest.";
        }

        if (timerText != null)
            timerText.gameObject.SetActive(false);
    }

    void TimerExpired()
    {
        if (audioSource != null) audioSource.Stop();

        if (startMessageText != null)
        {
            startMessageText.gameObject.SetActive(true);
            startMessageText.text = "Time's up! You failed the challenge.";
        }

        if (timerText != null)
            timerText.gameObject.SetActive(false);

        var ui = FindObjectOfType<UIManager>();
        if (ui != null)
            ui.ShowLose();
    }
}
