using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft
    {
        get
        {
            return facingLeft;
        }
        set
        {
            facingLeft = value;
        }
    }
    [SerializeField] private float moveSpeed = 1.0f;
    public static PlayerController instance;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private bool facingLeft = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        instance = this;
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        animator.SetFloat("horizontal", Mathf.Abs(movement.x));
        animator.SetFloat("vertical", Mathf.Abs(movement.y));
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void AdjustFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        spriteRenderer.flipX = FacingLeft = mousePos.x < playerScreenPoint.x;
    }
}
