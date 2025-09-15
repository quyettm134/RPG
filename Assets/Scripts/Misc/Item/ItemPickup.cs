using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private enum ItemType
    {
        GoldCoin,
        Health
    }

    [SerializeField] private ItemType itemType;
    [SerializeField] private float pickupDistance = 5f;
    [SerializeField] private float accelerationRate = 0.2f;
    [SerializeField] private float moveSpeed = 3f;

    private Vector3 moveDirection;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 playerPosition = Player.Instance.transform.position;
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

        bool canPickup = true;

        switch (itemType)
        {
            case ItemType.GoldCoin:
                canPickup = distanceToPlayer < pickupDistance;
                break;

            case ItemType.Health:
                canPickup = distanceToPlayer < pickupDistance &&
                            PlayerHealth.Instance.CurrentHealth < PlayerHealth.Instance.MaxHealth;
                break;
        }

        moveDirection = canPickup ? (playerPosition - transform.position).normalized : Vector3.zero;
        moveSpeed = canPickup ? moveSpeed + accelerationRate : 0;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            if (HandlePickup())
            {
                Destroy(this.gameObject);
            }
        }
    }

    private bool HandlePickup()
    {
        switch (itemType)
        {
            case ItemType.GoldCoin:
                CoinCounter.Instance.UpdateCurrentCoin();
                return true;

            case ItemType.Health:
                if (PlayerHealth.Instance.CurrentHealth < PlayerHealth.Instance.MaxHealth)
                {
                    PlayerHealth.Instance.Heal();
                    return true;
                }
                return false;

            default:
                return false;
        }
    }
}
