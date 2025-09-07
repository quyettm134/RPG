using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int baseHealth = 3;

    private int currentHealth;

    void Start()
    {
        currentHealth = baseHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        IsDead();
    }

    private void IsDead()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
