using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;

    [Header("Stats")]
    public int damage = 10;
    public float chaseRange = 8f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.2f;

    private float lastAttackTime;
    private bool isDead = false;

    void Start()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    void Update()
    {
        if (isDead) return;
        if (player == null) return;
        if (agent == null || !agent.enabled) return;
        if (!gameObject.activeInHierarchy) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            if (agent.hasPath)
                agent.ResetPath();

            AttackPlayer();
        }
        else if (distance <= chaseRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            if (agent.hasPath)
                agent.ResetPath();
        }
    }

    void AttackPlayer()
    {
        if (isDead) return;
        if (player == null) return;

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            if (playerHealth == null)
                playerHealth = player.GetComponentInParent<PlayerHealth>();

            if (playerHealth == null)
                playerHealth = player.GetComponentInChildren<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Enemy attacked player!");
            }

            lastAttackTime = Time.time;
        }
    }

    public void StopEnemy()
    {
        isDead = true;

        if (agent != null && agent.enabled)
        {
            if (agent.hasPath)
                agent.ResetPath();

            agent.isStopped = true;
            agent.enabled = false;
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        enabled = false;
    }
}