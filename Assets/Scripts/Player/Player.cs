using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer trailRenderer;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private bool isDashing = false;
    private float baseMoveSpeed;

    public bool FacingLeft => facingLeft;
    private bool facingLeft = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private KnockBack knockBack;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockBack = GetComponent<KnockBack>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        var vcam = FindFirstObjectByType<CinemachineCamera>();
        if (vcam != null)
        {
            vcam.Follow = transform;
        }

        playerControls.Movement.Dash.performed += _ => Dash();
        baseMoveSpeed = moveSpeed;
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
        if (knockBack.gettingKnockedBack) return;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            trailRenderer.emitting = true;
            StartCoroutine(EndDash());
        }
    }

    private void AdjustFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        spriteRenderer.flipX = facingLeft = mousePos.x < playerScreenPoint.x;
    }

    private IEnumerator EndDash()
    {
        float dashTime = 0.2f;
        float dashCD = 0.5f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = baseMoveSpeed;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
