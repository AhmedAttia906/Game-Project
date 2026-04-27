using UnityEngine;

// Simple raycast sword combat.
// - Left mouse button swings the sword.
// - A short ray is cast from the camera; whatever it hits within range
//   takes damage if it has a BossAI component (extend later if needed).
public class SwordCombat : MonoBehaviour
{
    [Header("Setup")]
    public Camera playerCamera;       // drag the player's main camera
    public Animator swordAnimator;    // optional: animator on the sword model

    [Header("Stats")]
    public int damage = 25;
    public float attackRange = 2.5f;
    public float attackCooldown = 0.6f;

    [Header("Layers")]
    public LayerMask hitLayers = ~0;  // default: everything

    private float lastAttackTime;

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;
        lastAttackTime = Time.time;

        if (swordAnimator != null)
            swordAnimator.SetTrigger("Swing");

        if (playerCamera == null) return;

        Ray ray = new Ray(playerCamera.transform.position, transform.parent.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, attackRange, hitLayers))
        {
            // Look on the hit object first, then walk up to its parents
            BossAI boss = hit.collider.GetComponentInParent<BossAI>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
        }
    }
}
