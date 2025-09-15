using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] private int currentHealth = 10;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float knockBackForce = 10f;
    [SerializeField] private float recoveryTime = 1f;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private Slider healthSlider;
    private bool canTakeDamage = true;
    private KnockBack knockBack;
    private Flash flash;

    protected override void Awake()
    {
        base.Awake();
        knockBack = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthSlider();
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
        if (CurrentHealth < MaxHealth)
        {
            currentHealth++;
            UpdateHealthSlider();
        }
    }

    private void TakeDamage(int damage)
    {
        canTakeDamage = false;
        currentHealth -= damage;
        UpdateHealthSlider();
        StartCoroutine(Recovery());
    }

    private IEnumerator Recovery()
    {
        yield return new WaitForSeconds(recoveryTime);
        canTakeDamage = true;
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
        }

        healthSlider.maxValue = MaxHealth;
        healthSlider.value = CurrentHealth;
    }
}
