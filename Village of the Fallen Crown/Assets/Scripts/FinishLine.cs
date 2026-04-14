using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private bool playerFinished = false;

    private void OnTriggerEnter(Collider other)
    {
        if (playerFinished) return;

        if (other.CompareTag("Player"))
        {
            playerFinished = true;

            if (RaceManager.Instance != null)
            {
                RaceManager.Instance.PlayerFinished();
            }
        }
    }
}