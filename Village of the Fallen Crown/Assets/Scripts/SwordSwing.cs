using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    public float swingSpeed = 10f;

    public SwordDamage swordDamage;

    [Header("Audio")]
    public AudioClip swingSound;
    private AudioSource audioSource;

    private Quaternion originalRotation;
    private Quaternion attackRotation;
    private bool swinging = false;
    private bool returning = false;

    void Start()
    {
        originalRotation = transform.localRotation;
        attackRotation = originalRotation * Quaternion.Euler(35f, -35f, -25f);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !swinging)
        {
            swinging = true;
            returning = false;

            if (swingSound != null)
                audioSource.PlayOneShot(swingSound);

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