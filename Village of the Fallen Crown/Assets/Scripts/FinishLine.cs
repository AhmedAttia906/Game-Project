using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip finishSound;
    public float finishSoundDuration = 4f;
    private AudioSource audioSource;

    private bool playerFinished = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerFinished) return;

        if (other.CompareTag("Player"))
        {
            playerFinished = true;

            if (finishSound != null)
                StartCoroutine(PlayFinishSound());

            if (RaceManager.Instance != null)
                RaceManager.Instance.PlayerFinished();
        }
    }

    IEnumerator PlayFinishSound()
    {
        audioSource.PlayOneShot(finishSound);
        yield return new WaitForSecondsRealtime(finishSoundDuration);
        audioSource.Stop();
    }
}