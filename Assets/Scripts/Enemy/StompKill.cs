using UnityEngine;
using UnityEngine.SceneManagement;

public class StompKill : MonoBehaviour
{
    [SerializeField] float bounce = 12f;       // upward velocity after stomp
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
#if UNITY_600_0_OR_NEWER
            winUI = Object.FindFirstObjectByType<WinUI>(FindObjectsInactive.Include);
#else
            winUI = FindObjectOfType<WinUI>(true); 
#endif
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
        if (vy >= 0f) return; // only downward hits count

        dead = true;

        // stop hurting & disable all colliders
        var hurter = enemyRoot.GetComponent<HurtsPlayer>();
        if (hurter) hurter.DisableHurter();
        foreach (var c in enemyRoot.GetComponentsInChildren<Collider2D>()) c.enabled = false;

        // if this enemy is the boss, next scene.
        if (enemyRoot.GetComponent<BossMarker>())
        {
            int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

            // only load if there *is* a next scene in Build Settings
            if (nextIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextIndex);
            }
            else
            {
                // fallback: if no next scene, just show the win UI (optional)
                if (winUI != null)
                    winUI.Show();
            }
        }

        // delete enemy shortly after
        Destroy(enemyRoot, 0.05f);

        // bounce player upward
#if ENABLE_INPUT_SYSTEM || UNITY_600_0_OR_NEWER
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounce);
#else
        rb.velocity = new Vector2(rb.velocity.x, bounce);
#endif
    }
}
