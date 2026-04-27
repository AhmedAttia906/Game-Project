using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public HorseInteract horse;

    [Header("After Death")]
    public GameObject horseObject;
    public TextMeshProUGUI missionText;

    void Start()
    {
        currentHealth = maxHealth;

        //if (horseObject != null)
        //    horseObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy defeated!");

        EnemyAI ai = GetComponent<EnemyAI>();
        if (ai != null)
        {
            ai.StopEnemy();
        }

        if (horse != null)
        {
            horse.canInteract = true;
        }

        if (horseObject != null)
        {
            //horseObject.SetActive(true);
        }

        if (missionText != null)
        {
            missionText.gameObject.SetActive(true);
            missionText.text = "Thief is defeated! Take the horse back to the Villager.";
        }

        gameObject.SetActive(false);
    }
}