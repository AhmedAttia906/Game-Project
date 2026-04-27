using UnityEngine;

public class BossFightStart : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossHealthBar;

    private bool started = false;

    private void OnTriggerEnter(Collider other)
    {
        if (started) return;

        if (other.CompareTag("Player"))
        {
            started = true;

            boss.SetActive(true);

            if (bossHealthBar != null)
                bossHealthBar.SetActive(true);

            Debug.Log("Boss fight started!");
        }
    }
}