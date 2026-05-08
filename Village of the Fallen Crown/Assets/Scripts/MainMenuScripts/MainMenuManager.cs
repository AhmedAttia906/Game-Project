using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject storyPanel;

    [Header("Audio")]
    public AudioClip mainTheme;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.loop = true;
        audioSource.playOnAwake = false;

        if (mainTheme != null)
        {
            audioSource.clip = mainTheme;
            audioSource.Play();
        }
    }

    public void StartGame()
    {
        if (storyPanel != null)
        {
            mainMenuPanel.SetActive(false);
            storyPanel.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Level_01");
        }
    }

    public void BeginJourney()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LeaveGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }
}