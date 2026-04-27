using UnityEngine;
using System.Collections;

public class BossKnockback : MonoBehaviour
{
    private bool isKnockedBack = false;

    public void KnockBack(Vector3 direction, float force, float duration)
    {
        if (!isKnockedBack)
        {
            StartCoroutine(KnockbackRoutine(direction, force, duration));
        }
    }

    IEnumerator KnockbackRoutine(Vector3 direction, float force, float duration)
    {
        isKnockedBack = true;

        float timer = 0f;
        direction.y = 0;
        direction.Normalize();

        while (timer < duration)
        {
            transform.position += direction * force * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        isKnockedBack = false;
    }
}