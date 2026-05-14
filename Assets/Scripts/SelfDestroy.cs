using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    protected bool isDead = false;

    [SerializeField] protected GameObject[] items;
    public virtual void DestroySelf()
    {
        Destroy(gameObject);
    }

    protected virtual void DropItems()
    {
        foreach (GameObject item in items)
        {
            Debug.Log($"Attempting to drop item: {item.name}");
            float randomValue = Random.value;
            Debug.Log($"Random value for dropping {item.name}: {randomValue}");
            ItemData itemData = item.GetComponent<ItemData>();
            if (itemData != null && randomValue < itemData.GetDropRate())
            {
                Debug.Log($"Dropping item: {item.name} with drop rate: {itemData.GetDropRate()} and random value: {randomValue}");
                Instantiate(item, transform.position, Quaternion.identity);
            }
        }
    }
}
