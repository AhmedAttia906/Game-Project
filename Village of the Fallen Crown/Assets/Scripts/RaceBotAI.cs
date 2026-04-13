using UnityEngine;
using UnityEngine.AI;

public class RaceBotAI : MonoBehaviour
{
    public string botName = "Bot";
    public Transform[] waypoints;
    public int currentWaypointIndex = 0;
    public int currentCheckpointIndex = 0;

    private NavMeshAgent agent;
    private bool raceStarted = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
    }

    void Update()
    {
        if (!raceStarted || waypoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance < 1f)
        {
            if (currentWaypointIndex < waypoints.Length - 1)
            {
                currentWaypointIndex++;
                agent.SetDestination(waypoints[currentWaypointIndex].position);
            }
        }
    }

    public void StartRace()
    {
        raceStarted = true;
        agent.isStopped = false;
        currentWaypointIndex = 0;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    public void PassedCheckpoint(int checkpointIndex)
    {
        if (checkpointIndex == currentCheckpointIndex)
        {
            currentCheckpointIndex++;
        }
    }

    public float DistanceToNextWaypoint()
    {
        if (waypoints.Length == 0 || currentWaypointIndex >= waypoints.Length)
            return 99999f;

        return Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position);
    }
}