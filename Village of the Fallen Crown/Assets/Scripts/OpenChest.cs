using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    public Transform chestTop;
    public GameObject shield;

    public float openAngle = -70f;
    public float openSpeed = 3f;

    private bool playerNear = false;
    private bool isOpening = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = chestTop.localRotation;
        openRotation = closedRotation * Quaternion.Euler(openAngle, 0, 0);

        if (shield != null)
            shield.SetActive(false);
    }

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            isOpening = true;
        }

        if (isOpening)
        {
            chestTop.localRotation = Quaternion.Lerp(
                chestTop.localRotation,
                openRotation,
                Time.deltaTime * openSpeed
            );

            if (Quaternion.Angle(chestTop.localRotation, openRotation) < 2f)
            {
                chestTop.localRotation = openRotation;

                if (shield != null)
                    shield.SetActive(true);

                FindObjectOfType<LevelCompleteManager>().TriggerLevelComplete();

                isOpening = false; // VERY IMPORTANT (prevents repeating)
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}