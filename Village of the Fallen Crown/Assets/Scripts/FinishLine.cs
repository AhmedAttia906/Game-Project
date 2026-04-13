using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!RaceManager.Instance.raceStarted) return;

        if (other.CompareTag("Player"))
        {
            if (RaceManager.Instance.GetPlayerCheckpointIndex() >= RaceManager.Instance.totalCheckpoints)
            {
                RaceManager.Instance.RacerFinished(true);
            }
        }
        else
        {
            RaceBotAI bot = other.GetComponent<RaceBotAI>();
            if (bot != null && bot.currentCheckpointIndex >= RaceManager.Instance.totalCheckpoints)
            {
                RaceManager.Instance.RacerFinished(false);
            }
        }
    }
}