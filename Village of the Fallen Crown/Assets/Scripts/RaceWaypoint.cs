using UnityEngine;

public class RaceWaypoint : MonoBehaviour
{
    public int waypointIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RaceManager.Instance.PlayerPassedWaypoint(waypointIndex);
        }
    }
}