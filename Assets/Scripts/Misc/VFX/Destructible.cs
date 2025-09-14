using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DamageSource>())
        {
            var item = this.GetComponent<ItemSpawn>();
            if (item != null)
            {
                item.ItemDrop();
            }
            Instantiate(destroyVFX, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
