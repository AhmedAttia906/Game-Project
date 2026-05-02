using UnityEngine;

public class RaceCheckpoint : MonoBehaviour
{
    public int checkpointIndex = 0;

    [Header("Audio")]
    public AudioClip checkpointSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RaceManager.Instance.PlayerPassedCheckpoint(checkpointIndex);

            if (checkpointSound != null)
                audioSource.PlayOneShot(checkpointSound);
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