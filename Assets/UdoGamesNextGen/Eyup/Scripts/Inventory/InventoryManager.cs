using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    private void Awake()
    {
        instance = this;
        /* if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } */
    }
    [SerializeField] public List<ItemInventory> itemInventoriesList;
    public Queue<ItemInventory> itemInventories = new Queue<ItemInventory>();
    private int counter = 0;

    private void Start()
    {
        foreach (var item in itemInventoriesList)
        {
            itemInventories.Enqueue(item);
        }
        CleanUI();
        GameSceneManager.Instance.OnPlayerEnteredOcean += CleanUI;
    }

    [SerializeField] private Sprite nullSprite;

    private void CleanUI()
    {
        itemInventories.Clear();
        foreach (var item in itemInventoriesList)
        {
            itemInventories.Enqueue(item);
        }
        foreach (var item in itemInventoriesList)
        {
            item.SetUI(nullSprite, 0);
        }
    }

    public void GetAllInventory(ItemInventory item, CollectableProperty collectableProperty)
    {

        if (collectableProperty.Amount != 0)
        {
            itemInventoriesList[counter++].SetUI(collectableProperty.iconImage, collectableProperty.GetAmount());
            itemInventories.Dequeue();
        }
    }
}
