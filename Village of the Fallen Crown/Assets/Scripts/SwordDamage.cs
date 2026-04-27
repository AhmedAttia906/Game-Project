using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public int damage = 20;
    private bool canDamage = false;

    public void StartDamage()
    {
        canDamage = true;
        CancelInvoke(nameof(StopDamage));
        Invoke(nameof(StopDamage), 1f);
    }

    void StopDamage()
    {
        canDamage = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Sword touched: " + other.name);

        if (!canDamage) return;

        BossHealth boss = other.GetComponentInParent<BossHealth>();

        if (boss != null)
        {
            Debug.Log("Boss damaged!");
            boss.TakeDamage(damage);
            canDamage = false;
        }
    }
}