using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    public float swingSpeed = 10f;

    public SwordDamage swordDamage;

    private Quaternion originalRotation;
    private Quaternion attackRotation;
    private bool swinging = false;
    private bool returning = false;

    void Start()
    {
        originalRotation = transform.localRotation;
        attackRotation = originalRotation * Quaternion.Euler(35f, -35f, -25f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !swinging)
        {
            swinging = true;
            returning = false;

            if (swordDamage != null)
            {
                swordDamage.StartDamage();
            }
        }

        if (swinging)
        {
            Quaternion target = returning ? originalRotation : attackRotation;

            transform.localRotation = Quaternion.Slerp(
                transform.localRotation,
                target,
                Time.deltaTime * swingSpeed
            );

            if (Quaternion.Angle(transform.localRotation, target) < 2f)
            {
                if (!returning)
                    returning = true;
                else
                    swinging = false;
            }
        }
    }
}