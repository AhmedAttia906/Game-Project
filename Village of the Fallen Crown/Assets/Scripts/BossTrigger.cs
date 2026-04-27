using UnityEngine;

// Trigger volume placed at the entrance of the boss arena.
// When the player walks in:
//   1. Activates the boss.
//   2. Closes the gate behind the player (locking them into the fight).
//   3. Optionally shows a quick "intro" UI element for a few seconds.
[RequireComponent(typeof(Collider))]
public class BossTrigger : MonoBehaviour
{
    [Header("References")]
    public BossAI boss;
    public GateController gateToClose;   // optional
    public GameObject introUI;           // optional: a banner / text that says "The Fallen King"

    [Header("Intro")]
    public float introDuration = 2f;

    private bool triggered = false;

    void Start()
    {
        // Make sure the collider is set as a trigger
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;

        if (introUI != null) introUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;

        // 1) lock the gate
        if (gateToClose != null) gateToClose.CloseGate();

        // 2) play intro
        if (introUI != null)
        {
            introUI.SetActive(true);
            Invoke(nameof(HideIntroAndStartFight), introDuration);
        }
        else
        {
            StartFight();
        }
    }

    void HideIntroAndStartFight()
    {
        if (introUI != null) introUI.SetActive(false);
        StartFight();
    }

    void StartFight()
    {
        if (boss != null) boss.Activate();
    }
}
