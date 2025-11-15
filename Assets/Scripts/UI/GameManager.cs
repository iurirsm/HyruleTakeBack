using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [SerializeField] TMP_Text coinText;
    int coins;

    void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    public void BindCoinText(TMP_Text t)
    {
        coinText = t;
        UpdateUI();
    }

    public void AddCoins(int v)
    {
        coins += v;
        UpdateUI();
    }

    public void ResetCoins()
    {
        coins = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (coinText) coinText.text = $"x {coins}";
    }

    public int GetCoins()
    { return coins;
    }
}
