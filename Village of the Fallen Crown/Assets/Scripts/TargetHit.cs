using UnityEngine;

public class TargetHit : MonoBehaviour
{
    public ArcheryLevelManager manager;
    public AudioClip hitSound;

    private bool isHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow") && !isHit)
        {
            isHit = true;

            AudioSource audioSource = manager.GetComponent<AudioSource>();

            if (audioSource != null && hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);
            }

            manager.TargetHit();

            gameObject.SetActive(false);
        }
    }
}