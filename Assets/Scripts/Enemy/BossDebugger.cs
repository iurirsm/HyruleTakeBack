using UnityEngine;

[RequireComponent(typeof(BossHealth))]
public class BossDebugger : MonoBehaviour
{
    [Header("Debug Options")]
    [SerializeField] bool showHealthInHierarchy = true;
    [SerializeField] bool logPhaseChanges = true;
    [SerializeField] bool showGizmos = true;
    
    [Header("Gizmo Settings")]
    [SerializeField] Color healthBarColor = Color.green;
    [SerializeField] Color damagedColor = Color.red;
    [SerializeField] float barWidth = 2f;
    [SerializeField] float barHeight = 0.3f;
    [SerializeField] float barOffset = 1.5f;

    BossHealth bossHealth;
    int lastHealth;

    void Awake()
    {
        bossHealth = GetComponent<BossHealth>();
        lastHealth = bossHealth.MaxHealth;
    }

    void Update()
    {
        if (showHealthInHierarchy)
        {
            gameObject.name = $"Boss [{bossHealth.CurrentHealth}/{bossHealth.MaxHealth}]";
        }

        if (logPhaseChanges && bossHealth.CurrentHealth != lastHealth)
        {
            int phase = bossHealth.MaxHealth - bossHealth.CurrentHealth + 1;
            Debug.Log($"Boss Phase {phase}: Health {bossHealth.CurrentHealth}/{bossHealth.MaxHealth}");
            lastHealth = bossHealth.CurrentHealth;
        }
    }

    void OnDrawGizmos()
    {
        if (!showGizmos || bossHealth == null) return;

        Vector3 barPosition = transform.position + Vector3.up * barOffset;
        float healthPercent = (float)bossHealth.CurrentHealth / bossHealth.MaxHealth;

        Gizmos.color = Color.black;
        DrawBar(barPosition, barWidth, barHeight, 1f);

        Gizmos.color = Color.Lerp(damagedColor, healthBarColor, healthPercent);
        DrawBar(barPosition, barWidth * 0.9f, barHeight * 0.8f, healthPercent);
    }

    void DrawBar(Vector3 center, float width, float height, float fillPercent)
    {
        float filledWidth = width * fillPercent;
        Vector3 size = new Vector3(filledWidth, height, 0.1f);
        Vector3 offset = new Vector3((filledWidth - width) * 0.5f, 0, 0);
        Gizmos.DrawCube(center + offset, size);
    }

    [ContextMenu("Damage Boss (1 HP)")]
    void DebugDamage()
    {
        if (Application.isPlaying)
            bossHealth.TakeDamage(1);
        else
            Debug.LogWarning("Enter Play mode to damage boss");
    }

    [ContextMenu("Kill Boss")]
    void DebugKill()
    {
        if (Application.isPlaying)
            bossHealth.TakeDamage(bossHealth.MaxHealth);
        else
            Debug.LogWarning("Enter Play mode to kill boss");
    }
}
