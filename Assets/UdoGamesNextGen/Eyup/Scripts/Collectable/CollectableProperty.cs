using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "Property 1", menuName = "CollectableProperty/Property", order = 1)]
public class CollectableProperty : ScriptableObject
{
    public int BaseCost;
    public int Amount;
    public string Name;
    public Sprite iconImage;
    public bool isCollected;
    public bool isSaved;
    public ItemInventory itemInventory;

    public enum Conditions
    {
        Damaged,
        Moderate,
        Good
    }
    public Conditions condition;

    public enum Frequency
    {
        High,
        Average, // Düzeltilmiş yazım hatası
        Low
    }
    public Frequency frequency;

    private async void OnEnable() 
    {
        await SaveWhenEnable();
    }

    public int GetCost()
    {
        return CalculateCost();
    }

    public int CalculateCost()
    {
        float conditionMultiplier = 1.0f;
        float frequencyMultiplier = 1.0f;

        switch (condition)
        {
            case Conditions.Damaged:
                conditionMultiplier = 0.5f;
                break;
            case Conditions.Moderate:
                conditionMultiplier = 1.0f;
                break;
            case Conditions.Good:
                conditionMultiplier = 1.5f;
                break;
        }

        switch (frequency)
        {
            case Frequency.High:
                frequencyMultiplier = 0.75f;
                break;
            case Frequency.Average:
                frequencyMultiplier = 1.0f;
                break;
            case Frequency.Low:
                frequencyMultiplier = 1.25f;
                break;
        }

        return Mathf.RoundToInt(BaseCost * conditionMultiplier * frequencyMultiplier);
    }

    public void SaveInventory()
    {
        PlayerPrefs.SetInt(Name, Amount);
    }

    public int GetAmount()
    {
        if (!PlayerPrefs.HasKey(Name))
        {
            SaveInventory();
        }

        return PlayerPrefs.GetInt(Name);
    }

    private async Task SaveWhenEnable()
    {
        await Task.Delay(100); // Delay added to simulate some async work
        SaveAll();
    }

    public void SaveAll()
    {
        if (!isSaved)
        {
            if (InventoryManager.instance != null)
            {
                InventoryManager.instance.SaveAllInventory(itemInventory, this);
                isSaved = true;
            }
            else
            {
                Debug.LogError("InventoryManager instance is null!");
            }
        }
    }
}
