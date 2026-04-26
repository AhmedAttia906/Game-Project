using UnityEngine;
using TMPro;

public class VillagerTrigger : MonoBehaviour
{
    public TextMeshProUGUI missionText;
    public HorseInteract horse;

    private bool playerInRange = false;
    private bool completed = false;

    void Update()
    {
        if (playerInRange && !completed)
        {
            if (horse != null && horse.HasHorse())
            {
                CompleteMission();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !completed)
        {
            playerInRange = true;

            if (missionText != null)
            {
                missionText.gameObject.SetActive(true);
                missionText.text = "My horse was stolen! Defeat the thief and bring it back.";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !completed)
        {
            playerInRange = false;

            if (missionText != null)
            {
                missionText.gameObject.SetActive(false);
            }
        }
    }

    void CompleteMission()
    {
        completed = true;
        playerInRange = false;

        if (missionText != null)
        {
            missionText.gameObject.SetActive(true);
            missionText.text = "Thank you! You got armor!";
        }

        PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.Heal(30);
        }

        Debug.Log("Mission Completed with Villager!");
    }
}