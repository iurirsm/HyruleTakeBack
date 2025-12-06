using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealthUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] Slider healthBar;
    [SerializeField] TMP_Text healthText;
    [SerializeField] GameObject healthBarContainer;

    [Header("Boss Reference")]
    [SerializeField] BossHealth bossHealth;

    void Start()
    {
        if (bossHealth == null)
        {
            BossMarker boss = FindFirstObjectByType<BossMarker>();
            if (boss != null)
                bossHealth = boss.GetComponent<BossHealth>();
        }

        if (bossHealth == null && healthBarContainer != null)
        {
            healthBarContainer.SetActive(false);
        }
    }

    void Update()
    {
        if (bossHealth == null) return;

        UpdateHealthBar();

        if (bossHealth.IsDead && healthBarContainer != null)
        {
            healthBarContainer.SetActive(false);
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = bossHealth.MaxHealth;
            healthBar.value = bossHealth.CurrentHealth;
        }

        if (healthText != null)
        {
            healthText.text = $"{bossHealth.CurrentHealth} / {bossHealth.MaxHealth}";
        }
    }
}
