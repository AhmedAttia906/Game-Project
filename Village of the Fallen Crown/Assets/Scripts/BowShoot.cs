using UnityEngine;

public class BowShoot : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform shootPoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // left click
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
    }
}