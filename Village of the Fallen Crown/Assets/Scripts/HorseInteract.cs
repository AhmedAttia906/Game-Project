using UnityEngine;
using TMPro;

public class HorseInteract : MonoBehaviour
{
    public TextMeshProUGUI missionText;
    public bool canInteract = false;

    public Transform player;
    public Vector3 followOffset = new Vector3(0, 0, 2);

    private bool playerInRange = false;
    private bool pickedUp = false;

    void Update()
    {
        if (!canInteract) return;

        if (playerInRange && !pickedUp && Input.GetKeyDown(KeyCode.E))
        {
            PickUpHorse();
        }

        if (pickedUp && player != null)
        {
            transform.position = player.position + player.forward * followOffset.z;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !pickedUp && canInteract)
        {
            playerInRange = true;

            if (missionText != null)
            {
                missionText.gameObject.SetActive(true);
                missionText.text = "Press E to take the horse";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void PickUpHorse()
    {
        pickedUp = true;

        if (missionText != null)
        {
            missionText.text = "Return the horse to the villager!";
        }

        Debug.Log("Horse picked up");
    }

    public bool HasHorse()
    {
        return pickedUp;
    }
}