using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthBar;
    public Animator animator;

    public float deathDelay = 3f;

    [Header("Audio")]
    public AudioClip[] damageSounds;
    public AudioClip deathSound;
    public AudioClip victorySound;
    private AudioSource audioSource;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Boss HP: " + currentHealth);

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (damageSounds != null && damageSounds.Length > 0 && audioSource != null)
            audioSource.PlayOneShot(damageSounds[Random.Range(0, damageSounds.Length)]);

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        isDead = true;

        Debug.Log("Boss Dead");

        // stop movement & attack
        BossFollow follow = GetComponent<BossFollow>();
        if (follow != null) follow.enabled = false;

        BossAttack attack = GetComponent<BossAttack>();
        if (attack != null) attack.enabled = false;

        BossKnockback knockback = GetComponent<BossKnockback>();
        if (knockback != null) knockback.enabled = false;

        // play death animation
        if (animator != null)
        {
            animator.SetBool("isRunning", false);
            animator.SetTrigger("Die");
        }

        yield return new WaitForSeconds(1f);

        if (deathSound != null && audioSource != null)
            audioSource.PlayOneShot(deathSound);

        // stop fight OST
        var bossFightStart = FindObjectOfType<BossFightStart>();
        if (bossFightStart != null)
        {
            var src = bossFightStart.GetComponent<AudioSource>();
            if (src != null) src.Stop();
        }

        // wait for animation
        yield return new WaitForSeconds(deathDelay);

        if (victorySound != null && audioSource != null)
            audioSource.PlayOneShot(victorySound);

        FindObjectOfType<FinalVictoryManager>().ShowFinalVictory();

        // wait for victory sound to finish before deactivating (WaitForSecondsRealtime
        // works even if FinalVictoryManager sets timeScale to 0)
        yield return new WaitForSecondsRealtime(victorySound != null ? victorySound.length : 0f);

        gameObject.SetActive(false);
    }
}