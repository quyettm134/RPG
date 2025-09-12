using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveDirection;
    private KnockBack knockBack;
    private bool isFlipped;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockBack = GetComponent<KnockBack>();
        isFlipped = spriteRenderer.flipX;
    }

    private void Update()
    {
        animator.SetFloat("horizontal", Mathf.Abs(moveDirection.x));
        animator.SetFloat("vertical", Mathf.Abs(moveDirection.y));

        if (Mathf.Abs(moveDirection.x) > 0.05f)
        {
            isFlipped = moveDirection.x < -0.05f;
        }

        spriteRenderer.flipX = isFlipped;
    }

    private void FixedUpdate()
    {
        if (knockBack.gettingKnockedBack) return;
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public void MoveTo(Vector2 position)
    {
        moveDirection = position;
    }
}
