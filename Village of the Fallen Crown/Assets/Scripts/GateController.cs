using UnityEngine;

// A castle gate that slides up to open and back down to close.
// - Auto-opens when the player gets within `openDistance` (configurable).
// - Can also be opened/closed by other scripts via OpenGate() / CloseGate().
public class GateController : MonoBehaviour
{
    [Header("Open / Close Motion")]
    public Vector3 openOffset = new Vector3(0f, 5f, 0f); // how far the gate moves when open
    public float moveSpeed = 2f;                          // units per second (0..1 lerp speed)

    [Header("Auto-Open On Player Approach")]
    public bool autoOpenWhenPlayerNear = true;
    public Transform player;        // optional; auto-found by tag if null
    public float openDistance = 6f; // how close the player must be to trigger opening

    private Vector3 closedPos;
    private Vector3 openPos;
    private float t = 0f;           // 0 = closed, 1 = open
    private bool wantOpen = false;

    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + openOffset;

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        if (autoOpenWhenPlayerNear && player != null && !wantOpen)
        {
            if (Vector3.Distance(player.position, closedPos) <= openDistance)
                wantOpen = true;
        }

        float target = wantOpen ? 1f : 0f;
        t = Mathf.MoveTowards(t, target, moveSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(closedPos, openPos, t);
    }

    public void OpenGate()
    {
        wantOpen = true;
    }

    // Lock the gate down — used by BossTrigger to seal the player in the arena
    public void CloseGate()
    {
        wantOpen = false;
        autoOpenWhenPlayerNear = false;
    }
}
