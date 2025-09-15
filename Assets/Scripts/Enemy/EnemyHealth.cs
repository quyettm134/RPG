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
    private ItemSpawn item;

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();
        item = GetComponent<ItemSpawn>();
    }

    void Start()
    {
        currentHealth = baseHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockBack.GetKnockedBack(Player.Instance.transform, knockBackForce);
        StartCoroutine(flash.Flashing());
        StartCoroutine(CheckStatus());
    }

    private IEnumerator CheckStatus()
    {
        yield return new WaitForSeconds(flash.GetRestoreMaterialTime());
        IsDead();
    }

    public void IsDead()
    {
        if (currentHealth <= 0)
        {
            item.ItemDrop();
            Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
