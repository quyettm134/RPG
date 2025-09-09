using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int baseHealth = 3;

    private int currentHealth;
    private KnockBack knockBack;

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
    }

    void Start()
    {
        currentHealth = baseHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockBack.GetKnockedBack(PlayerController.instance.transform, 15f);
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
