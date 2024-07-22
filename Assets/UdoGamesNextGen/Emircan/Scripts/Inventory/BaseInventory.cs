using System.Collections;
using System.Collections.Generic;
using UdoGames.NextGenDev;
using UnityEngine;

public class BaseInventory : MonoBehaviour
{
    [SerializeField] private List<ItemSO> _inventoryItems;

    public virtual int ItemCount()
    {
        return _inventoryItems.Count;
    }

    public virtual void RemoveItem(ItemSO itemSO)
    {
        _inventoryItems.Remove(itemSO);
    }

    public virtual ItemSO GetRandomItem()
    {
        return _inventoryItems[Random.Range(0, _inventoryItems.Count)];
    }

}
