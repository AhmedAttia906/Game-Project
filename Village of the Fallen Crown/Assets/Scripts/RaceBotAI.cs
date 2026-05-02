using UnityEngine;
using UnityEngine.AI;

public class RaceBotAI : MonoBehaviour
{
    public Transform[] waypoints;
    public int currentWaypointIndex = 0;
    public int currentCheckpointIndex = -1;
    public Animator animator;

    private NavMeshAgent agent;
    private bool hasFinished = false;

    public void ResumeBot()
    {
        if (agent == null)
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        foreach (var r in GetComponentsInChildren<Renderer>())
            r.enabled = true;

        agent.isStopped = false;

        if (waypoints.Length > 0)
            agent.SetDestination(waypoints[currentWaypointIndex].position);

        if (animator != null)
            animator.Play("metarig|Walk");
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;

        if (animator != null)
            animator.Play("metarig|Idle");

        foreach (var r in GetComponentsInChildren<Renderer>())
            r.enabled = false;
    }

    private void Update()
    {
        if (RaceManager.Instance != null && RaceManager.Instance.raceEnded)
            return;

        if (hasFinished) return;
        if (waypoints.Length == 0) return;
        if (agent.isStopped) return;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (currentWaypointIndex < waypoints.Length - 1)
            {
                currentWaypointIndex++;
                agent.SetDestination(waypoints[currentWaypointIndex].position);
            }
            else
            {
                FinishRace();
            }
        }
    }

    public void PassedCheckpoint(int checkpointIndex)
    {
        if (checkpointIndex > currentCheckpointIndex)
        {
            currentCheckpointIndex = checkpointIndex;
        }
    }

    void FinishRace()
    {
        if (hasFinished) return;

        hasFinished = true;
        agent.isStopped = true;

        if (animator != null)
            animator.Play("metarig|Idle");

        if (RaceManager.Instance != null)
            RaceManager.Instance.BotFinished();
    }

    public void StopBot()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        agent.isStopped = true;

        if (animator != null)
            animator.Play("metarig|Idle");
    }
}