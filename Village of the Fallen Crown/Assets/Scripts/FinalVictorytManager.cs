using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalVictoryManager : MonoBehaviour
{
    public GameObject finalVictoryPanel;

    public void ShowFinalVictory()
    {
        Time.timeScale = 0f;

        finalVictoryPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}