using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    public event Action<GameObject> OnCollected;
    public CollectableProperty itemProperty;
    public TextMeshProUGUI nameText;

    void OnApplicationQuit()
    {
        /*itemProperty.isCollected = false;
        itemProperty.itemInventory = null;*/
        itemProperty.isSaved = false;
    }



    void Start()
    {
        //itemProperty.Amount = itemProperty.GetAmount();
        SetUI();
    }

    void OnEnable()
    {
        OnCollected += HandleOnCollected;
    }

    private void SetUI()
    {
        nameText.text = itemProperty.Name;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke(gameObject);
            itemProperty.isCollected = true;
        }
    }

    public void HandleOnCollected(GameObject gameObject)
    {
        if (!itemProperty.isCollected)
        {
            itemProperty.itemInventory = InventoryManager.instance.itemInventories.Dequeue();
            itemProperty.Amount++;
            itemProperty.itemInventory.SetUI(itemProperty.iconImage, itemProperty.Amount);
        }
        else
        {
            if (!itemProperty.isSaved)
            {
                itemProperty.Amount++;
                itemProperty.itemInventory.SetUI(itemProperty.iconImage, itemProperty.Amount);
            }
            else
            {
                itemProperty.Amount++;
                foreach (var item in InventoryManager.instance.itemInventoriesList)
                {
                    if(item.icon.sprite == itemProperty.iconImage)
                    {
                        item.SetUI(itemProperty.iconImage, itemProperty.Amount);
                    }
                }
            }

        }

        itemProperty.SaveInventory();

    }
}
