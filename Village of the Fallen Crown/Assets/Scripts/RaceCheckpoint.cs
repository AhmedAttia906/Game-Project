using UnityEngine;

public class RaceCheckpoint : MonoBehaviour
{
    public int checkpointIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RaceManager.Instance.PlayerPassedCheckpoint(checkpointIndex);
        }
        else
        {
            RaceBotAI bot = other.GetComponent<RaceBotAI>();
            if (bot != null)
            {
                bot.PassedCheckpoint(checkpointIndex);
            }
        }
    }
}