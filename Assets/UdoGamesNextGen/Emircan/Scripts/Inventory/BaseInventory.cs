using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UdoGames.NextGenDev;
using UnityEngine;

[Serializable]
public class SerializableItem
{
    public string itemName;
    public int amount;

    public SerializableItem(ItemSO item)
    {
        itemName = item.ItemName;
        amount = item.collectableProperty.Amount;
    }
}

[Serializable]
public class InventoryData
{
    public List<SerializableItem> items;
}

public class BaseInventory : MonoBehaviour
{
    public static BaseInventory Instance { get; private set; }

    [SerializeField] private List<ItemSO> _allItemsList;
    public Int BackpackLimit;
    private int itemCount;
    private List<ItemSO> _inventoryItems;

    private string savePath;

    private void Awake()
    {
        Instance = this;
        _inventoryItems = new List<ItemSO>();
        savePath = Path.Combine(Application.persistentDataPath, "inventory.json");
    }

    private void Start()
    {
        Load();
        if (!GameSceneManager.Instance) return;

        GameSceneManager.Instance.OnPlayerEnteredOcean += ResetCount;
        GameSceneManager.Instance.OnPlayerEnteredMarket += OnOceanEnd;
    }

    private void ResetCount()
    {
        itemCount = 0;
        foreach (var item in _allItemsList)
        {
            item.collectableProperty.ClearSave();
        }
    }

    /* private void OnDisable()
    {
        Save();
    } */

    public bool IsThereAnySpace()
    {
        return itemCount < BackpackLimit.Value;
    }

    private void Load()
    {
        if (File.Exists(savePath))
        {
            /* string json = File.ReadAllText(savePath); */

        }
        string json = PlayerPrefs.GetString("inventoryemir", "0");
        if (json == "0") return;
        InventoryData data = JsonUtility.FromJson<InventoryData>(json);

        _inventoryItems.Clear();
        foreach (var serializableItem in data.items)
        {
            ItemSO item = _allItemsList.Find(x => x.ItemName == serializableItem.itemName);
            for (int i = 0; i < serializableItem.amount; i++)
            {
                _inventoryItems.Add(item);
            }
        }

        /* itemCount = _inventoryItems.Count; */
    }

    private void Save()
    {
        InventoryData data = new InventoryData();
        data.items = _inventoryItems.Select(item => new SerializableItem(item)).ToList();

        string json = JsonUtility.ToJson(data, true);
        /* File.WriteAllText(savePath, json); */
        PlayerPrefs.SetString("inventoryemir", json);
    }

    public void OnOceanEnd()
    {
        Save();
        foreach (var item in _inventoryItems)
        {
            item.collectableProperty.ClearSave();
        }
        itemCount = 0;
    }

    public virtual int ItemCount()
    {
        return _inventoryItems.Count;
    }

    public void AddItem(ItemSO item)
    {
        itemCount++;
        _inventoryItems.Add(item);
    }

    public virtual void RemoveItem(ItemSO itemSO)
    {
        _inventoryItems.Remove(itemSO);
        itemCount--;
        /* int amount = Mathf.Max(0, itemSO.collectableProperty.Amount - 1);
        itemSO.collectableProperty.Amount = amount;
        if (amount <= 0)
        {
            itemSO.collectableProperty.isSaved = false;
            itemSO.collectableProperty.isCollected = false;
        }
        itemSO.collectableProperty.SaveInventory(); */
    }

    public virtual ItemSO GetRandomItem()
    {
        return _inventoryItems[UnityEngine.Random.Range(0, _inventoryItems.Count)];
    }
}
