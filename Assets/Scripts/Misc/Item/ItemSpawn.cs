using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] private GameObject goldCoin;

    public void ItemDrop()
    {
        Instantiate(goldCoin, this.transform.position, Quaternion.identity);
    }
}
