using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class WinUI : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField] Button restartButton;
    [SerializeField] Button menuButton;
    [SerializeField] Button nextLevelButton;

    [Header("Star Rating UI")]
    [SerializeField] GameObject[] stars;
    [SerializeField] TMP_Text performanceText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text coinsText;
    [SerializeField] TMP_Text damageText;

    [Header("Scene Management")]
    [SerializeField] bool useNextSceneInBuildSettings = true;
    [SerializeField] string specificNextSceneName = "";

    void Awake()
    {
        if (restartButton) restartButton.onClick.AddListener(Restart);
        if (menuButton) menuButton.onClick.AddListener(MainMenu);
        if (nextLevelButton) nextLevelButton.onClick.AddListener(NextLevel);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        UpdateStarDisplay();
        UpdateNextLevelButton();
        Time.timeScale = 0f;
    }

    void UpdateStarDisplay()
    {
        StarRating starRating = StarRating.Instance;
        if (starRating == null) return;

        int earnedStars = starRating.CalculateStars();

        if (stars != null && stars.Length > 0)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                if (stars[i] != null)
                    stars[i].SetActive(i < earnedStars);
            }
        }

        if (timeText)
            timeText.text = $"Time: {starRating.ElapsedTime:F1}s";

        if (coinsText)
            coinsText.text = $"Coins: {starRating.CoinsCollected}/{starRating.TotalCoins}";

        if (damageText)
            damageText.text = $"No Damage: {(!starRating.TookDamage ? "Yes" : "No")}";

        if (performanceText)
            performanceText.text = starRating.GetPerformanceSummary();
    }

    void UpdateNextLevelButton()
    {
        if (nextLevelButton == null) return;

        bool hasNextLevel = HasNextLevel();
        nextLevelButton.gameObject.SetActive(hasNextLevel);

        if (restartButton)
            restartButton.gameObject.SetActive(!hasNextLevel);
    }

    bool HasNextLevel()
    {
        if (!useNextSceneInBuildSettings && !string.IsNullOrEmpty(specificNextSceneName))
            return true;

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        return nextSceneIndex < SceneManager.sceneCountInBuildSettings;
    }

    void NextLevel()
    {
        Time.timeScale = 1f;

        if (!useNextSceneInBuildSettings && !string.IsNullOrEmpty(specificNextSceneName))
        {
            SceneManager.LoadScene(specificNextSceneName);
            return;
        }

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
