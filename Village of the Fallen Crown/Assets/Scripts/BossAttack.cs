using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public Transform player;
    public int damage = 10;
    public float attackRange = 3f;
    public float attackCooldown = 1.5f;

    public Animator animator;

    private float nextAttackTime = 0f;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange && Time.time >= nextAttackTime)
        {
            // Play attack animation
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }

            // Damage player
            PlayerHealthLvl3 playerHealth = player.GetComponent<PlayerHealthLvl3>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Boss attacked player");
            }

            nextAttackTime = Time.time + attackCooldown;
        }
    }
}