using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField] Button restartButton;
    [SerializeField] Button menuButton;

    void Awake()
    {
        if (restartButton) restartButton.onClick.AddListener(Restart);
        if (menuButton) menuButton.onClick.AddListener(MainMenu);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f; // pause the game
    }

    void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
