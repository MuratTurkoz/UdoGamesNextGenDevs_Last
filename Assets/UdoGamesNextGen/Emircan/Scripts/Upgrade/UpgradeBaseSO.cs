using System.Collections;
using System.Collections.Generic;
using UdoGames.NextGenDev;
using UnityEngine;

public abstract class UpgradeBaseSO : ScriptableObject
{
    public int level = 1;
    public int[] prices;
    public int Price => prices.Length >= level ? prices[level - 1] : 0;
    public string upgradeName;
    public Sprite icon;


    public virtual void Load()
    {
        level = 1; // TEMP
    }

    public void ReducePrice()
    {
        CurrencyManager.Instance.ReduceGold(Price, "Upgrade " + upgradeName);
    }

    public abstract void Upgrade();
    public abstract string CurrentValue();
    public abstract string NextLevelValue();

    public bool CanPlayerBuy()
    {
        return CurrencyManager.Instance.Gold >= Price;
    }
    public bool CanLevelUpMore()
    {
        return level < prices.Length;
    }
}
