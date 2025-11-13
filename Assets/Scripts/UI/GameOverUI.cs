using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Button restartButton;
    [SerializeField] Button menuButton;

    void Awake()
    {
        if (restartButton) restartButton.onClick.AddListener(Restart);
        if (menuButton) menuButton.onClick.AddListener(Menu);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f; 
    }

    void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
