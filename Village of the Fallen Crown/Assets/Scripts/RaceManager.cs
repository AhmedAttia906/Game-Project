using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;
    private int playerWaypointIndex = 0;

    [Header("Player")]
    public PlayerController playerController;

    [Header("Bots")]
    public List<RaceBotAI> bots = new List<RaceBotAI>();

    [Header("Checkpoints")]
    public int totalCheckpoints = 5;

    [Header("Race State")]
    public bool raceStarted = false;
    public bool raceFinished = false;

    private int playerCheckpointIndex = 0;
    private int finishedRacersCount = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        playerController.canMove = false;
        UIManager.Instance.UpdateCheckpoint(0, totalCheckpoints);
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        UIManager.Instance.UpdateCountdown("3");
        yield return new WaitForSeconds(1f);

        UIManager.Instance.UpdateCountdown("2");
        yield return new WaitForSeconds(1f);

        UIManager.Instance.UpdateCountdown("1");
        yield return new WaitForSeconds(1f);

        UIManager.Instance.UpdateCountdown("GO!");
        yield return new WaitForSeconds(1f);

        UIManager.Instance.UpdateCountdown("");

        raceStarted = true;
        playerController.canMove = true;

        foreach (RaceBotAI bot in bots)
        {
            if (bot != null)
                bot.StartRace();
        }
    }

    public int GetPlayerCheckpointIndex()
    {
        return playerCheckpointIndex;
    }

    public void PlayerPassedWaypoint(int waypointIndex)
    {
        if (waypointIndex > playerWaypointIndex)
        {
            playerWaypointIndex = waypointIndex;
        }
    }

    public void PlayerPassedCheckpoint(int checkpointIndex)
    {
        if (checkpointIndex == playerCheckpointIndex)
        {
            playerCheckpointIndex++;
            UIManager.Instance.UpdateCheckpoint(playerCheckpointIndex, totalCheckpoints);
        }
    }

    public void RacerFinished(bool isPlayer)
    {
        finishedRacersCount++;

        if (isPlayer)
        {
            raceFinished = true;

            if (finishedRacersCount == 1)
                GameManager.Instance.LevelComplete();
            else
                GameManager.Instance.PlayerDied();
        }
    }

    void Update()
    {
        if (!raceStarted || raceFinished) return;

        UpdatePlayerPositionUI();
    }

    void UpdatePlayerPositionUI()
    {
        int playerPosition = 1;

        float playerProgress = GetPlayerProgress();

        foreach (RaceBotAI bot in bots)
        {
            if (bot == null) continue;

            float botProgress = GetBotProgress(bot);

            if (botProgress > playerProgress)
            {
                playerPosition++;
            }
        }

        UIManager.Instance.UpdatePosition(playerPosition);
    }

    float GetPlayerProgress()
    {
        float progress = 0f;

        progress += GetPlayerCheckpointIndex() * 1000f;
        progress += playerWaypointIndex * 10f;

        return progress;
    }

    float GetBotProgress(RaceBotAI bot)
    {
        float progress = 0f;

        progress += bot.currentCheckpointIndex * 1000f;
        progress += bot.currentWaypointIndex * 10f;
        progress -= bot.DistanceToNextWaypoint();

        return progress;
    }
}