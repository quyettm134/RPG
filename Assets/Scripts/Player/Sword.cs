using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerControls playerControls;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
