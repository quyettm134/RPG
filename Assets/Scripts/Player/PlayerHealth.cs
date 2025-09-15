using System.Collections;
using UnityEngine;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] private int currentHealth = 10;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float knockBackForce = 10f;
    [SerializeField] private float recoveryTime = 1f;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private bool canTakeDamage = true;
    private KnockBack knockBack;
    private Flash flash;

    protected override void Awake()
    {
        base.Awake();
        currentHealth = maxHealth;
        knockBack = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
        if (enemy && canTakeDamage)
        {
            TakeDamage(1);
            knockBack.GetKnockedBack(collision.gameObject.transform, knockBackForce);
            StartCoroutine(flash.Flashing());
        }
    }

    public void Heal()
    {
        currentHealth++;
    }

    private void TakeDamage(int damage)
    {
        canTakeDamage = false;
        currentHealth -= damage;
        StartCoroutine(Recovery());
    }

    private IEnumerator Recovery()
    {
        yield return new WaitForSeconds(recoveryTime);
        canTakeDamage = true;
    }
}
