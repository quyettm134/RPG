using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DamageSource>())
        {
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
