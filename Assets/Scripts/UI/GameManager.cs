using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text levelText;

    int coins;
    int level = 1;

    void Awake()
    {
        // Singleton pattern
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
        DontDestroyOnLoad(gameObject);

        // Listen for scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Clean up event subscription (in case object is destroyed)
        if (I == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Auto-set level based on scene index
        SetLevel(scene.buildIndex);
    }

    // --- COINS ---

    public void BindCoinText(TMP_Text t)
    {
        coinText = t;
        UpdateCoinUI();
    }

    public void AddCoins(int v)
    {
        coins += v;
        UpdateCoinUI();
    }

    public void ResetCoins()
    {
        coins = 0;
        UpdateCoinUI();
    }

    void UpdateCoinUI()
    {
        if (coinText)
        {
            coinText.text = $"Gems x {coins}";
        }
    }

    // --- LEVEL ---

    public void BindLevelText(TMP_Text t)
    {
        levelText = t;
        UpdateLevelUI();
    }

    public void SetLevel(int v)
    {
        level = v;
        UpdateLevelUI();
    }

    void UpdateLevelUI()
    {
        if (levelText)
        {
            levelText.text = $"Level {level}";
        }
    }
}
