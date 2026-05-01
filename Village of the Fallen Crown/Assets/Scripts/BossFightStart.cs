using UnityEngine;

public class BossFightStart : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossHealthBar;

    [Header("Audio")]
    public AudioClip fightMusic;
    private AudioSource audioSource;

    private bool started = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (started) return;

        if (other.CompareTag("Player"))
        {
            started = true;

            boss.SetActive(true);

            if (bossHealthBar != null)
                bossHealthBar.SetActive(true);

            if (fightMusic != null && audioSource != null)
            {
                audioSource.clip = fightMusic;
                audioSource.volume = 0.4f;
                audioSource.Play();
            }

            Debug.Log("Boss fight started!");
        }
    }
}