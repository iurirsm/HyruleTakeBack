using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField] Button playAgainButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button menuButton;

    [Header("Coin Text")]
    [SerializeField] TMP_Text coinsText;
    void Awake()
    {
        if (playAgainButton) playAgainButton.onClick.AddListener(PlayAgain);
        if (exitButton) exitButton.onClick.AddListener(ExitButton);
        if (menuButton) menuButton.onClick.AddListener(MainMenu);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        coinsText.text = "Coins: " + GameManager.I.GetCoins();

        Time.timeScale = 0f; // pause the game
    }

    void PlayAgain()
    {
        Time.timeScale = 1f;
        GameManager.I.ResetCoins();
        SceneManager.LoadScene("GameScene");
    }

    void ExitButton()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
    void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
