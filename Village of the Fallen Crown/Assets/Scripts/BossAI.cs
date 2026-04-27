using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Simple boss enemy.
// - Stays idle until Activate() is called (by BossTrigger).
// - Chases the player and melee-attacks when in range.
// - Has health; dies when health hits 0 and triggers level win.
[RequireComponent(typeof(NavMeshAgent))]
public class BossAI : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 200;
    public int currentHealth;
    public int damage = 20;
    public float chaseRange = 30f;
    public float attackRange = 2.5f;
    public float attackCooldown = 1.5f;

    [Header("References")]
    public Transform player;
    public NavMeshAgent agent;
    public Animator animator;       // optional
    public Slider bossHealthBar;    // optional UI slider for the boss

    [Header("Death")]
    public float winDelay = 2f;     // wait after death animation, then show win screen

    private float lastAttackTime;
    private bool isActive = false;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (agent == null) agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        // Boss is idle until BossTrigger activates it
        agent.isStopped = true;

        if (bossHealthBar != null)
        {
            bossHealthBar.maxValue = maxHealth;
            bossHealthBar.value = currentHealth;
            bossHealthBar.gameObject.SetActive(false); // hidden until fight starts
        }
    }

    // Called from BossTrigger when player enters the arena
    public void Activate()
    {
        if (isActive || isDead) return;
        isActive = true;
        agent.isStopped = false;

        if (bossHealthBar != null)
            bossHealthBar.gameObject.SetActive(true);
    }

    void Update()
    {
        if (!isActive || isDead || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseRange)
        {
            agent.SetDestination(player.position);
        }

        if (distance <= attackRange)
        {
            agent.ResetPath();
            FacePlayer();
            AttackPlayer();
        }
    }

    void FacePlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude > 0.01f)
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(dir), 5f * Time.deltaTime);
    }

    void AttackPlayer()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;
        lastAttackTime = Time.time;

        if (animator != null) animator.SetTrigger("Attack");

        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (ph != null) ph.TakeDamage(damage);
    }

    // Called by SwordCombat (or anything that should hurt the boss)
    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (bossHealthBar != null)
            bossHealthBar.value = currentHealth;

        if (animator != null) animator.SetTrigger("Hit");

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        isDead = true;
        agent.isStopped = true;

        if (animator != null) animator.SetTrigger("Die");
        if (bossHealthBar != null) bossHealthBar.gameObject.SetActive(false);

        // Disable collider so the player can walk through the corpse if needed
        Collider c = GetComponent<Collider>();
        if (c != null) c.enabled = false;

        Invoke(nameof(TriggerLevelComplete), winDelay);
    }

    void TriggerLevelComplete()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.LevelComplete();
    }
}
