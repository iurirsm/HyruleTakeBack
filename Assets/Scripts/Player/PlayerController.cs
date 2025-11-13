using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem; 
#endif

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float jumpForce = 12f;
    [SerializeField] float airControlMultiplier = 0.8f;

    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundRadius = 0.15f;
    [SerializeField] LayerMask groundLayer;

    Rigidbody2D rb;
    SpriteRenderer sr;

    bool isGrounded;
    float inputX;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        inputX = ReadHorizontal();

        if (JumpPressedThisFrame() && IsGrounded())
        {
        
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (inputX != 0) sr.flipX = (inputX < 0);
    }

    void FixedUpdate()
    {
        float control = IsGrounded() ? 1f : airControlMultiplier;
        float targetVX = inputX * moveSpeed * control;
        rb.linearVelocity = new Vector2(targetVX, rb.linearVelocity.y);
    }

    bool IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        return isGrounded;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    // -------- input helpers --------
    float ReadHorizontal()
    {
        float x = 0f;
#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x -= 1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x += 1f;
            return x;
        }
#endif
        return Input.GetAxisRaw("Horizontal");
    }

    bool JumpPressedThisFrame()
    {
#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null) return Keyboard.current.spaceKey.wasPressedThisFrame;
#endif
        return Input.GetButtonDown("Jump");
    }
}
