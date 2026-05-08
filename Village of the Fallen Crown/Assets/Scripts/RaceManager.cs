using UnityEngine;
using System.Collections;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;

    public PlayerController playerController;
    public RaceBotAI[] bots;
    public UIManager uiManager;
    public GameObject rewardChest;
    public AudioClip chestSound;
    public AudioClip raceMusic;
    private AudioSource audioSource;

    public bool raceStarted = false;
    public bool raceEnded = false;
    public bool waitForNPC = false;

    private int finishedCount = 0;
    public int playerCheckpointIndex = -1;
    public int playerWaypointIndex = -1;

    private Vector3 playerStartPos;
    private Quaternion playerStartRot;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Start()
    {
        if (playerController != null)
        {
            playerStartPos = playerController.transform.position;
            playerStartRot = playerController.transform.rotation;
        }

        if (!waitForNPC)
        {
            StartCoroutine(StartCountdown());
        }
        else
        {
            foreach (RaceBotAI bot in bots)
                if (bot != null) bot.StopBot();
            if (uiManager != null)
            {
                uiManager.UpdateCountdown("");
                uiManager.HideRaceHUD();
            }
        }
    }

    public void BeginRace()
    {
        StartCoroutine(StartCountdown());
    }

    void Update()
{
    if (!raceStarted || raceEnded) return;

    if (uiManager != null)
    {
        uiManager.UpdatePosition(GetPlayerPosition());
    }
}

    IEnumerator StartCountdown()
    {
        Time.timeScale = 1f;
        raceStarted = false;
        raceEnded = false;
        finishedCount = 0;

        if (playerController != null)
        {
            playerController.canMove = false;
            // Teleport player back to race starting line
            var cc = playerController.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;
            playerController.transform.SetPositionAndRotation(playerStartPos, playerStartRot);
            if (cc != null) cc.enabled = true;
        }

        foreach (RaceBotAI bot in bots)
        {
            if (bot != null)
                bot.StopBot();
        }

        if (uiManager != null) uiManager.UpdateCountdown("3");
        yield return new WaitForSeconds(1f);

        if (uiManager != null) uiManager.UpdateCountdown("2");
        yield return new WaitForSeconds(1f);

        if (uiManager != null) uiManager.UpdateCountdown("1");
        yield return new WaitForSeconds(1f);

        if (uiManager != null) uiManager.UpdateCountdown("GO!");
        yield return new WaitForSeconds(1f);

        if (uiManager != null) uiManager.UpdateCountdown("");

        StartRace();
    }

    public void StartRace()
    {
        Time.timeScale = 1f;
        raceStarted = true;
        raceEnded = false;
        finishedCount = 0;
        playerCheckpointIndex = -1;
        playerWaypointIndex = -1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerController != null)
            playerController.canMove = true;

        if (uiManager != null)
            uiManager.ShowRaceHUD();

        if (raceMusic != null && audioSource != null)
        {
            audioSource.clip = raceMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

        foreach (RaceBotAI bot in bots)
        {
            if (bot != null)
                bot.ResumeBot();
        }
    }

    public void PlayerPassedWaypoint(int waypointIndex)
    {
        if (raceEnded) return;

        if (waypointIndex > playerWaypointIndex)
        {
            playerWaypointIndex = waypointIndex;
        }
    }

    public void PlayerPassedCheckpoint(int checkpointIndex)
    {
        if (raceEnded) return;

        if (checkpointIndex == playerCheckpointIndex + 1)
        {
            playerCheckpointIndex = checkpointIndex;

            if (uiManager != null)
            {
                uiManager.UpdateCheckpoint(checkpointIndex + 1, 5);
            }
        }
    }

    public void PlayerFinished()
    {
        if (raceEnded) return;

        finishedCount++;

        if (finishedCount == 1) WinRace();
        else LoseRace();
    }

    public void BotFinished()
    {
        if (raceEnded) return;

        finishedCount++;

        if (finishedCount == 1) LoseRace();
    }

    void WinRace()
    {
        raceEnded = true;
        StopAllBots();

        if (audioSource != null) audioSource.Stop();

        if (rewardChest != null)
        {
            rewardChest.SetActive(true);
            if (audioSource != null && chestSound != null)
                audioSource.PlayOneShot(chestSound);
        }
        else
        {
            if (playerController != null)
                playerController.canMove = false;
            if (uiManager != null)
                uiManager.ShowWin();
        }
    }

    void LoseRace()
    {
        raceEnded = true;

        if (playerController != null)
            playerController.canMove = false;

        if (audioSource != null) audioSource.Stop();

        StopAllBots();

        if (uiManager != null)
            uiManager.ShowLose();
    }

    void StopAllBots()
    {
        foreach (RaceBotAI bot in bots)
        {
            if (bot != null)
                bot.StopBot();
        }
    }

    public int GetPlayerPosition()
{
    int position = 1;

    foreach (RaceBotAI bot in bots)
    {
        if (bot == null) continue;

        bool botAhead = false;

        if (bot.currentCheckpointIndex > playerCheckpointIndex)
        {
            botAhead = true;
        }
        else if (bot.currentCheckpointIndex == playerCheckpointIndex)
        {
            if (bot.currentWaypointIndex > playerWaypointIndex)
            {
                botAhead = true;
            }
        }

        if (botAhead)
        {
            position++;
        }
    }

    return position;
}
}