using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] private GameObject goldCoin, health;

    public void ItemDrop()
    {
        switch (Random.Range(1, 3))
        {
            case 1:
                Instantiate(goldCoin, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(health, transform.position, Quaternion.identity);
                break;
        }
    }
}
