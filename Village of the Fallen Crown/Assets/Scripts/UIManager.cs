using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Slider healthBar;
    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public TMP_Text countdownText;
    public TMP_Text checkpointText;
    public TMP_Text positionText;

    [Header("Audio")]
    public AudioClip mainTheme;
    private AudioSource audioSource;

    private bool isPaused = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.ignoreListenerPause = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void UpdateHealth(int current, int max)
    {
        if (healthBar != null)
        {
            healthBar.maxValue = max;
            healthBar.value = current;
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;

        if (isPaused)
        {
            if (mainTheme != null && audioSource != null)
            {
                audioSource.clip = mainTheme;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource != null)
                audioSource.Stop();
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (audioSource != null)
            audioSource.Stop();
    }

    public void ShowWin()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowLose()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void UpdateCountdown(string text)
    {
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(!string.IsNullOrEmpty(text));
            countdownText.text = text;
        }
    }

    public void ShowRaceHUD()
    {
        if (checkpointText != null) checkpointText.gameObject.SetActive(true);
        if (positionText != null) positionText.gameObject.SetActive(true);
    }

    public void HideRaceHUD()
    {
        if (checkpointText != null) checkpointText.gameObject.SetActive(false);
        if (positionText != null) positionText.gameObject.SetActive(false);
        if (countdownText != null) countdownText.gameObject.SetActive(false);
    }

    public void UpdateCheckpoint(int current, int total)
    {
        if (checkpointText != null)
            checkpointText.text = "Checkpoint: " + current + "/" + total;
    }

    public void UpdatePosition(int position)
    {
        string suffix = "th";

        if (position == 1) suffix = "st";
        else if (position == 2) suffix = "nd";
        else if (position == 3) suffix = "rd";

        if (positionText != null)
            positionText.text = "Position: " + position + suffix;
    }
}