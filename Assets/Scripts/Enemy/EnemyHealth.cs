using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int baseHealth = 3;
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private float knockBackForce = 15f;

    private int currentHealth;
    private KnockBack knockBack;
    private Flash flash;

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();
    }

    void Start()
    {
        currentHealth = baseHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockBack.GetKnockedBack(PlayerController.instance.transform, knockBackForce);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckStatusRoutine());
    }

    private IEnumerator CheckStatusRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMaterialTime());
        IsDead();
    }

    public void IsDead()
    {
        if (currentHealth <= 0)
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
