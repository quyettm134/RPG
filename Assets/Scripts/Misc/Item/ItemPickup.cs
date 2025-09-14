using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private float PickupDistance = 5f;
    [SerializeField] private float accelarationRate = 0.2f;
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

        moveDirection = Vector3.Distance(this.transform.position, playerPosition) < PickupDistance ?
                        (playerPosition - this.transform.position).normalized :
                        Vector3.zero;

        moveSpeed = Vector3.Distance(this.transform.position, playerPosition) < PickupDistance ?
                    moveSpeed + accelarationRate :
                    0;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Destroy(this.gameObject);
        }
    }
}
