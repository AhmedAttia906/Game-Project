using UnityEngine;

public class ShieldBash : MonoBehaviour
{
    public Transform normalPosition;   // ShieldHolder
    public Transform bashPosition;     // Empty object in front/middle
    public float bashSpeed = 12f;
    public float returnSpeed = 10f;
    public float bashTime = 0.25f;

    public float knockbackForce = 4f;
    public float knockbackDuration = 0.2f;

    [Header("Audio")]
    public AudioClip bashSound;
    private AudioSource audioSource;

    private bool isBashing = false;
    private bool canHit = false;
    private float timer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isBashing)
        {
            StartBash();
        }

        if (isBashing)
        {
            timer += Time.deltaTime;

            transform.position = Vector3.Lerp(
                transform.position,
                bashPosition.position,
                bashSpeed * Time.deltaTime
            );

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                bashPosition.rotation,
                bashSpeed * Time.deltaTime
            );

            if (timer >= bashTime)
            {
                isBashing = false;
                canHit = false;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(
                transform.position,
                normalPosition.position,
                returnSpeed * Time.deltaTime
            );

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                normalPosition.rotation,
                returnSpeed * Time.deltaTime
            );
        }
    }

    void StartBash()
    {
        isBashing = true;
        canHit = true;
        timer = 0f;

        if (bashSound != null)
            audioSource.PlayOneShot(bashSound);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!canHit) return;

        BossKnockback boss = other.GetComponentInParent<BossKnockback>();

        if (boss != null)
        {
            boss.KnockBack(transform.forward, knockbackForce, knockbackDuration);
            canHit = false;
        }
    }
}