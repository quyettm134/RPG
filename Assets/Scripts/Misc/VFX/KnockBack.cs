using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool gettingKnockedBack { get; private set; }
    [SerializeField] private float knockBackTime = 0.1f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float knockBackPower)
    {
        gettingKnockedBack = true;
        Vector2 diff = (transform.position - damageSource.position).normalized * knockBackPower * rb.mass;
        rb.AddForce(diff, ForceMode2D.Impulse);
        StartCoroutine(KnockBackRoutine());
    }

    private IEnumerator KnockBackRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.linearVelocity = Vector2.zero;
        gettingKnockedBack = false;
    }
}
