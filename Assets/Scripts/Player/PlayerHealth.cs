using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float knockBackForce = 10f;
    [SerializeField] private float recoveryTime = 1f;

    private int currentHealth;
    private bool canTakeDamage = true;
    private KnockBack knockBack;
    private Flash flash;

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
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
