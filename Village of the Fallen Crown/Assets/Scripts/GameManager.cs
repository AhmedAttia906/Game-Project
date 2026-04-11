using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool gameEnded = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayerDied()
    {
        if (gameEnded) return;
        gameEnded = true;

        UIManager.Instance.ShowLose();
    }

    public void LevelComplete()
    {
        if (gameEnded) return;
        gameEnded = true;

        UIManager.Instance.ShowWin();
    }
}