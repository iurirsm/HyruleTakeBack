using UnityEngine;

public class StarRating : MonoBehaviour
{
    public static StarRating Instance;

    [Header("Star Criteria")]
    [SerializeField] float threeStarTimeLimit = 60f;
    [SerializeField] float twoStarTimeLimit = 90f;

    float levelStartTime;
    int totalCoins;
    int coinsCollected;
    bool tookDamage = false;

    public int CoinsCollected => coinsCollected;
    public int TotalCoins => totalCoins;
    public float ElapsedTime => Time.time - levelStartTime;
    public bool TookDamage => tookDamage;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        levelStartTime = Time.time;
        CountTotalCoins();
    }

    void CountTotalCoins()
    {
        Collectible[] coins = FindObjectsByType<Collectible>(FindObjectsSortMode.None);
        totalCoins = coins.Length;
    }

    public void OnCoinCollected()
    {
        coinsCollected++;
    }

    public void OnPlayerDamaged()
    {
        tookDamage = true;
    }

    public int CalculateStars()
    {
        int stars = 0;

        bool allCoinsCollected = (coinsCollected >= totalCoins);
        bool noDamageTaken = !tookDamage;
        bool fastCompletion = ElapsedTime <= threeStarTimeLimit;
        bool mediumCompletion = ElapsedTime <= twoStarTimeLimit;

        if (allCoinsCollected && noDamageTaken && fastCompletion)
        {
            stars = 3;
        }
        else if ((allCoinsCollected && noDamageTaken) || (allCoinsCollected && mediumCompletion) || (noDamageTaken && mediumCompletion))
        {
            stars = 2;
        }
        else if (allCoinsCollected || noDamageTaken || mediumCompletion)
        {
            stars = 1;
        }

        return stars;
    }

    public string GetPerformanceSummary()
    {
        return $"Time: {ElapsedTime:F1}s | Coins: {coinsCollected}/{totalCoins} | Damage: {(tookDamage ? "Yes" : "No")}";
    }
}
