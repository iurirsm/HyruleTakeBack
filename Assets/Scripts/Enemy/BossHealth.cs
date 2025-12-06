using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] int maxHealth = 5;
    [SerializeField] float invincibilityTime = 0.5f;

    [Header("Phase Settings")]
    [SerializeField] float speedIncreasePerHit = 0.3f;
    [SerializeField] Color damageFlashColor = Color.red;
    [SerializeField] float flashDuration = 0.15f;

    [Header("Audio")]
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;

    int currentHealth;
    bool isInvincible = false;
    SpriteRenderer spriteRenderer;
    Color originalColor;
    EnemyPatrol patrol;
    AudioSource audioSource;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => currentHealth <= 0;

    void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        patrol = GetComponent<EnemyPatrol>();
        audioSource = GetComponent<AudioSource>();
        
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    public bool TakeDamage(int damage = 1)
    {
        if (isInvincible || IsDead) return false;

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        if (hitSound && audioSource)
            audioSource.PlayOneShot(hitSound);

        if (currentHealth > 0)
        {
            StartCoroutine(DamageFlashRoutine());
            StartCoroutine(InvincibilityRoutine());
            IncreasePhase();
        }
        else
        {
            if (deathSound && audioSource)
                audioSource.PlayOneShot(deathSound);
        }

        return true;
    }

    void IncreasePhase()
    {
        if (patrol != null)
        {
            float newMultiplier = 1f + (speedIncreasePerHit * (maxHealth - currentHealth));
            patrol.SpeedMultiplier = newMultiplier;
        }
    }

    IEnumerator DamageFlashRoutine()
    {
        if (spriteRenderer == null) yield break;

        float elapsed = 0f;
        while (elapsed < flashDuration)
        {
            spriteRenderer.color = Color.Lerp(damageFlashColor, originalColor, elapsed / flashDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = originalColor;
    }

    IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }
}
