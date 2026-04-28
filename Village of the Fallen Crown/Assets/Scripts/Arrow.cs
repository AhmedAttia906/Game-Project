using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 30f;
    public float lifeTime = 10f;

    private Vector3 moveDirection;

    void Start()
    {
        moveDirection = transform.forward;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}