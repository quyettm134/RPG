using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private Transform slashEffect;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private float attackCD = 0.5f;

    private PlayerControls playerControls;
    private Player player;
    private ActiveWeapon activeWeapon;
    private Animator animator;
    private GameObject slashAnimation;
    private bool attackBtnDown, isAttacking = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        player = GetComponentInParent<Player>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        MouseFollow();
        Attack();
    }

    private void StartAttacking()
    {
        attackBtnDown = true;
    }

    private void StopAttacking()
    {
        attackBtnDown = false;
    }

    private void Attack()
    {
        if (attackBtnDown && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);

            slashAnimation = Instantiate(slashPrefab, slashEffect.position, Quaternion.identity);
            slashAnimation.transform.parent = this.transform.parent;
            slashAnimation.GetComponent<SpriteRenderer>().flipX = player.FacingLeft;

            StartCoroutine(AttackCDRoutine());
        }
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(attackCD);
        isAttacking = false;
    }

    private void AttackRegistering()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlip()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
    }

    public void SwingDownFlip()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void MouseFollow()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(player.transform.position);

        float mouseAngle = Mathf.Atan2(mousePos.x, mousePos.y) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, mouseAngle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, mouseAngle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
