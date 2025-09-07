using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private Transform slashEffect;
    [SerializeField] private Transform weaponCollider;

    private PlayerControls playerControls;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;
    private Animator animator;
    private GameObject slashAnimation;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update()
    {
        MouseFollow();
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);

        slashAnimation = Instantiate(slashPrefab, slashEffect.position, Quaternion.identity);
        slashAnimation.transform.parent = this.transform.parent;
        slashAnimation.GetComponent<SpriteRenderer>().flipX = playerController.FacingLeft;
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
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

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
