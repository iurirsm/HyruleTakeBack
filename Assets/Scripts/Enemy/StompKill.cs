using UnityEngine;
using UnityEngine.SceneManagement;

public class StompKill : MonoBehaviour
{
    [SerializeField] float bounce = 12f;
    [SerializeField] GameObject enemyRoot;
    [SerializeField] WinUI winUI;

    bool dead = false;

    void Reset()
    {
        if (enemyRoot == null) enemyRoot = transform.parent?.gameObject;
    }

    void Start()
    {
        if (winUI == null)
        {
            winUI = FindFirstObjectByType<WinUI>(FindObjectsInactive.Include);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (dead) return;
        if (!other.CompareTag("Player")) return;

        var rb = other.attachedRigidbody;
        if (rb == null) return;

#if ENABLE_INPUT_SYSTEM || UNITY_600_0_OR_NEWER
        float vy = rb.linearVelocity.y;
#else
        float vy = rb.velocity.y;
#endif
        if (vy >= 0f) return;

        bool isBoss = enemyRoot.GetComponent<BossMarker>() != null;
        BossHealth bossHealth = isBoss ? enemyRoot.GetComponent<BossHealth>() : null;

        if (bossHealth != null)
        {
            bool damaged = bossHealth.TakeDamage(1);
            if (!damaged)
            {
                BouncePlayer(rb);
                return;
            }

            if (!bossHealth.IsDead)
            {
                BouncePlayer(rb);
                return;
            }
        }

        dead = true;

        var hurter = enemyRoot.GetComponent<HurtsPlayer>();
        if (hurter) hurter.DisableHurter();
        foreach (var c in enemyRoot.GetComponentsInChildren<Collider2D>()) c.enabled = false;

        if (isBoss)
        {
            VictorySequence victorySeq = FindFirstObjectByType<VictorySequence>();
            if (victorySeq != null)
            {
                victorySeq.PlayVictorySequence(enemyRoot.transform.position);
            }
            else
            {
                int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

                if (nextIndex < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(nextIndex);
                }
                else
                {
                    if (winUI != null)
                        winUI.Show();
                }
            }
        }

        Destroy(enemyRoot, 0.05f);
        BouncePlayer(rb);
    }

    void BouncePlayer(Rigidbody2D rb)
    {
#if ENABLE_INPUT_SYSTEM || UNITY_600_0_OR_NEWER
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounce);
#else
        rb.velocity = new Vector2(rb.velocity.x, bounce);
#endif
    }
}
