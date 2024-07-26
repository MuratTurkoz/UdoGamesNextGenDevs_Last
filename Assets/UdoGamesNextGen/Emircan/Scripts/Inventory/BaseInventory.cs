using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
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
        amount = 1;
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

    [SerializeField] private TextMeshProUGUI _inGameBackpackTMP;

    [SerializeField] private List<ItemSO> _allItemsList;
    public Int BackpackLimit;
    private int itemCount;
    private List<ItemSO> _inventoryItems;

    private string savePath;

    public void SetItemCount()
    {
        UIManager.Instance.SetBackpackItemCount(_inventoryItems.Count);
    }

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

    /* private void OnEnable() {
        foreach (var item in _allItemsList)
        {
            item.collectableProperty.isCollected = false;
            item.collectableProperty.isSaved = false;
        }
    } */

    /* private void OnDisable() {
        
    } */

    private void SetInGameBackpackCount()
    {
        _inGameBackpackTMP.SetText(itemCount + " / " + BackpackLimit.Value);
    }

    private void ResetCount()
    {
        itemCount = 0;
        SetInGameBackpackCount();
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
        string json = PlayerPrefs.GetString("inventoryemir", "0");
        if (json == "0")
        {
            SetItemCount();
            return;
        }
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

        SetItemCount();

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
        SetInGameBackpackCount();
        _inventoryItems.Add(item);
    }

    public virtual void RemoveItem(ItemSO itemSO)
    {
        _inventoryItems.Remove(itemSO);
        SetItemCount();
        itemCount--;
        SetInGameBackpackCount();
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
