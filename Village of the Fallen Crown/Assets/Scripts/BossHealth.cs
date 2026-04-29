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

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

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

        // wait for animation
        yield return new WaitForSeconds(deathDelay);

        FindObjectOfType<FinalVictoryManager>().ShowFinalVictory();

        // disappear
        gameObject.SetActive(false);
    }
}