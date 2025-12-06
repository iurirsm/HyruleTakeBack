using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtsPlayer : MonoBehaviour
{
    bool disabled = false;  

    public void DisableHurter() => disabled = true;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (disabled) return;
        if (!col.collider.CompareTag("Player")) return;

        var health = col.collider.GetComponent<PlayerHealth>();
        if (health) health.Kill();
    }
}
