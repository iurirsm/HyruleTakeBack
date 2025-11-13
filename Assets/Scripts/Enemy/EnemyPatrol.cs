using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 2f;
    [SerializeField] bool startFacingRight = true;

    [Header("Detection")]
    [SerializeField] LayerMask groundLayer;     
    [SerializeField] float edgeAhead = 0.4f;    
    [SerializeField] float edgeDown = 0.8f;     
    [SerializeField] float wallAhead = 0.3f;    
    [SerializeField] float chestHeight = 0.3f;  

    Rigidbody2D rb;
    SpriteRenderer sr;
    Collider2D col;
    int dir; // +1 = right, -1 = left

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        dir = startFacingRight ? 1 : -1;
        if (sr) sr.flipX = (dir < 0);
    }

    void FixedUpdate()
    {
        
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);

        
        if (ShouldTurnAround())
            Flip();
    }

    bool ShouldTurnAround()
    {
        Bounds b = col.bounds;

        
        Vector2 aheadFeet = new Vector2(
            b.center.x + dir * (b.extents.x + edgeAhead),
            b.min.y + 0.05f
        );

        
        bool groundHit = Physics2D.Raycast(aheadFeet, Vector2.down, edgeDown, groundLayer);

        // wall check from chest
        Vector2 chest = new Vector2(b.center.x, b.center.y + chestHeight);
        bool wallHit = Physics2D.Raycast(chest, new Vector2(dir, 0f), wallAhead, groundLayer);

        return !groundHit || wallHit;
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

        Vector2 aheadFeet = new Vector2(b.center.x + d * (b.extents.x + edgeAhead), b.min.y + 0.05f);
        Vector2 chest = new Vector2(b.center.x, b.center.y + chestHeight);

        Gizmos.color = Color.yellow; 
        Gizmos.DrawLine(aheadFeet, aheadFeet + Vector2.down * edgeDown);

        Gizmos.color = Color.cyan; 
        Gizmos.DrawLine(chest, chest + new Vector2(d, 0f) * wallAhead);
    }
}
