using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int baseDamage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(baseDamage);
        }
    }
}
