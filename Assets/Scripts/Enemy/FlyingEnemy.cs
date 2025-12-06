using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float horizontalSpeed = 2f;
    [SerializeField] bool startFacingRight = true;

    [Header("Wave Motion")]
    [SerializeField] float waveAmplitude = 0.5f;
    [SerializeField] float waveFrequency = 2f;

    [Header("Detection")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float wallDetectionDistance = 0.5f;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Collider2D col;
    int dir;
    float waveTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        
        dir = startFacingRight ? 1 : -1;
        if (sr) sr.flipX = (dir < 0);
        
        rb.gravityScale = 0;
    }

    void FixedUpdate()
    {
        waveTime += Time.fixedDeltaTime;
        
        float verticalVelocity = Mathf.Cos(waveTime * waveFrequency) * waveAmplitude * waveFrequency;
        
        rb.linearVelocity = new Vector2(dir * horizontalSpeed, verticalVelocity);
        
        if (ShouldTurnAround())
            Flip();
    }

    bool ShouldTurnAround()
    {
        if (col == null) return false;

        Bounds b = col.bounds;
        Vector2 rayOrigin = new Vector2(b.center.x, b.center.y);
        
        bool wallHit = Physics2D.Raycast(rayOrigin, new Vector2(dir, 0f), wallDetectionDistance, groundLayer);
        
        return wallHit;
    }

    void Flip()
    {
        dir *= -1;
        if (sr) sr.flipX = (dir < 0);
    }

    void OnDrawGizmosSelected()
    {
        if (!GetComponent<Collider2D>()) return;
        
        var b = GetComponent<Collider2D>().bounds;
        int d = (Application.isPlaying ? dir : (startFacingRight ? 1 : -1));

        Vector2 rayOrigin = new Vector2(b.center.x, b.center.y);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(rayOrigin, rayOrigin + new Vector2(d, 0f) * wallDetectionDistance);
    }
}
