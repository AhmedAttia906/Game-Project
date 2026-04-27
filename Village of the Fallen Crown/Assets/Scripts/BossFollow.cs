using UnityEngine;

public class BossFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float stopDistance = 2.5f;

    public Animator animator;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            // Move toward player
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;

            transform.position += direction * speed * Time.deltaTime;

            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

            // Play run animation
            if (animator != null)
                animator.SetBool("isRunning", true);
        }
        else
        {
            // Stop running
            if (animator != null)
                animator.SetBool("isRunning", false);
        }
    }
}