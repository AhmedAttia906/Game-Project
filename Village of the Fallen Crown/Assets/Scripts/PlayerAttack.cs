using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Camera playerCamera;
    public float attackRange = 4f;
    public int damage = 25;
    public float attackCooldown = 0.7f;

    private float nextAttackTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Attack()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, attackRange))
        {
            Debug.Log("Attack hit: " + hit.collider.name);

            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();

            if (enemy == null)
                enemy = hit.collider.GetComponentInParent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Hit enemy!");
            }
        }
        else
        {
            Debug.Log("Attack missed");
        }
    }
}